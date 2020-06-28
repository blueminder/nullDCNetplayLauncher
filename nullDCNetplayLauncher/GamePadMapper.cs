using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace nullDCNetplayLauncher
{
    public class GamePadMapper : IDisposable
    {
        bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private ControllerEngine controller;
        private Dictionary<string, string> KeyboardQkcMapping;
        private GamePadState OldState;

        public GamePadMapper()
        {
            controller = new ControllerEngine();
            controller.clock.Start();
            KeyboardQkcMapping = ReadKeyboardQkc();
            System.Diagnostics.Debug.WriteLine(controller.CapabilitiesGamePad.ToString());
            //hWindow = Launcher.NullDCWindowHandle();
        }


        public void InitializeController(Object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(controller.CapabilitiesGamePad.ToString());
        }

        PropertyInfo[] AvailableButtonProperties = typeof(GamePadButtons).GetProperties().Where(
            p =>
            {
                return p.Name != "IsAnyButtonPressed";
            }).ToArray();

        PropertyInfo[] AvailableDPadProperties = typeof(GamePadDPad).GetProperties().Where(
            p =>
            {
                return p.PropertyType == typeof(Boolean);
            }).ToArray();

        public void InputRoll(object sender, EventArgs e)
        {
            var State = GamePad.GetState(0);
            var Capabilities = controller.CapabilitiesGamePad;

            foreach (PropertyInfo buttonProperty in AvailableButtonProperties)
            {
                ButtonState CurrentButtonState = (ButtonState)buttonProperty.GetValue(State.Buttons);
                ButtonState OldButtonState = (ButtonState)buttonProperty.GetValue(State.Buttons);
                if (!Object.ReferenceEquals(OldState, null))
                {
                    OldButtonState = (ButtonState)buttonProperty.GetValue(OldState.Buttons);
                }

                if (CurrentButtonState == ButtonState.Pressed &&
                (Object.ReferenceEquals(OldState, null) || OldButtonState == ButtonState.Released))
                {
                    CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Pressed");
                }
                if (CurrentButtonState == ButtonState.Released &&
                    (!Object.ReferenceEquals(OldState, null) && OldButtonState == ButtonState.Pressed))
                {
                    CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Released");
                }
            }

            foreach (PropertyInfo buttonProperty in AvailableDPadProperties)
            {
                Boolean CurrentDPadState = (Boolean)buttonProperty.GetValue(State.DPad);
                Boolean OldDPadState = (Boolean)buttonProperty.GetValue(State.DPad);
                if (!Object.ReferenceEquals(OldState, null))
                {
                    OldDPadState = (Boolean)buttonProperty.GetValue(OldState.DPad);
                }

                if (CurrentDPadState == true &&
                (Object.ReferenceEquals(OldState, null) || OldDPadState == false))
                {
                    CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Pressed");
                }
                if (CurrentDPadState == false &&
                    (!Object.ReferenceEquals(OldState, null) && OldDPadState == true))
                {
                    CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Released");
                }
            }

            PropertyInfo[] AvailableTriggerProperties = typeof(GamePadTriggers).GetProperties().ToArray();

            foreach (PropertyInfo buttonProperty in AvailableTriggerProperties)
            {
                float CurrentTriggerState = (float)buttonProperty.GetValue(State.Triggers);
                float OldTriggerState = (float)buttonProperty.GetValue(State.Triggers);
                if (!Object.ReferenceEquals(OldState, null))
                {
                    OldTriggerState = (float)buttonProperty.GetValue(OldState.Triggers);
                }

                if (CurrentTriggerState == 1 &&
                (Object.ReferenceEquals(OldState, null) || OldTriggerState == 0))
                {
                    CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Trigger Pressed");
                }
                if (CurrentTriggerState == 0 &&
                    (!Object.ReferenceEquals(OldState, null) && OldTriggerState == 1))
                {
                    CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Trigger Released");
                }
            }

            var oldLeftX = Math.Round(OldState.ThumbSticks.Left.X);
            var oldLeftY = Math.Round(OldState.ThumbSticks.Left.Y);

            var newLeftX = Math.Round(State.ThumbSticks.Left.X);
            var newLeftY = Math.Round(State.ThumbSticks.Left.Y);

            if (oldLeftX == 0 && newLeftX == 1)
            {
                System.Diagnostics.Debug.WriteLine("Right Pushed");
                CallButtonMapping("IsRight", true);
            }

            if (oldLeftX == 1 && newLeftX == 0)
            {
                System.Diagnostics.Debug.WriteLine("Right Released");
                CallButtonMapping("IsRight", false);
            }

            if (oldLeftX == 0 && newLeftX == -1)
            {
                System.Diagnostics.Debug.WriteLine("Left Pushed");
                CallButtonMapping("IsLeft", true);
            }

            if (oldLeftX == -1 && newLeftX == 0)
            {
                System.Diagnostics.Debug.WriteLine("Left Released");
                CallButtonMapping("IsLeft", false);
            }

            if (oldLeftY == 0 && newLeftY == 1)
            {
                System.Diagnostics.Debug.WriteLine("Up Pushed");
                CallButtonMapping("IsUp", true);
            }

            if (oldLeftY == 1 && newLeftY == 0)
            {
                System.Diagnostics.Debug.WriteLine("Up Released");
                CallButtonMapping("IsUp", false);
            }

            if (oldLeftY == 0 && newLeftY == -1)
            {
                System.Diagnostics.Debug.WriteLine("Down Pushed");
                CallButtonMapping("IsDown", true);
            }

            if (oldLeftY == -1 && newLeftY == 0)
            {
                System.Diagnostics.Debug.WriteLine("Down Released");
                CallButtonMapping("IsDown", false);
            }

            OldState = State;
        }

        public String GetThumbStickDirection(double X, double Y)
        {
            var roundX = Math.Round(X);
            var roundY = Math.Round(Y);

            if(roundX == 0)
            {
                if (roundY == 1)
                    return "Up";
                else if (roundY == -1)
                    return "Down";
            }
            else if (roundY == 0)
            {
                if (roundX == -1)
                    return "Left";
                else if (roundX == 1)
                    return "Right";
            }
            else if (roundX == 1)
            {
                if (roundY == -1)
                    return "DownRight";
                else if (roundY == 1)
                    return "UpRight";
            }
            else if (roundX == -1)
            {
                if (roundY == -1)
                    return "DownLeft";
                else if (roundY == 1)
                    return "UpLeft";
            }
            System.Diagnostics.Debug.WriteLine($"{roundX} {roundY}");
            return "Center";
        }

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern IntPtr SetFocus(IntPtr hWnd);

        const byte KEY_1 = 0x31;
        const byte KEY_3 = 0x33;
        const byte KEY_5 = 0x35;
        const byte KEY_W = 0x57;
        const byte KEY_S = 0x53;
        const byte KEY_A = 0x41;
        const byte KEY_D = 0x44;
        const byte KEY_8 = 0x38;
        const byte KEY_9 = 0x39;
        const byte KEY_0 = 0x30;
        const byte KEY_U = 0x55;
        const byte KEY_I = 0x49;
        const byte KEY_O = 0x4F;

        private void CallButtonMapping(string button, bool push)
        {
            String called = push ? "Pushed" : "Released";
            System.Diagnostics.Debug.WriteLine($"Button {button} {called}");
            Console.WriteLine($"Button {button} {called}");

            Dictionary<string, byte> VirtualMapping = new Dictionary<string, byte>()
            {
                { "", 0 },
                { "1", KEY_8 },
                { "2", KEY_9 },
                { "3", KEY_0 },
                { "4", KEY_U },
                { "5", KEY_I },
                { "6", KEY_O },
                { "Start", KEY_5 },
                { "Coin", KEY_1 },
                { "Test", KEY_3 },
                { "Up", KEY_W },
                { "Down", KEY_S },
                { "Left", KEY_A },
                { "Right", KEY_D },
            };

            String virtualIndex = (String)Launcher.ActiveGamePadMapping[button];
            if (virtualIndex.Equals("Test"))
            {
                System.Diagnostics.Debug.WriteLine($"Virtual Index {virtualIndex} {called}");
                //return;
            }

            if (push)
            {
                PushKey(VirtualMapping[virtualIndex]);
            }
            else
            {
                ReleaseKey(VirtualMapping[virtualIndex]);
            }
        }

        public void PushKey(byte key)
        {
            Process[] processes = Process.GetProcessesByName("nullDC_Win32_Release-NoTrace");
            //if (processes.Length > 0)
            //{
                //Process ndc = processes[0];
                //IntPtr ptr = ndc.MainWindowHandle;

                //SetFocus(ptr);

                keybd_event(key, 0, 0, 0);
            //}
        }

        public void ReleaseKey(byte key)
        {
            //Process[] processes = Process.GetProcessesByName("nullDC_Win32_Release-NoTrace");
            //if (processes.Length > 0)
            //{
                const uint KEYEVENTF_KEYUP = 0x0002;
                keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
            //}
        }

        public Dictionary<string, string> ReadKeyboardQkc()
        {
            var keyboardQkcMapping = new Dictionary<string, string>();
            string keyboardQkcPath = Launcher.rootDir + "nulldc-1-0-4-en-win\\qkoJAMMA\\Keyboard.qkc";
            var keyboardQkcLines = File.ReadAllLines(keyboardQkcPath);

            foreach (string line in keyboardQkcLines)
            {
                var entry = line.Split('=');
                keyboardQkcMapping.Add(entry[1], entry[0]);
            }

            return keyboardQkcMapping;
        }
    }

}
