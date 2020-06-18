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
    public class ControllerEngine : IDisposable
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
            clock.Stop();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DispatcherTimer clock;
        private GamePadState oldgstate;

        // Event Declaration
        // GamePadAction: Triggered when the oldgstate is different from the current GamePadState
        public event EventHandler<ActionEventArgs> GamePadAction;

        private int clockspeed = 8;

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
