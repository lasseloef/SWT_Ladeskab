namespace Ladeskab.Library.StationControl
{
    public interface ILadeskabState
    {
        void HandleOpenDoor(StationControl stationControl);
        void HandleClosedDoor(StationControl stationControl);
        void HandleRfid(StationControl stationControl, int id);
        void HandleCharge(StationControl stationControl);
    }
}