using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Library.Door
{
    public class Door : IDoor
    {
        public event EventHandler DoorOpenedEvent;
        public event EventHandler DoorClosedEvent;
        public void LockDoor()
        {
            throw new NotImplementedException();
        }

        public void UnlockDoor()
        {
            throw new NotImplementedException();
        }

        public void OpenDoor()
        {
            throw new NotImplementedException();
        }

        public void CloseDoor()
        {
            throw new NotImplementedException();
        }
    }
}
