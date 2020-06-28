using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using XInputDotNetPure;

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
        private OpenTK.Input.GamePadState OTKOldState;
        private XInputDotNetPure.GamePadState XOldState;

        public GamePadMapper(ControllerEngine controllerEngine)
        {
            controller = controllerEngine;
            StartClock();
            KeyboardQkcMapping = ReadKeyboardQkc();
            System.Diagnostics.Debug.WriteLine(controller.CapabilitiesGamePad.ToString());
            //hWindow = Launcher.NullDCWindowHandle();
        }

        public void StartClock()
        {
            controller.clock.Start();
        }

        public void StopClock()
        {
            controller.clock.Stop();
        }

        public void InputRoll(object sender, EventArgs e)
        {
            if (XInputDotNetPure.GamePad.GetState(PlayerIndex.One).IsConnected)
                XInputGamePadInputRoll();
            else
                OpenTKGamePadInputRoll();
        }

        PropertyInfo[] AvailableXButtonProperties = typeof(XInputDotNetPure.GamePadButtons).GetProperties().Where(
            p =>
            {
                return p.Name != "IsAnyButtonPressed";
            }).ToArray();

        PropertyInfo[] AvailableXDPadProperties = typeof(XInputDotNetPure.GamePadDPad).GetProperties().Where(
            p =>
            {
                return p.PropertyType == typeof(Boolean);
            }).ToArray();

        public void XInputGamePadInputRoll()
        {
            var State = XInputDotNetPure.GamePad.GetState(PlayerIndex.One);

            foreach (PropertyInfo buttonProperty in AvailableXButtonProperties)
            {
                XInputDotNetPure.ButtonState CurrentButtonState = (XInputDotNetPure.ButtonState)buttonProperty.GetValue(State.Buttons);
                XInputDotNetPure.ButtonState OldButtonState = (XInputDotNetPure.ButtonState)buttonProperty.GetValue(State.Buttons);
                if (!Object.ReferenceEquals(XOldState, null))
                {
                    OldButtonState = (XInputDotNetPure.ButtonState)buttonProperty.GetValue(XOldState.Buttons);
                }

                if (CurrentButtonState == XInputDotNetPure.ButtonState.Pressed &&
                (Object.ReferenceEquals(XOldState, null) || OldButtonState == XInputDotNetPure.ButtonState.Released))
                {
                    CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Pressed");
                }
                if (CurrentButtonState == XInputDotNetPure.ButtonState.Released &&
                    (!Object.ReferenceEquals(XOldState, null) && OldButtonState == XInputDotNetPure.ButtonState.Pressed))
                {
                    CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Released");
                }
            }

            PropertyInfo[] AvailableXTriggerProperties = typeof(XInputDotNetPure.GamePadTriggers).GetProperties().ToArray();

            foreach (PropertyInfo buttonProperty in AvailableXTriggerProperties)
            {
                float CurrentTriggerState = (float)buttonProperty.GetValue(State.Triggers);
                float OldTriggerState = (float)buttonProperty.GetValue(State.Triggers);
                if (!Object.ReferenceEquals(XOldState, null))
                {
                    OldTriggerState = (float)buttonProperty.GetValue(XOldState.Triggers);
                }

                if (CurrentTriggerState == 1 &&
                (Object.ReferenceEquals(XOldState, null) || OldTriggerState == 0))
                {
                    CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Trigger Pressed");
                }
                if (CurrentTriggerState == 0 &&
                    (!Object.ReferenceEquals(XOldState, null) && OldTriggerState == 1))
                {
                    CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Trigger Released");
                }
            }

            var oldUp = Convert.ToBoolean(XOldState.DPad.Up);
            var oldDown = Convert.ToBoolean(XOldState.DPad.Down);
            var oldLeft = Convert.ToBoolean(XOldState.DPad.Left);
            var oldRight = Convert.ToBoolean(XOldState.DPad.Right);

            var newUp = Convert.ToBoolean(State.DPad.Up);
            var newDown = Convert.ToBoolean(State.DPad.Down);
            var newLeft = Convert.ToBoolean(State.DPad.Left);
            var newRight = Convert.ToBoolean(State.DPad.Right);

            var oldLeftX = Math.Round(XOldState.ThumbSticks.Left.X);
            var oldLeftY = Math.Round(XOldState.ThumbSticks.Left.Y);

            var newLeftX = Math.Round(State.ThumbSticks.Left.X);
            var newLeftY = Math.Round(State.ThumbSticks.Left.Y);

            if (oldLeftX == 0 && newLeftX == 1 || oldRight == true && newRight == false)
            {
                System.Diagnostics.Debug.WriteLine("Right Pushed");
                CallButtonMapping("IsRight", true);
            }

            if (oldLeftX == 1 && newLeftX == 0 || oldRight == false && newRight == true)
            {
                System.Diagnostics.Debug.WriteLine("Right Released");
                CallButtonMapping("IsRight", false);
            }

            if (oldLeftX == 0 && newLeftX == -1 || oldLeft == true && newLeft == false)
            {
                System.Diagnostics.Debug.WriteLine("Left Pushed");
                CallButtonMapping("IsLeft", true);
            }

            if (oldLeftX == -1 && newLeftX == 0 || oldLeft == false && newLeft == true)
            {
                System.Diagnostics.Debug.WriteLine("Left Released");
                CallButtonMapping("IsLeft", false);
            }

            if (oldLeftY == 0 && newLeftY == 1 || oldUp == true && newUp == false)
            {
                System.Diagnostics.Debug.WriteLine("Up Pushed");
                CallButtonMapping("IsUp", true);
            }

            if (oldLeftY == 1 && newLeftY == 0 || oldUp == false && newUp == true)
            {
                System.Diagnostics.Debug.WriteLine("Up Released");
                CallButtonMapping("IsUp", false);
            }

            if (oldLeftY == 0 && newLeftY == -1 || oldDown == true && newDown == false)
            {
                System.Diagnostics.Debug.WriteLine("Down Pushed");
                CallButtonMapping("IsDown", true);
            }

            if (oldLeftY == -1 && newLeftY == 0 || oldDown == false && newDown == true)
            {
                System.Diagnostics.Debug.WriteLine("Down Released");
                CallButtonMapping("IsDown", false);
            }

            XOldState = State;
        }


        PropertyInfo[] AvailableOTKButtonProperties = typeof(OpenTK.Input.GamePadButtons).GetProperties().Where(
            p =>
            {
                return p.Name != "IsAnyButtonPressed";
            }).ToArray();

        PropertyInfo[] AvailableOTKDPadProperties = typeof(OpenTK.Input.GamePadDPad).GetProperties().Where(
            p =>
            {
                return p.PropertyType == typeof(Boolean);
            }).ToArray();

        public void OpenTKGamePadInputRoll()
        {
            var State = OpenTK.Input.GamePad.GetState(0);
            var Capabilities = controller.CapabilitiesGamePad;

            foreach (PropertyInfo buttonProperty in AvailableOTKButtonProperties)
            {
                OpenTK.Input.ButtonState CurrentButtonState = (OpenTK.Input.ButtonState)buttonProperty.GetValue(State.Buttons);
                OpenTK.Input.ButtonState OldButtonState = (OpenTK.Input.ButtonState)buttonProperty.GetValue(State.Buttons);
                if (!Object.ReferenceEquals(OTKOldState, null))
                {
                    OldButtonState = (OpenTK.Input.ButtonState)buttonProperty.GetValue(OTKOldState.Buttons);
                }

                if (CurrentButtonState == OpenTK.Input.ButtonState.Pressed &&
                (Object.ReferenceEquals(OTKOldState, null) || OldButtonState == OpenTK.Input.ButtonState.Released))
                {
                    CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Pressed");
                }
                if (CurrentButtonState == OpenTK.Input.ButtonState.Released &&
                    (!Object.ReferenceEquals(OTKOldState, null) && OldButtonState == OpenTK.Input.ButtonState.Pressed))
                {
                    CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Released");
                }
            }

            foreach (PropertyInfo buttonProperty in AvailableOTKDPadProperties)
            {
                Boolean CurrentDPadState = (Boolean)buttonProperty.GetValue(State.DPad);
                Boolean OldDPadState = (Boolean)buttonProperty.GetValue(State.DPad);
                if (!Object.ReferenceEquals(OTKOldState, null))
                {
                    OldDPadState = (Boolean)buttonProperty.GetValue(OTKOldState.DPad);
                }

                if (CurrentDPadState == true &&
                (Object.ReferenceEquals(OTKOldState, null) || OldDPadState == false))
                {
                    CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Pressed");
                }
                if (CurrentDPadState == false &&
                    (!Object.ReferenceEquals(OTKOldState, null) && OldDPadState == true))
                {
                    CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Released");
                }
            }

            PropertyInfo[] AvailableOTKTriggerProperties = typeof(OpenTK.Input.GamePadTriggers).GetProperties().ToArray();

            foreach (PropertyInfo buttonProperty in AvailableOTKTriggerProperties)
            {
                float CurrentTriggerState = (float)buttonProperty.GetValue(State.Triggers);
                float OldTriggerState = (float)buttonProperty.GetValue(State.Triggers);
                if (!Object.ReferenceEquals(OTKOldState, null))
                {
                    OldTriggerState = (float)buttonProperty.GetValue(OTKOldState.Triggers);
                }

                if (CurrentTriggerState == 1 &&
                (Object.ReferenceEquals(OTKOldState, null) || OldTriggerState == 0))
                {
                    CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Trigger Pressed");
                }
                if (CurrentTriggerState == 0 &&
                    (!Object.ReferenceEquals(OTKOldState, null) && OldTriggerState == 1))
                {
                    CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Trigger Released");
                }
            }

            var oldLeftX = Math.Round(OTKOldState.ThumbSticks.Left.X);
            var oldLeftY = Math.Round(OTKOldState.ThumbSticks.Left.Y);

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

            OTKOldState = State;
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
