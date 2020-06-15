using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;

namespace nullDCNetplayLauncher
{
    public class GamePadMapper
    {
        private ControllerEngine controller;
        private Dictionary<string, string> KeyboardQkcMapping;
        private GamePadState OldState;

        public GamePadMapper(ControllerEngine controllerEngine)
        {
            controller = controllerEngine;
            KeyboardQkcMapping = ReadKeyboardQkc();
            System.Diagnostics.Debug.WriteLine(controller.CapabilitiesGamePad.ToString());
            //hWindow = Launcher.NullDCWindowHandle();
        }

        public void initializeController(Object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(controller.CapabilitiesGamePad.ToString());
            controller.GamePadAction += controller_GamePadAction;
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

        private void controller_GamePadAction(object sender, ActionEventArgs e)
        {   
            var State = e.GamePadState;
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
                    //System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Pressed");
                }
                if (CurrentButtonState == ButtonState.Released &&
                    (!Object.ReferenceEquals(OldState, null) && OldButtonState == ButtonState.Pressed))
                {
                    CallButtonMapping(buttonProperty.Name, false);
                    //System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Released");
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
                    //System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Pressed");
                }
                if (CurrentDPadState == false &&
                    (!Object.ReferenceEquals(OldState, null) && OldDPadState == true))
                {
                    CallButtonMapping(buttonProperty.Name, false);
                    //System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Released");
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
                    //System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Trigger Pressed");
                }
                if (CurrentTriggerState == 0 &&
                    (!Object.ReferenceEquals(OldState, null) && OldTriggerState == 1))
                {
                    CallButtonMapping(buttonProperty.Name, false);
                    //System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Trigger Released");
                }
            }

            OldState = State;
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

            Dictionary<string, byte> ButtonMapping = new Dictionary<string, byte>()
            {
                { "Y", KEY_8 },
                { "A", KEY_9 },
                { "Back", KEY_0 },
                { "X", KEY_U },
                { "B", KEY_I },
                { "Start", KEY_O },
                { "RightStick", KEY_5 },
                { "LeftShoulder", KEY_1 },
                { "RightShoulder", KEY_3 },
                { "IsUp", KEY_W },
                { "IsDown", KEY_S },
                { "IsLeft", KEY_A },
                { "IsRight", KEY_D },
            };

            if(push)
            {
                PushKey(ButtonMapping[button]);
            }
            else
            {
                ReleaseKey(ButtonMapping[button]);
            }
        }

        public void PushKey(byte key)
        {
            Process[] processes = Process.GetProcessesByName("notepad");
            if (processes.Length > 0)
            {
                Process ndc = processes[0];
                IntPtr ptr = ndc.MainWindowHandle;

                SetFocus(ptr);

                keybd_event(key, 0, 0, 0);
            }
        }

        public void ReleaseKey(byte key)
        {
            Process[] processes = Process.GetProcessesByName("notepad");
            if (processes.Length > 0)
            {
                Process ndc = processes[0];
                IntPtr ptr = ndc.MainWindowHandle;

                SetFocus(ptr);

                const uint KEYEVENTF_KEYUP = 0x0002;
                keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
            }
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

    public class ControllerEngine
    {
        private DispatcherTimer clock;
        private GamePadState oldgstate;

        // Event Declaration
        // GamePadAction: Triggered when the oldgstate is different from the current GamePadState
        public event EventHandler<ActionEventArgs> GamePadAction;

        private int clockspeed = 16;

        public ControllerEngine()
        {
            this.ActiveDevice = 0;
            oldgstate = GamePad.GetState(this.ActiveDevice);
            createNewTimer();
        }
        private void createNewTimer()
        {
            clock = new DispatcherTimer();
            clock.Tick += new EventHandler(checkGamePads);
            clock.Interval = new TimeSpan(0, 0, 0, 0, this.clockspeed);
            clock.Start();
        }
        public int ActiveDevice { get; set; }

        public GamePadCapabilities CapabilitiesGamePad { get { return GamePad.GetCapabilities(this.ActiveDevice); } }

        protected virtual void OnGamePadAction(ActionEventArgs e)
        {
            EventHandler<ActionEventArgs> handler = GamePadAction;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void checkGamePads(Object sender, EventArgs e)
        {
            ActionEventArgs args = new ActionEventArgs(this.ActiveDevice);
            if (!args.GamePadState.Equals(oldgstate))
            {
                //call event
                OnGamePadAction(args);
                oldgstate = args.GamePadState;
            }
            if (!args.GamePadState.Triggers.Equals(oldgstate.Triggers))
            {
                //call event
                OnGamePadAction(args);
                oldgstate = args.GamePadState;
            }

        }
    }

    public class ActionEventArgs : EventArgs
    {
        public ActionEventArgs()
        {
            this.Instance = 0;
        }

        public ActionEventArgs(int ControllerInstance)
        {
            this.Instance = ControllerInstance;
        }

        public int Instance { get; set; }

        public GamePadState GamePadState { get { return GamePad.GetState(this.Instance); } }

    }
}
