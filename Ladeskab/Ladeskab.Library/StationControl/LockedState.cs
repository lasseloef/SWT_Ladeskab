using System;
using Ladeskab.Library.ChargeControl;

namespace Ladeskab.Library.StationControl
{
    public class LockedState : ILadeskabState
    {
        public void HandleOpenDoor(StationControl stationControl)
        {
            Console.WriteLine("ERROR: Ladeskab door compromised, calling the cops, assholes!");
            stationControl.Disp.DisplayMessage("ERROR: Ladeskab door compromised, calling the cops, assholes!");
        }

        public void HandleClosedDoor(StationControl stationControl)
        {

        }

        public void HandleRfid(StationControl stationControl, int id)
        {

        }

        public void HandleCharge(StationControl stationControl, ChargerEventArgs args)
        {
            throw new NotImplementedException();
        }

    }
}