using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nullDCNetplayLauncher
{

    public class VKBMapper
    {
        Dictionary<string, int> SrcVkb;
        Dictionary<string, int> DstVkb;
        Dictionary<string, string> VkbMapping;

        Dictionary<string, int> OldKeyState = null;
        Dictionary<string, int> KeyState = null;

        Dictionary<string, int> QkcDefinition;

        //InterceptKeys ik;

        public VKBMapper()
        {
            // authorized virtual keys
            SrcVkb = new Dictionary<string, int>()
            {
                { "ENTER", 13 },
                { "SPACE", 32 },
                { "LEFT", 37 },
                { "UP", 38 },
                { "RIGHT", 39 },
                { "DOWN", 40 },
            };

            // source key / dest mapping
            VkbMapping = new Dictionary<string, string>()
            {
                { "UP", "Up" },
                { "DOWN", "Down" },
                { "LEFT", "Left" },
                { "RIGHT", "Right" },
                { "ENTER", "Start" },
                { "SPACE", "Up" },
            };

            //Application.Idle += KeyboardAction;

            //ik = new InterceptKeys();

            QkcDefinition = Launcher.ReadFromQkc();
        }

        public void KeyboardAction(object sender, EventArgs e)
        {
            if (NetplayLaunchForm.EnableVKBMapper)
                KBRoll();
        }

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

        public void KBRoll()
        {
            KeyState = new Dictionary<string, int>();

            if (OldKeyState == null)
                OldKeyState = KeyState;

            foreach (string key in SrcVkb.Keys)
            {
                KeyState[key] = GetKeyState(SrcVkb[key]) & 0x800;

                if(KeyState[key] != OldKeyState[key])
                {
                    
                    if (KeyState[key] > 0)
                    {
                        if (key == "LEFT")
                            CallVKeyMapping("RIGHT", false);
                        if (key == "UP")
                            CallVKeyMapping("DOWN", false);
                        if (key == "RIGHT")
                            CallVKeyMapping("LEFT", false);
                        if (key == "DOWN")
                            CallVKeyMapping("UP", false);

                        CallVKeyMapping(key, true);
                    }
                    else
                    {
                        CallVKeyMapping(key, false);
                    }
                }
            }
            

            OldKeyState = KeyState;
        }

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern IntPtr SetFocus(IntPtr hWnd);

        private void CallVKeyMapping(string vkey, bool push)
        {
            String called = push ? "Pushed" : "Released";
            //System.Diagnostics.Debug.WriteLine($"Virtual Key {vkey} {called}");

            Dictionary<string, int> VirtualMapping = QkcDefinition;
            if (!VkbMapping.ContainsKey(vkey))
                return;

            String virtualIndex = VkbMapping[vkey];
            if (string.IsNullOrEmpty(virtualIndex) || !VirtualMapping.ContainsKey(virtualIndex))
            {
                return;
            }

            if (push)
            {
                PushKey(BitConverter.GetBytes(VirtualMapping[virtualIndex]).First());
            }
            else
            {
                ReleaseKey(BitConverter.GetBytes(VirtualMapping[virtualIndex]).First());
            }
        }

        public void PushKey(byte key)
        {
            Process[] processes = Process.GetProcessesByName("nullDC_Win32_Release-NoTrace");
            if (processes.Length > 0 || NetplayLaunchForm.ControllerSetupOpen)
            {
                //Process ndc = processes[0];
                //IntPtr ptr = ndc.MainWindowHandle;

                //SetFocus(ptr);

                keybd_event(key, 0, 0, 0);
            }
        }

        public void ReleaseKey(byte key)
        {
            Process[] processes = Process.GetProcessesByName("nullDC_Win32_Release-NoTrace");
            if (processes.Length > 0 || NetplayLaunchForm.ControllerSetupOpen)
            {
                const uint KEYEVENTF_KEYUP = 0x0002;
                keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
            }
        }

    }


}
