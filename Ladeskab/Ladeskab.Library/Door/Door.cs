using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Library.Door
{
    public class Door : IDoor
    {
        public bool Locked { get; private set; }
        public bool Open { get; private set; }

        public event EventHandler DoorOpenedEvent;
        public event EventHandler DoorClosedEvent;

        public void LockDoor()
        {
            if (!Open)
            {
                Locked = true;
            }
        }

        public void UnlockDoor()
        {
            Locked = false;
        }

        public void OpenDoor()
        {
            if (!Locked)
            {
                Open = true;
                DoorOpenedEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public void CloseDoor()
        {
            Open = false;
            DoorClosedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
