using System;
using Ladeskab.Library.ChargeControl;

namespace Ladeskab.Library.StationControl
{
    public class DoorOpenState : ILadeskabState
    {
        public void HandleOpenDoor(IControl stationControl)
        {
            stationControl.Disp.DisplayMessage("Door already open");
            //Do nothing - door already open.
        }

        public void HandleClosedDoor(IControl stationControl)
        {
            stationControl.SetState(stationControl.Available);
            stationControl.Disp.DisplayMessage("Scan RFID");
        }

        public void HandleRfid(IControl stationControl, int id)
        {
            stationControl.Disp.DisplayMessage("Please close the door");
            //Do nothing else, door needs to be closed for rfid to work
        }

        public void HandleCharge(IControl stationControl, ChargerEventArgs args)
        {
            if (args.Type == ChargerEventType.ChargingError)
            {
                stationControl.Disp.DisplayMessage("ERROR: Charger overcurrent detected! Disabling charger...");
            }
        }

    }
}