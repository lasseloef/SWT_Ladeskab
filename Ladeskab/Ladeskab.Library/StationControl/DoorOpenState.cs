using System;

namespace Ladeskab.Library.StationControl
{
    public class DoorOpenState : ILadeskabState
    {
        public void HandleOpenDoor(StationControl stationControl)
        {
            Console.WriteLine("Please connect a telephone");
            stationControl.Disp.DisplayMessage("Please connect a telephone");
        }

        public void HandleClosedDoor(StationControl stationControl)
        {

        }

        public void HandleRfid(StationControl stationControl)
        {

        }

        public void HandleCharge(StationControl stationControl)
        {

        }

    }
}