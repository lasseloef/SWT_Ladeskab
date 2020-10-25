using System;

namespace Ladeskab.Library.Door
{
    public interface IDoor
    {
        event EventHandler DoorOpenedEvent;
        event EventHandler DoorClosedEvent;

        void LockDoor();
        void UnlockDoor();
    }
}