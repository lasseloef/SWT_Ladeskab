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
            stationControl.Disp.DisplayMessage("Door being closed");
            System.Threading.Thread.Sleep(1000);
            stationControl.SetState(stationControl.Available);
            stationControl.Disp.DisplayMessage("Scan RFID");
        }

        public void HandleRfid(IControl stationControl, int id)
        {
            stationControl.Disp.DisplayMessage("RFID scanned with id:" + id);
            //Shows the current RFID id insted of message to close door.
            
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