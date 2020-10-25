using System;

namespace Ladeskab.Library.Door
{
    public interface IDoor
    {
        public bool Locked { get; }
        public bool Open { get; }

        event EventHandler DoorOpenedEvent;
        event EventHandler DoorClosedEvent;

        void LockDoor();
        void UnlockDoor();
    }
}