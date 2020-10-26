using System;
using Ladeskab.Library.ChargeControl;

namespace Ladeskab.Library.StationControl
{
    public class AvailableState : ILadeskabState
    {
        public void HandleOpenDoor(IControl stationControl)
        {
            if(!stationControl.ChargeControl.IsConnected())
                stationControl.Disp.DisplayMessage("Please connect a phone");
            else
            {
                stationControl.Disp.DisplayMessage("Please close the door");
            }
        }

        public void HandleClosedDoor(IControl stationControl)
        {
            stationControl.Disp.DisplayMessage("Scan RFID");
        }

        public void HandleRfid(IControl stationControl, int id)
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
                stationControl.SetState(stationControl.Locked);
            }
        }

        public void HandleCharge(IControl stationControl, ChargerEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}