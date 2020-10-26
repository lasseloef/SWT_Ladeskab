using System;

namespace Ladeskab.Library.StationControl
{
    public class DoorOpenState : ILadeskabState
    {
        public void HandleOpenDoor(StationControl stationControl)
        {
            //Do nothing, door is already open
        }

        public void HandleClosedDoor(StationControl stationControl)
        {
            stationControl.Disp.DisplayMessage("Scan RFID");
        }

        public void HandleRfid(StationControl stationControl, int id)
        {
            stationControl.Disp.DisplayMessage("Please close the door");
        }

        public void HandleCharge(StationControl stationControl)
        {

        }

    }
}