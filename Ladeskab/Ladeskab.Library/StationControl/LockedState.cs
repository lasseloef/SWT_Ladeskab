using System;
using Ladeskab.Library.ChargeControl;

namespace Ladeskab.Library.StationControl
{
    public class LockedState : ILadeskabState
    {
        public void HandleOpenDoor(IControl stationControl)
        {
            stationControl.Disp.DisplayMessage("ERROR: Door is locked!");
        }

        public void HandleClosedDoor(IControl stationControl)
        {
            //Do nothing, door is already closed
        }

        public void HandleRfid(IControl stationControl, int id)
        {

        }

        public void HandleCharge(IControl stationControl, ChargerEventArgs args)
        {
            throw new NotImplementedException();
        }

    }
}