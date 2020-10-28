using Ladeskab.Library.ChargeControl;

namespace Ladeskab.Library.StationControl
{
    public interface ILadeskabState
    {
        void HandleOpenDoor(IControl stationControl);
        void HandleClosedDoor(IControl stationControl);
        void HandleRfid(IControl stationControl, int id);
        void HandleCharge(IControl stationControl, ChargerEventArgs args);
    }
}