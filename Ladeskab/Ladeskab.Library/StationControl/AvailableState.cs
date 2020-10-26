using System;

namespace Ladeskab.Library.StationControl
{
    public class AvailableState : ILadeskabState
    {
        public void HandleOpenDoor(StationControl stationControl)
        {
            stationControl.Disp.DisplayMessage("Please close the door");
        }

        public void HandleClosedDoor(StationControl stationControl)
        {

        }

        public void HandleRfid(StationControl stationControl, int id)
        {
            if (!stationControl.ChargeControl.IsConnected())
            {
                stationControl.Disp.DisplayMessage("ERROR: Phone not connected");
            }
            else
            {
                stationControl.OldId = id;
                stationControl.Door.LockDoor();
                stationControl.Logger.LogDoorLocked(id);
            }
        }

        public void HandleCharge(StationControl stationControl)
        {

        }
    }
}