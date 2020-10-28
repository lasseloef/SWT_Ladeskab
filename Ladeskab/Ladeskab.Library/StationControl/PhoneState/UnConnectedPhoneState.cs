namespace Ladeskab.Library.StationControl.PhoneState
{
    public class UnConnectedPhoneState : IPhoneState
    {
        public void HandleConnectionTry(IControl stationControl)
        {
            if (stationControl.Door.Open)
            {
                stationControl.Disp.DisplayMessage($"Phone is connected");
                stationControl.SetPhoneState(stationControl.PhoneConnected);
                //Debugging
                stationControl.Disp.DisplayMessage($"\nCurrent Phone state: {stationControl.PhoneState}");
            }
            else
            {
                stationControl.Disp.DisplayMessage("Can't connect phone, door is closed");
            }
        }
        public void HandleDisconnectionTry(IControl stationControl)
        {
            stationControl.Disp.DisplayMessage("A phone is not connected");
        }
    }
}