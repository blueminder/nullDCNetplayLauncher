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


        private ControllerEngine controller;
        private GamePadState OldState;

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
            joystickBgWorker = new BackgroundWorker();
            InitializeJoystick();
            InitializeJoystickBgWorker();

            ButtonAssignments = new Dictionary<string, List<JoystickUpdate>>();
            directionalButtons = new string[] { "Up", "Down", "Left", "Right" };
            faceButtons = new string[] { "1", "2", "3", "4", "5", "6" };
            optionButtons = new string[] { "Start", "Coin", "Test" };

            buttonNames = directionalButtons.Concat(faceButtons).Concat(optionButtons).ToArray();
        }

        private void ControllerControl_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("FOO " + controller.CapabilitiesGamePad.ToString());
            controller.GamePadAction += controller_GamePadAction;
        }

        private void ControllerControl_Close(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("BAR " + controller.CapabilitiesGamePad.ToString());

            controller.GamePadAction -= controller_GamePadAction;
            //controller.Dispose();
        }

        private void InitializeJoystick()
        {
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

            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                hideAllButtons();
                ClearArcadeStick();
                Console.WriteLine("No Controller Found");
                lblController.Text = "No Controller Found\n\nIf it is plugged in, then NullDC does not natively support your controller. To use it, you will have to enable the Gamepad Mapper to continue.\n\nSet your controls in AntiMicro to match the current keyboard mapping and minimize. Click \"Play Offline\" to test your controls.";
                showDetectControllerButton();
                showEnableGamepadMapperButtons();
            }
            else
            {
                RestoreArcadeStick();

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
                btnSkip.Enabled = false;
                btnCancel.Enabled = false;

                hideAllButtons();
                showSetupButtons();
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
        private string SetJoystickAnalog(SharpDX.DirectInput.Joystick joystick)
        {
            int LEFT_UP = 0;
            int RIGHT_DOWN = 65535;
            int[] directionInput = { LEFT_UP, RIGHT_DOWN };

            int Xaxis = joystick.GetCurrentState().X;
            int Yaxis = joystick.GetCurrentState().Y;

            // wait for valid analog input
            while (directionInput.Contains(Xaxis) || directionInput.Contains(Yaxis))
            {
                Xaxis = joystick.GetCurrentState().X;
                Yaxis = joystick.GetCurrentState().Y;

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

        private void controller_GamePadAction(object sender, ActionEventArgs e)
        {
            var State = e.GamePadState;
            var Capabilities = controller.CapabilitiesGamePad;

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

            foreach (PropertyInfo buttonProperty in AvailableButtonProperties)
            {
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
                    Launcher.ButtonMapping[buttonProperty.Name] = CurrentButtonAssignment;
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
                    Launcher.ButtonMapping[buttonProperty.Name] = CurrentButtonAssignment;
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
                    //CallButtonMapping(buttonProperty.Name, true);
                    System.Diagnostics.Debug.WriteLine($"{CurrentButtonAssignment} Trigger: {buttonProperty.Name} Pressed");
                    Launcher.ButtonMapping[buttonProperty.Name] = CurrentButtonAssignment;
                }
                if (CurrentTriggerState == 0 &&
                    (!Object.ReferenceEquals(OldState, null) && OldTriggerState == 1))
                {
                    //CallButtonMapping(buttonProperty.Name, false);
                    System.Diagnostics.Debug.WriteLine($"{CurrentButtonAssignment} Trigger: {buttonProperty.Name} Released");
                    CurrentlyAssigned = true;
                }
            }

            OldState = State;
        }

        private List<JoystickUpdate> SetJoystickButton(SharpDX.DirectInput.Joystick joystick, string button)
        {
            //var ButtonAssignments = new Dictionary<string, JoystickUpdate[]>;

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
            //ui update
            string launcherText = File.ReadAllText(Launcher.rootDir + "launcher.cfg");
            string[] cfgLines = File.ReadAllLines(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
            string cfgText = File.ReadAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
            var player1_old = cfgLines.Where(s => s.Contains("player1=")).ToList().First();
            if (ZDetected)
            {
                launcherText = launcherText.Replace("launch_antimicro=0", "launch_antimicro=1");
                cfgText = cfgText.Replace(player1_old, "player1=keyboard");
                Launcher.LaunchAntiMicro(hidden: true);

                ClearArcadeStick();
                lblController.Text = "Z-Axis Trigger Detected. Gamepad Mapper Enabled.\n\nSet your controls in AntiMicro to match the current keyboard mapping and minimize.\n\nClick \"Play Offline\" to test your controls.";
                hideAllButtons();
                showLaunchAntimicroButtons();
            }
            else
            {
                launcherText = launcherText.Replace("launch_antimicro=1", "launch_antimicro=0");
                cfgText = cfgText.Replace(player1_old, "player1=joy1");
                Launcher.KillAntiMicro();

                File.WriteAllText(Launcher.rootDir + "nulldc-1-0-4-en-win//qkoJAMMA//" + JoystickName + ".qjc", ButtonAssignmentText);

                picArcadeStick.Image = global::nullDCNetplayLauncher.Properties.Resources.base_full;
                lblController.Size = new System.Drawing.Size(286, 114);
                lblController.Text = $"\nNew qkoJAMMA Profile \"{JoystickName}\" Created\n\nExit any old instances of NullDC and \nclick \"Play Offline\" to test your controls.";
                hideAllButtons();
                btnSkip.Enabled = false;
                btnCancel.Enabled = false;
                btnSetup.Enabled = true;
            }
            File.WriteAllText(Launcher.rootDir + "launcher.cfg", launcherText);
            File.WriteAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg", cfgText);
        }

        private bool IsDirection(string button)
        {
            return (bool) directionalButtons.Contains(button);
        }

        private void joystickBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            var button_index = 0;
            ZDetected = false;
            foreach (string button in buttonNames)
            {
                worker.ReportProgress(button_index);
                CurrentButtonAssignment = button;
                CurrentlyAssigned = false;

                ActionEventArgs args = new ActionEventArgs(controller.ActiveDevice);
                OldState = args.GamePadState;

                while (CurrentlyAssigned == false)
                {
                    Thread.Sleep(600);
                }

                    /*
                    var isNumeric = int.TryParse(button, out _);
                    var buttonName = button;

                    if (isNumeric)
                        buttonName = "Button_" + button;

                    if (IsDirection(button) && AnalogSet)
                    {
                        String analogAssignment = null;
                        while (analogAssignment is null)
                        {
                            analogAssignment = SetJoystickAnalog(joystick);
                        }
                        ButtonAssignmentText += $"{analogAssignment}={buttonName}\n";
                    }
                    else
                    {
                        var joystickUpdate = SetJoystickButton(joystick, button);

                        //AssignGamePadButton(button);

                        if (ZDetected)
                        {
                            break;
                        }

                        if (joystickUpdate.Count > 0)
                        {
                            var buttonAssignment = JoystickUpdateToQko(joystickUpdate[0]);
                            while (buttonAssignment == null)
                            {
                                buttonAssignment = JoystickUpdateToQko(joystickUpdate[0]);
                            }
                            ButtonAssignmentText += $"{buttonAssignment}={buttonName}\n";
                            ButtonAssignments.Add(button, joystickUpdate);
                        }
                        else
                        {
                            ButtonAssignmentText += $"none={buttonName}\n";
                            ButtonAssignments.Add(button, joystickUpdate);
                        }
                    }
                    */
                    button_index++;
                /*
                if (directionalButtons.Any(button.Contains) && AnalogSet)
                {
                    Thread.Sleep(500);
                }
                */
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
            // reset button assignments on click
            ButtonAssignments = new Dictionary<string, List<JoystickUpdate>>();
            ButtonAssignmentText = "";
            hideAllButtons();
            showDPadOrAnalogScreen();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((Form)this.TopLevelControl).Close();
        }

        private void showDPadOrAnalogScreen()
        {
            btnDPad.Visible = true;
            btnAnalog.Visible = true;

            lblController.Text = "Digital or Analog Input?";
        }

        private void showLaunchAntimicroButtons()
        {
            btnLaunchAntimicro.Visible = true;
            btnShowKeyboard.Visible = true;
        }

        private void showEnableGamepadMapperButtons()
        {
            btnEnableGamepadMapper.Visible = true;
            btnShowKeyboard.Visible = true;
        }

        private void showDetectControllerButton()
        {
            btnDetectController.Visible = true;
        }

        private void showSetupButtons()
        {
            btnSetup.Visible = true;
            btnSkip.Visible = true;
            btnCancel.Visible = true;
        }

        private void hideAllButtons()
        {
            btnLaunchAntimicro.Visible = false;
            btnShowKeyboard.Visible = false;
            btnDetectController.Visible = false;
            btnSetup.Visible = false;
            btnSkip.Visible = false;
            btnCancel.Visible = false;
            btnDPad.Visible = false;
            btnAnalog.Visible = false;
            btnEnableGamepadMapper.Visible = false;
        }

        private void btnLaunchAntimicro_Click(object sender, EventArgs e)
        {
            Launcher.LaunchAntiMicro();
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
            AnalogSet = true;
            hideAllButtons();
            BeginSetup();
        }

        private void btnEnableGamepadMapper_Click(object sender, EventArgs e)
        {
            string launcherText = File.ReadAllText(Launcher.rootDir + "launcher.cfg");
            string[] cfgLines = File.ReadAllLines(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
            string cfgText = File.ReadAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
            var player1_old = cfgLines.Where(s => s.Contains("player1=")).ToList().First();
            launcherText = launcherText.Replace("launch_antimicro=0", "launch_antimicro=1");
            cfgText = cfgText.Replace(player1_old, "player1=keyboard");
            Launcher.LaunchAntiMicro();
            File.WriteAllText(Launcher.rootDir + "launcher.cfg", launcherText);
            File.WriteAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg", cfgText);
        }
    }
}
