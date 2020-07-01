using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.DirectInput;
using System.Linq.Expressions;
using System.Resources;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using OpenTK.Input;
using System.Text.RegularExpressions;
using XInputDotNetPure;
using System.Globalization;

namespace nullDCNetplayLauncher
{
    public partial class ControllerControl : UserControl
    {
        SharpDX.DirectInput.Joystick joystick;
        Dictionary<string, List<JoystickUpdate>> ButtonAssignments;
        string ButtonAssignmentText;
        string JoystickName;
        BackgroundWorker joystickBgWorker;
        bool AnalogSet;
        bool ZDetected;
        bool Skip;
        bool IsUnnamed;

        public bool OldEnableMapper;
        public bool SetupUnfinished;

        GamePadMapping WorkingMapping;
        Dictionary<string, string> jWorkingMapping;

        private ControllerEngine controller;
        
        private OpenTK.Input.GamePadState OldState;
        private OpenTK.Input.JoystickState jOldState;
        private XInputDotNetPure.GamePadState XOldState;
        private SharpDX.DirectInput.JoystickState DOldState;
        

        string[] directionalButtons;
        string[] faceButtons;
        string[] optionButtons;

        string[] buttonNames;
        readonly ResourceManager rm;

        string CurrentButtonAssignment;
        Boolean CurrentlyAssigned = false;

        public ControllerControl(ControllerEngine controllerEngine)
        {
            InitializeComponent();

            controller = controllerEngine;
            rm = Properties.Resources.ResourceManager;
            AnalogSet = false;
            Skip = false;
            SetupUnfinished = false;
            joystickBgWorker = new BackgroundWorker();
            InitializeJoystick();
            InitializeJoystickBgWorker();

            ButtonAssignments = new Dictionary<string, List<JoystickUpdate>>();
            directionalButtons = new string[] { "Up", "Down", "Left", "Right" };
            faceButtons = new string[] { "1", "2", "3", "4", "5", "6" };
            optionButtons = new string[] { "Start", "Coin", "Test" };

            buttonNames = directionalButtons.Concat(faceButtons).Concat(optionButtons).ToArray();

            WorkingMapping = new GamePadMapping();
            jWorkingMapping = new Dictionary<string, string>();
        }

        private void ControllerControl_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("FOO " + controller.CapabilitiesGamePad.ToString());
            
            if(controller.CapabilitiesGamePad.GamePadType.Equals(GamePadType.GamePad))
            {
                lblController.Text = "Controller Detected";
            }
            chkForceMapper.Visible = true;

            if (controller.CapabilitiesGamePad.GamePadType.Equals(GamePadType.GamePad))
            {
                lblController.Text = "Controller Detected";
            }
            chkForceMapper.Visible = true;

            // if XInput or GamePad not detected, Keyboard Mapper not activated
            if (!XInputDotNetPure.GamePad.GetState(PlayerIndex.One).IsConnected
                && !OpenTK.Input.GamePad.GetState(0).IsConnected)
            {
                chkForceMapper.Enabled = false;
            }
        }

        private void ControllerControl_Close(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("BAR " + controller.CapabilitiesGamePad.ToString());

            if (NetplayLaunchForm.EnableMapper == false)
            {
                NetplayLaunchForm.controller.clock.Stop();
            }
        }

        int ExtractNumberFromText(string text)
        {
            Match m = Regex.Match(text, @"\d*");
            int parsed;
            Int32.TryParse(m.Groups[0].Value, out parsed);
            return parsed;
        }

        private void InitializeJoystick()
        {
            if (!NetplayLaunchForm.controller.clock.IsEnabled)
            {
                NetplayLaunchForm.controller.clock.Start();
            }
            // Initialize DirectInput
            var directInput = new DirectInput();

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;
            var joystickName = "";

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                        DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid = deviceInstance.InstanceGuid;
                joystickName = deviceInstance.InstanceName;
            }

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                        DeviceEnumerationFlags.AllDevices))
                {
                    joystickGuid = deviceInstance.InstanceGuid;
                    joystickName = deviceInstance.InstanceName;
                }

            RestoreArcadeStick();

            // If Joystick not found, renamed generic gamepad for mapper to take over
            if (joystickGuid == Guid.Empty)
            {
                /*
                hideAllButtons();
                ClearArcadeStick();
                Console.WriteLine("No Controller Found");
                lblController.Text = "No Controller Found\n\nIf it is plugged in, then NullDC does not natively support your controller. To use it, you will have to enable the Keyboard Mapper to continue.\n\nSet your controls in AntiMicro to match the current keyboard mapping and minimize. Click \"Play Offline\" to test your controls.";
                showDetectControllerButton();
                showEnableGamepadMapperButtons();
                */
                var unnamedMappings = Launcher.mappings.GamePadMappings.Where(g => g.Name.StartsWith("Gamepad #")).ToList();
                List<string> gpNums = unnamedMappings.Select(m => m.Name.Replace("Gamepad #","") ).ToList();
                int maxUM = 0;
                if(gpNums.Count > 0)
                {
                    maxUM = gpNums.Max(ExtractNumberFromText);
                }

                IsUnnamed = true;
                JoystickName = $"Gamepad #{maxUM + 1}";

                btnSetup.Enabled = true;
                btnCancel.Enabled = false;

                hideAllButtons();
                showSetupButtons();
            }
            else
            {
                // Instantiate the joystick
                joystick = new SharpDX.DirectInput.Joystick(directInput, joystickGuid);
                JoystickName = joystickName;

                Console.WriteLine("Found Controller: {0}", joystickName);
                lblController.Text = $"Found Controller:\n{joystickName}";

                // Query all suported ForceFeedback effects
                var allEffects = joystick.GetEffects();
                foreach (var effectInfo in allEffects)
                {
                    Console.WriteLine("Effect available {0}", effectInfo.Name);
                    //lblController.Text = "Effect available {effectInfo.Name}";
                }

                // Set BufferSize in order to use buffered data.
                joystick.Properties.BufferSize = 128;

                // Acquire the joystick
                joystick.Acquire();

                btnSetup.Enabled = true;
                btnCancel.Enabled = false;

                hideAllButtons();
                showSetupButtons();
                chkForceMapper.Visible = true;
            }
        }

        private String JoystickUpdateToQko(SharpDX.DirectInput.JoystickUpdate update)
        {
            string offset = update.Offset.ToString();
            int value = update.Value;
            string[] ignoredInput = { "X", "Y" };
            string qkoInput = "";
            if (offset.Contains("Buttons"))
            {
                var buttonNum = Int32.Parse(offset.Replace("Buttons", "")) + 1;
                qkoInput = "button_" + buttonNum;
            }
            else if (offset.Contains("PointOfViewControllers"))
            {
                string qkoDirection = "";
                if (value == 0)
                    qkoDirection = "up";
                else if (value == 18000)
                    qkoDirection = "down";
                else if (value == 27000)
                    qkoDirection = "left";
                else if (value == 9000)
                    qkoDirection = "right";
                
                var qkoPovNum = Int32.Parse(offset.Replace("PointOfViewControllers", ""));

                qkoInput = $"hat_{qkoPovNum}_{qkoDirection}";
            } 
            else if (ignoredInput.Contains(offset))
            {
                qkoInput = null;
            }
            return qkoInput;
        }

        // special thanks to MarioBrotha's joystick input code for analog detection
        private string SetJoystickAnalog(SharpDX.DirectInput.JoystickState joystickState)
        {
            int LEFT_UP = 0;
            int RIGHT_DOWN = 65535;
            int[] directionInput = { LEFT_UP, RIGHT_DOWN };

            int Xaxis = joystickState.X;
            int Yaxis = joystickState.Y;

            // wait for valid analog input
            while (directionInput.Contains(Xaxis) || directionInput.Contains(Yaxis))
            {
                Xaxis = joystickState.X;
                Yaxis = joystickState.Y;

                if (Xaxis == LEFT_UP) // left
                {
                    return "axis_w_negative";
                }
                if (Xaxis == RIGHT_DOWN) // right
                {
                    return "axis_w_positive";
                }
                if (Yaxis == LEFT_UP) // up
                {
                    return "axis_z_negative";
                }
                if (Yaxis == RIGHT_DOWN) // down
                {
                    return "axis_z_positive";
                }
            }
            return null;
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

        public void XInputGamePadInputRoll(object sender, ActionEventArgs e)
        {
            var State = XInputDotNetPure.GamePad.GetState(PlayerIndex.One);

            var defaultXInputs = new Dictionary<string, string>()
            {
                { "X", "1"},
                { "Y", "2"},
                { "LeftTrigger", "3"},
                { "A", "4"},
                { "B", "5"},
                { "RightTrigger", "6"},
                { "Start", "Start"},
                { "LeftShoulder", "Coin"},
                { "RightShoulder", "Test"}
            };

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
                    //CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Pressed");
                    WorkingMapping[buttonProperty.Name] = CurrentButtonAssignment;
                }
                if (CurrentButtonState == XInputDotNetPure.ButtonState.Released &&
                    (!Object.ReferenceEquals(XOldState, null) && OldButtonState == XInputDotNetPure.ButtonState.Pressed))
                {
                    //CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Released");
                    CurrentlyAssigned = true;
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
                    //CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Trigger Pressed");
                    WorkingMapping[buttonProperty.Name] = CurrentButtonAssignment;
                }
                if (CurrentTriggerState == 0 &&
                    (!Object.ReferenceEquals(XOldState, null) && OldTriggerState == 1))
                {
                    //CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Trigger Released");
                    CurrentlyAssigned = true;
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
                WorkingMapping["IsRight"] = CurrentButtonAssignment;
            }

            if (oldLeftX == 1 && newLeftX == 0 || oldRight == false && newRight == true)
            {
                System.Diagnostics.Debug.WriteLine("Right Released");
                CurrentlyAssigned = true;
            }

            if (oldLeftX == 0 && newLeftX == -1 || oldLeft == true && newLeft == false)
            {
                System.Diagnostics.Debug.WriteLine("Left Pushed");
                WorkingMapping["IsLeft"] = CurrentButtonAssignment;
            }

            if (oldLeftX == -1 && newLeftX == 0 || oldLeft == false && newLeft == true)
            {
                System.Diagnostics.Debug.WriteLine("Left Released");
                CurrentlyAssigned = true;
            }

            if (oldLeftY == 0 && newLeftY == 1 || oldUp == true && newUp == false)
            {
                System.Diagnostics.Debug.WriteLine("Up Pushed");
                WorkingMapping["IsUp"] = CurrentButtonAssignment;
            }

            if (oldLeftY == 1 && newLeftY == 0 || oldUp == false && newUp == true)
            {
                System.Diagnostics.Debug.WriteLine("Up Released");
                //CallButtonMapping("IsUp", false);
                WorkingMapping["IsUp"] = CurrentButtonAssignment;
                CurrentlyAssigned = true;
            }

            if (oldLeftY == 0 && newLeftY == -1 || oldDown == true && newDown == false)
            {
                System.Diagnostics.Debug.WriteLine("Down Pushed");
                WorkingMapping["IsDown"] = CurrentButtonAssignment;
            }

            if (oldLeftY == -1 && newLeftY == 0 || oldDown == false && newDown == true)
            {
                System.Diagnostics.Debug.WriteLine("Down Released");
                CurrentlyAssigned = true;
            }

            XOldState = State;
        }

        private void controller_GamePadAction(object sender, ActionEventArgs e)
        {
            if (OpenTK.Input.GamePad.GetState(0).IsConnected)
            {
                OpenTKGamePadInputRoll(sender, e);
            }
            else if (XInputDotNetPure.GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                ZDetected = true;
                XInputGamePadInputRoll(sender, e);
            }
            else
            {
                // DirectInput fallback for working PS3 and triggerless 
                // controllers not picked up by XInput or OpenTK
                DInputRoll(sender, e);
            }
                
        }

        public string CapitalizeFirstLetter(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;
            if (s.Length == 1)
                return s.ToUpper();
            return s.Remove(1).ToUpper() + s.Substring(1);
        }

        private void DInputRoll(object sender, ActionEventArgs e)
        {
            var State = new SharpDX.DirectInput.JoystickState();
            joystick.GetCurrentState(ref State);

            // according to directinputhid mapping
            // https://docs.microsoft.com/en-us/windows/win32/xinput/directinput-and-xusb-devices
            var defaultDInputButtons = new Dictionary<int, string>()
            {
                { 1, "A"},
                { 2, "B"},
                { 3, "X"},
                { 4, "Y"},
                { 5, "LeftShoulder"},
                { 6, "RightShoulder"},
                { 7, "Back"},
                { 8, "Start"},
            };

            string assign;
            if (int.TryParse(CurrentButtonAssignment, out _))
            {
                assign = $"Button_{CurrentButtonAssignment}";
            }
            else
            {
                assign = CurrentButtonAssignment;
            }

            
            for (int i = 0; i < State.Buttons.Length; i++)
            {
                if (DOldState.Buttons[i] != State.Buttons[i])
                {
                    //WorkingMapping[defaultDInputButtons[i + 1]] = CurrentButtonAssignment;
                    jWorkingMapping[assign] = $"button_{i + 1}";
                    CurrentlyAssigned = true;
                }
            }

            for (int hatNum = 0; hatNum < State.PointOfViewControllers.Length; hatNum++)
            {
                if (DOldState.PointOfViewControllers[hatNum] != State.PointOfViewControllers[hatNum])
                {
                    var value = State.PointOfViewControllers[hatNum];

                    string qkoDirection = "";
                    switch (value)
                    {
                        case 0:
                            qkoDirection = "up";
                            break;
                        case 18000:
                            qkoDirection = "down";
                            break;
                        case 27000:
                            qkoDirection = "left";
                            break;
                        case 9000:
                            qkoDirection = "right";
                            break;
                    }

                    if(!string.IsNullOrEmpty(qkoDirection))
                    {
                        //var field = $"Is{CapitalizeFirstLetter(qkoDirection)}";
                        //WorkingMapping[field] = CapitalizeFirstLetter(qkoDirection);
                        jWorkingMapping[assign] = $"hat_{hatNum}_{qkoDirection}";
                        System.Diagnostics.Debug.WriteLine(jWorkingMapping[assign]);
                        CurrentlyAssigned = true;
                    }
                    
                }
                
            }
            
            if(AnalogSet && !string.IsNullOrEmpty(SetJoystickAnalog(State)))
            {
                jWorkingMapping[assign] = SetJoystickAnalog(State);
                System.Diagnostics.Debug.WriteLine(jWorkingMapping[assign]);
                CurrentlyAssigned = true;
            }

            DOldState = State;
        }


        private void OpenTKGamePadInputRoll(object sender, ActionEventArgs e)
        {
            var State = e.GamePadState;
            var Capabilities = controller.CapabilitiesGamePad;

            var jState = e.JoystickState;
            var jCapabilities = controller.CapabilitiesJoystick;

            PropertyInfo[] AvailableButtonProperties = typeof(OpenTK.Input.GamePadButtons).GetProperties().Where(
                p =>
                {
                    return p.Name != "IsAnyButtonPressed";
                }).ToArray();

            PropertyInfo[] AvailableDPadProperties = typeof(OpenTK.Input.GamePadDPad).GetProperties().Where(
                p =>
                {
                    return p.PropertyType == typeof(Boolean);
                }).ToArray();

            for (int i = 0; i < jCapabilities.ButtonCount; i++)
            {
                string assign;
                if (int.TryParse(CurrentButtonAssignment, out _))
                {
                    assign = $"Button_{CurrentButtonAssignment}";
                }
                else
                {
                    assign = CurrentButtonAssignment;
                }
                if (jOldState.GetButton(i) != jState.GetButton(i))
                {
                    jWorkingMapping[assign] = $"button_{i + 1}";
                }
            }

            if (!jOldState.GetHat(JoystickHat.Hat0).Equals(jState.GetHat(JoystickHat.Hat0)))
            {
                string assign;
                if (int.TryParse(CurrentButtonAssignment, out _))
                {
                    assign = $"Button_{CurrentButtonAssignment}";
                }
                else
                {
                    assign = CurrentButtonAssignment;
                }
                switch (jState.GetHat(JoystickHat.Hat0).Position)
                {
                    case HatPosition.Up:
                        jWorkingMapping[assign] = "hat_0_up";
                        break;
                    case HatPosition.Down:
                        jWorkingMapping[assign] = "hat_0_down";
                        break;
                    case HatPosition.Left:
                        jWorkingMapping[assign] = "hat_0_left";
                        break;
                    case HatPosition.Right:
                        jWorkingMapping[assign] = "hat_0_right";
                        break;
                }
            }

            foreach (PropertyInfo buttonProperty in AvailableButtonProperties)
            {

                // GamePad logic
                var CurrentButtonState = (OpenTK.Input.ButtonState)buttonProperty.GetValue(State.Buttons);
                var OldButtonState = (OpenTK.Input.ButtonState)buttonProperty.GetValue(State.Buttons);
                if (!Object.ReferenceEquals(OldState, null))
                {
                    OldButtonState = (OpenTK.Input.ButtonState)buttonProperty.GetValue(OldState.Buttons);
                }

                if (CurrentButtonState == OpenTK.Input.ButtonState.Pressed &&
                (Object.ReferenceEquals(OldState, null) || OldButtonState == OpenTK.Input.ButtonState.Released))
                {
                    //CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{CurrentButtonAssignment}: {buttonProperty.Name} Pressed");
                    WorkingMapping[buttonProperty.Name] = CurrentButtonAssignment;
                }
                if (CurrentButtonState == OpenTK.Input.ButtonState.Released &&
                    (!Object.ReferenceEquals(OldState, null) && OldButtonState == OpenTK.Input.ButtonState.Pressed))
                {
                    //CallButtonMapping(buttonProperty.Name, false);
                   // System.Diagnostics.Debug.WriteLine($"{buttonProperty.Name} Released");
                    System.Diagnostics.Debug.WriteLine($"{CurrentButtonAssignment}: {buttonProperty.Name} Released");
                    CurrentlyAssigned = true;
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
                    //CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{CurrentButtonAssignment}: {buttonProperty.Name} Pressed");
                    WorkingMapping[buttonProperty.Name] = CurrentButtonAssignment;
                    CurrentlyAssigned = true;
                }
                if (CurrentDPadState == false &&
                    (!Object.ReferenceEquals(OldState, null) && OldDPadState == true))
                {
                    //CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{CurrentButtonAssignment}: {buttonProperty.Name} Released");
                    CurrentlyAssigned = true;
                }
            }

            PropertyInfo[] AvailableTriggerProperties = typeof(OpenTK.Input.GamePadTriggers).GetProperties().ToArray();

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
                    //CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{CurrentButtonAssignment} Trigger: {buttonProperty.Name} Pressed");
                    WorkingMapping[buttonProperty.Name] = CurrentButtonAssignment;
                }
                if (CurrentTriggerState == 0 &&
                    (!Object.ReferenceEquals(OldState, null) && OldTriggerState == 1))
                {
                    //CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{CurrentButtonAssignment} Trigger: {buttonProperty.Name} Released");
                    CurrentlyAssigned = true;
                }
            }

            var oldLeftX = Math.Round(OldState.ThumbSticks.Left.X);
            var oldLeftY = Math.Round(OldState.ThumbSticks.Left.Y);

            var newLeftX = Math.Round(State.ThumbSticks.Left.X);
            var newLeftY = Math.Round(State.ThumbSticks.Left.Y);

            if (oldLeftX == 0 && newLeftX == 1)
            {
                System.Diagnostics.Debug.WriteLine("Right Pushed");
                WorkingMapping["IsRight"] = CurrentButtonAssignment;
                if (AnalogSet)
                {
                    jWorkingMapping["Right"] = "axis_w_positive";
                }
            }

            if (oldLeftX == 1 && newLeftX == 0)
            {
                System.Diagnostics.Debug.WriteLine("Right Released");
                CurrentlyAssigned = true;
            }

            if (oldLeftX == 0 && newLeftX == -1)
            {
                System.Diagnostics.Debug.WriteLine("Left Pushed");
                WorkingMapping["IsLeft"] = CurrentButtonAssignment;
                if (AnalogSet)
                {
                    jWorkingMapping["Left"] = "axis_w_negative";
                }
            }

            if (oldLeftX == -1 && newLeftX == 0)
            {
                System.Diagnostics.Debug.WriteLine("Left Released");
                CurrentlyAssigned = true;
            }

            if (oldLeftY == 0 && newLeftY == 1)
            {
                System.Diagnostics.Debug.WriteLine("Up Pushed");
                WorkingMapping["IsUp"] = CurrentButtonAssignment;
                if (AnalogSet)
                {
                    jWorkingMapping["Up"] = "axis_z_negative";
                }
            }

            if (oldLeftY == 1 && newLeftY == 0)
            {
                System.Diagnostics.Debug.WriteLine("Up Released");
                CurrentlyAssigned = true;
            }

            if (oldLeftY == 0 && newLeftY == -1)
            {
                System.Diagnostics.Debug.WriteLine("Down Pushed");
                WorkingMapping["IsDown"] = CurrentButtonAssignment;
                if (AnalogSet)
                {
                    jWorkingMapping["Down"] = "axis_z_positive";
                }
            }

            if (oldLeftY == -1 && newLeftY == 0)
            {
                System.Diagnostics.Debug.WriteLine("Down Released");
                CurrentlyAssigned = true;
            }

            OldState = State;
            jOldState = jState;
        }

        private List<JoystickUpdate> SetJoystickButton(SharpDX.DirectInput.Joystick joystick, string button)
        {
            List<JoystickUpdate> ButtonPressRelease = new List<JoystickUpdate>();

            // Poll events from joystick
            while (ButtonPressRelease.Count < 2)
            {
                if (Skip)
                {
                    Skip = false;
                    break;
                }
                else
                {
                    joystick.Poll();
                }
                var datas = joystick.GetBufferedData();
                foreach (var state in datas)
                {
                    if (state.Offset == JoystickOffset.Z && !AnalogSet)
                    {
                        ZDetected = true;
                        return null;
                    }
                    var acceptedState = state.Offset != JoystickOffset.X && state.Offset != JoystickOffset.Y && state.Offset != JoystickOffset.Z && state.Offset != JoystickOffset.RotationZ;
                    if (acceptedState)
                    {
                        ButtonPressRelease.Add(state);
                        Console.WriteLine(state);
                    }
                }
            }
            Console.WriteLine(ButtonPressRelease);
            return ButtonPressRelease;
        }

        private void InitializeJoystickBgWorker()
        {
            joystickBgWorker.DoWork += new DoWorkEventHandler(joystickBgWorker_DoWork);
            joystickBgWorker.ProgressChanged += joystickBgWorker_ProgressChanged;
            joystickBgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(joystickBgWorker_RunWorkerCompleted);
            joystickBgWorker.WorkerReportsProgress = true;
            joystickBgWorker.WorkerSupportsCancellation = true;
        }

        private void joystickBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // ui update
            string launcherText = File.ReadAllText(Launcher.rootDir + "launcher.cfg");
            string[] cfgLines = File.ReadAllLines(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
            string cfgText = File.ReadAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
            var player1_old = cfgLines.Where(s => s.Contains("player1=")).ToList().First();

            string successText;

            // only writes qkoJAMMA joystick configuration if 11 buttons minimum are assigned
            //  && ButtonAssignments.Count >= 11
            if (!ZDetected && !IsUnnamed && jWorkingMapping.Count >= 11)
            {
                string[] qkoFields = { "Start", "Test", "Up", "Down", "Left", "Right",
                                        "Button_1", "Button_2", "Button_3", "Button_4",
                                        "Button_5", "Button_6", "Coin"};

                ButtonAssignmentText = "";
                foreach (string field in qkoFields)
                {
                    if(jWorkingMapping.ContainsKey(field))
                    {
                        ButtonAssignmentText += $"{jWorkingMapping[field]}={field}\n";
                    }
                    else
                    {
                        ButtonAssignmentText += $"none={field}\n";
                    }
                }

                NetplayLaunchForm.EnableMapper = false;
                launcherText = launcherText.Replace("enable_mapper=1", "enable_mapper=0");
                launcherText = launcherText.Replace(player1_old, "player1=joy1");
                cfgText = cfgText.Replace(player1_old, "player1=joy1");

                var qjcPath = Launcher.rootDir + "nulldc-1-0-4-en-win//qkoJAMMA//" + JoystickName + ".qjc";
                if (File.Exists(qjcPath))
                {
                    File.SetAttributes(qjcPath, FileAttributes.Normal);
                }
                File.WriteAllText(qjcPath, ButtonAssignmentText);
                // prevent qkoJAMMA from changing controls on skipped face buttons or coin
                // skipped controls work fine up until the moment you exit the nulldc first time
                File.SetAttributes(qjcPath, FileAttributes.ReadOnly);

                successText = $"\nNew qkoJAMMA Profile \"{JoystickName}\" Created\n\nExit any old instances of NullDC and \nclick \"Play Offline\" to test your controls.";
            }
            else
            {
                SaveMapping(JoystickName);

                NetplayLaunchForm.EnableMapper = true;
                launcherText = launcherText.Replace("enable_mapper=0", "enable_mapper=1");
                launcherText = launcherText.Replace(player1_old, "player1=keyboard");

                cfgText = cfgText.Replace(player1_old, "player1=keyboard");

                successText = $"\nNew Keyboard Mapper Profile \"{JoystickName}\" Created\n\nExit any old instances of NullDC and \nclick \"Play Offline\" to test your controls.";
            }

            picArcadeStick.Image = global::nullDCNetplayLauncher.Properties.Resources.base_full;
            lblController.Size = new System.Drawing.Size(286, 114);
            lblController.Text = successText;

            hideAllButtons();
            btnCancel.Enabled = false;
            btnSetup.Enabled = true;

            File.WriteAllText(Launcher.rootDir + "launcher.cfg", launcherText);
            File.WriteAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg", cfgText);

            SetupUnfinished = false;
            controller.GamePadAction -= controller_GamePadAction;
        }

        public void TestButtonMode()
        {
            picArcadeStick.Image = global::nullDCNetplayLauncher.Properties.Resources.base_full;
            
        }

        private void picArcadeStick_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            DrawInput("up", e);
            DrawInput("down", e);
            DrawInput("left", e);
            DrawInput("right", e);
        }

        private void DrawInput(string input, System.Windows.Forms.PaintEventArgs e)
        {
            int x = 0;
            int y = 0;
            if(input == "1")
            {
                x = 112;
                y = 28;
            }
            else if(input == "2")
            {
                x = 169;
                y = 4;
            }
            else if (input == "3")
            {
                x = 230;
                y = 14;
            }
            else if (input == "4")
            {
                x = 100;
                y = 93;
            }
            else if (input == "5")
            {
                x = 158;
                y = 70;
            }
            else if (input == "6")
            {
                x = 219;
                y = 80;
            }
            else if (input == "up")
            {
                x = 22;
                y = 60;
            }
            else if (input == "down")
            {
                x = 22;
                y = 90;
            }
            else if (input == "left")
            {
                x = 10;
                y = 75;
            }
            else if (input == "right")
            {
                x = 35;
                y = 75;
            }

            int width = 45;
            int height = 45;

            if (input == "up"
                || input == "down"
                || input == "left"
                || input == "right")
            {
                width = 35;
                height = 35;
            }
            

            SolidBrush b = new SolidBrush(Color.Red);
            Graphics g = e.Graphics;
            g.FillEllipse(b, x, y, width, height);
        }

        private bool IsDirection(string button)
        {
            return (bool) directionalButtons.Contains(button);
        }

        private void joystickBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            var button_index = 0;
            if(!chkForceMapper.Checked)
                ZDetected = false;
            if (XInputDotNetPure.GamePad.GetState(PlayerIndex.One).IsConnected)
                ZDetected = true;
            foreach (string button in buttonNames)
            {
                worker.ReportProgress(button_index);
                CurrentButtonAssignment = button;
                CurrentlyAssigned = false;
                Skip = false;

                ActionEventArgs args = new ActionEventArgs(controller.ActiveDevice);
                OldState = args.GamePadState;
                jOldState = args.JoystickState;
                if (joystick != null)
                    DOldState = joystick.GetCurrentState();

                if (!IsUnnamed)
                {
                    var isNumeric = int.TryParse(button, out _);
                    var buttonName = button;

                    if (isNumeric)
                        buttonName = "Button_" + button;
                }

                if (!IsUnnamed || AnalogSet)
                {
                    if (directionalButtons.Any(button.Contains))
                    {
                        Thread.Sleep(700);
                    }
                }
                
                while (CurrentlyAssigned == false && Skip == false)
                {
                    Thread.Sleep(700);
                }
                button_index++;
            }
        }

        private void ClearArcadeStick()
        {
            picArcadeStick.Visible = false;
            lblController.Location = new System.Drawing.Point(22, 10);
            lblController.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblController.Size = new System.Drawing.Size(286, 253);
        }

        private void RestoreArcadeStick()
        {
            picArcadeStick.Visible = true;
            lblController.Location = new System.Drawing.Point(22, 188);
            lblController.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblController.Size = new System.Drawing.Size(286, 75);
        }

        private void joystickBgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var button = buttonNames[e.ProgressPercentage];

            picArcadeStick.Image = (Bitmap)rm.GetObject(buttonNames[e.ProgressPercentage].ToString().ToLower());
            if (directionalButtons.Any(button.Contains))
            {
                Console.WriteLine($"Press and release {button}.");
                lblController.Text = $"Press and release {button}.";
            }
            else if (faceButtons.Any(button.Contains))
            {
                Console.WriteLine($"Press and release button {button}.");
                lblController.Text = $"Press and release button {button}.";
            }
            else
            {
                Console.WriteLine($"Press and release the {button} button.");
                lblController.Text = $"Press and release the {button} button.";
            }
        }

        private void BeginSetup()
        {
            controller.GamePadAction += controller_GamePadAction;
            // disable gamepad mapper if enabled
            /*
            if (NetplayLaunchForm.EnableMapper)
            {
                NetplayLaunchForm.EnableMapper = false;
                NetplayLaunchForm.controller.clock.Stop();
            }
            */

            jWorkingMapping = new Dictionary<string, string>();

            OldEnableMapper = NetplayLaunchForm.EnableMapper;
            SetupUnfinished = true;

            showSetupButtons();
            btnSkip.Enabled = true;
            btnCancel.Enabled = true;
            btnSetup.Enabled = false;
            joystickBgWorker.WorkerSupportsCancellation = true;
            if (!joystickBgWorker.IsBusy)
                joystickBgWorker.RunWorkerAsync();

        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            //DrawButton("1", 120, 120);
            /*
            NetplayLaunchForm.EnableMapper = false;
            // reset button assignments on click
            ButtonAssignments = new Dictionary<string, List<JoystickUpdate>>();
            ButtonAssignmentText = "";
            hideAllButtons();
            showDPadOrAnalogScreen();
            */
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            controller.GamePadAction -= controller_GamePadAction;
            SetupUnfinished = true;
            ((Form)this.TopLevelControl).Close();
        }

        private void showDPadOrAnalogScreen()
        {
            btnDPad.Visible = true;
            btnAnalog.Visible = true;

            lblController.Text = "Digital or Analog Input?";
        }

        private void showSetupButtons()
        {
            btnSetup.Visible = true;
            btnSkip.Visible = true;
            btnCancel.Visible = true;
        }

        private void hideAllButtons()
        {
            btnSetup.Visible = false;
            btnSkip.Visible = false;
            btnCancel.Visible = false;
            btnDPad.Visible = false;
            btnAnalog.Visible = false;
            chkForceMapper.Visible = false;
        }

        private void btnDetectController_Click(object sender, EventArgs e)
        {
            InitializeJoystick();
        }

        private void btnShowKeyboard_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", Launcher.rootDir + "nulldc-1-0-4-en-win\\qkoJAMMA\\keyboard.qkc");
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            Skip = true;
        }

        private void btnDPad_Click(object sender, EventArgs e)
        {
            AnalogSet = false;
            hideAllButtons();
            BeginSetup();
        }

        private void btnAnalog_Click(object sender, EventArgs e)
        {
            if(!IsUnnamed)
                AnalogSet = true;
            hideAllButtons();
            BeginSetup();
        }

        private void btnEnableGamepadMapper_Click(object sender, EventArgs e)
        {
            ZDetected = true;
        }

        public void SaveMapping(string mappingName)
        {
            try
            {
                string launcherText = File.ReadAllText(Launcher.rootDir + "launcher.cfg");
                string[] cfgLines = File.ReadAllLines(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
                string cfgText = File.ReadAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
                var player1_old = cfgLines.Where(s => s.Contains("player1=")).ToList().First();
                launcherText = launcherText.Replace("enable_mapper=0", "enable_mapper=1");
                cfgText = cfgText.Replace(player1_old, "player1=keyboard");

                File.WriteAllText(Launcher.rootDir + "launcher.cfg", launcherText);
                File.WriteAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg", cfgText);
            }
            catch { }

            foreach (GamePadMapping mapping in Launcher.mappings.GamePadMappings)
            {
                mapping.Default = false;
            }

            var toEdit = Launcher.mappings.GamePadMappings.FirstOrDefault(p => p.Name == mappingName);
            if (toEdit != null)
            {
                toEdit = WorkingMapping;
                toEdit.Name = mappingName;

                string[] buttons = { "Y", "A", "Back", "X", "B", "Start", "BigButton", "LeftStick", "RightStick",
                                     "LeftShoulder", "RightShoulder", "LeftTrigger", "RightTrigger",
                                     "IsUp", "IsDown", "IsLeft", "IsRight" };

                foreach (string button in buttons)
                {
                    toEdit[button] = WorkingMapping[button];
                }

                toEdit.Default = true;

                GamePadMapping toReplace = Launcher.mappings.GamePadMappings.Where(g => g.Name.Equals(mappingName)).ToList().First();
                Launcher.mappings.GamePadMappings.Remove(toReplace);
                Launcher.mappings.GamePadMappings.Add(toEdit);
            }
            else
            {
                var toAdd = WorkingMapping;
                toAdd.Name = mappingName;
                toAdd.Default = true;
                Launcher.ActiveGamePadMapping = toAdd;
                Launcher.mappings.GamePadMappings.Add(toAdd);
            }

            var path = Launcher.rootDir + "GamePadMappingList.xml";
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(GamePadMappingList));
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer.BaseStream, Launcher.mappings);
            writer.Close();
        }

        private void chkForceMapper_CheckedChanged(object sender, EventArgs e)
        {
            if (chkForceMapper.Checked)
            {
                ZDetected = true;
            }
        }

    }
}
