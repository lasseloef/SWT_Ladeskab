using System;
using Ladeskab.Library.ChargeControl;

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
            stationControl.State = stationControl.Available;
            stationControl.Disp.DisplayMessage("Scan RFID");
        }

        public void HandleRfid(StationControl stationControl, int id)
        {
            stationControl.Disp.DisplayMessage("Please close the door");
            //Do nothing else, door needs to be closed for rfid to work
        }

        public void HandleCharge(StationControl stationControl, ChargerEventArgs args)
        {
            if (args.Type == ChargerEventType.ChargingError)
            {
                stationControl.Disp.DisplayMessage("ERROR: Charger overcurrent detected! Disabling charger...");
            }
        }

    }
}