using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsHook;

namespace nullDCNetplayLauncher
{

    public class VKBMapper
    {
        Dictionary<int, string> VkbMapping;
        Dictionary<string, int> QkcDefinition;

        Dictionary<int, int> OldKeyState = null;
        Dictionary<int, int> KeyState = null;

        public VKBMapper()
        {
            // source key / dest mapping
            VkbMapping = new Dictionary<int, string>()
            {
                { (int)System.Windows.Forms.Keys.Up, "Up" },
                { (int)System.Windows.Forms.Keys.Down, "Down" },
                { (int)System.Windows.Forms.Keys.Left, "Left" },
                { (int)System.Windows.Forms.Keys.Right, "Right" },
                { (int)System.Windows.Forms.Keys.Return, "Start" },
                { (int)System.Windows.Forms.Keys.Space, "Up" },
            };

            QkcDefinition = Launcher.ReadFromQkc();
        }

        private static IKeyboardMouseEvents _GlobalHook;
        private static IKeyboardMouseEvents _AppHook;
        public static IKeyboardMouseEvents GlobalHook => _GlobalHook;
        public static IKeyboardMouseEvents AppHook => _AppHook;

        public void Subscribe()
        {
            _GlobalHook = Hook.GlobalEvents();
            _GlobalHook.KeyDown += KeyDown;
            _GlobalHook.KeyUp += KeyUp;
            _AppHook = Hook.AppEvents();
            _AppHook.KeyDown += KeyDown;
            _AppHook.KeyUp += KeyUp;
            Application.Idle += KBRoll;
        }

        public void Unsubscribe()
        {
            _GlobalHook.KeyDown -= KeyDown;
            _GlobalHook.KeyUp -= KeyUp;
            _GlobalHook.Dispose();
            _AppHook.KeyDown -= KeyDown;
            _AppHook.KeyUp -= KeyUp;
            _AppHook.Dispose();
            Application.Idle -= KBRoll;
        }

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

        private bool IsNullDcOpen()
        {
            Process[] processes = Process.GetProcessesByName("nullDC_Win32_Release-NoTrace");
            return processes.Length > 0 && Launcher.GameOpen;
        }

        public void KBRoll(object sender, EventArgs e)
        {
            
            if (IsNullDcOpen() || NetplayLaunchForm.ControllerSetupOpen)
            {
                //System.Diagnostics.Debug.WriteLine(NetplayLaunchForm.ControllerSetupOpen);

                KeyState = new Dictionary<int, int>();

                if (OldKeyState == null)
                    OldKeyState = KeyState;

                foreach (int key in VkbMapping.Keys)
                {
                    if (QkcDefinition.ContainsKey(VkbMapping[key]))
                    {
                        KeyState[key] = GetKeyState(QkcDefinition[VkbMapping[key]]);
                        if (KeyState[key] != OldKeyState[key]
                            && VkbMapping.ContainsKey(key))
                        {
                            if (KeyState[key] > 0 && GetKeyState(QkcDefinition[VkbMapping[key]]) <= 0)
                            {
                                PushKey(QkcDefinition[VkbMapping[key]]);
                                System.Diagnostics.Debug.Print($"{QkcDefinition[VkbMapping[key]]} Pushed");
                            }
                            if (KeyState[key] < 0)
                            {
                                //ReleaseKey(QkcDefinition[VkbMapping[key]]);
                                //System.Diagnostics.Debug.Print($"{QkcDefinition[VkbMapping[key]]} Released");

                            }
                        }
                    }

                }

                OldKeyState = KeyState;
            }
        }

        private void KeyDown(object sender, WindowsHook.KeyEventArgs e)
        {
            if (VkbMapping.ContainsKey((int)e.KeyCode) 
                && (NetplayLaunchForm.ControllerSetupOpen || IsNullDcOpen()))
            {
                PushKey(QkcDefinition[VkbMapping[(int)e.KeyCode]]);
                e.SuppressKeyPress = true;
            }
        }
        
        private void KeyUp(object sender, WindowsHook.KeyEventArgs e)
        {
            //ReleaseKey(QkcDefinition[VkbMapping[(int)e.KeyCode]]);

            if (VkbMapping.ContainsKey((int)e.KeyCode)
                && (NetplayLaunchForm.ControllerSetupOpen || IsNullDcOpen()))
            {
                ReleaseKey(QkcDefinition[VkbMapping[(int)e.KeyCode]]);
                System.Diagnostics.Debug.Print($"{QkcDefinition[VkbMapping[(int)e.KeyCode]]} Released");
            }
        }
        
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern IntPtr SetFocus(IntPtr hWnd);

        public void PushKey(int keyCode)
        {
            if (IsNullDcOpen() || NetplayLaunchForm.ControllerSetupOpen)
            {
                byte key = BitConverter.GetBytes(keyCode).First();
                keybd_event(key, 0, 0, 0);
            }
        }

        public void ReleaseKey(int keyCode)
        {
            if (IsNullDcOpen() || NetplayLaunchForm.ControllerSetupOpen)
            {
                byte key = BitConverter.GetBytes(keyCode).First();
                const uint KEYEVENTF_KEYUP = 0x0002;
                keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
            }
        }

    }


}
