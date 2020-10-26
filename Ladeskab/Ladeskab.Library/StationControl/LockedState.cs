using System;
using Ladeskab.Library.ChargeControl;

namespace Ladeskab.Library.StationControl
{
    public class LockedState : ILadeskabState
    {
        public void HandleOpenDoor(IControl stationControl)
        {
            Console.WriteLine("ERROR: Ladeskab door compromised, calling the cops, assholes!");
            stationControl.Disp.DisplayMessage("ERROR: Ladeskab door compromised, calling the cops, assholes!");
        }

        public void HandleClosedDoor(IControl stationControl)
        {

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