namespace Ladeskab.Library.StationControl.PhoneState
{
    public class ConnectedPhoneState : IPhoneState
    {
        public void HandleConnectionTry(IControl stationControl)
        {
            stationControl.Disp.DisplayMessage("A phone is already connected");
        }

        public void HandleDisconnectionTry(IControl stationControl)
        {
            if (stationControl.Door.Open)
            {
                stationControl.Disp.DisplayMessage($"Phone is disconnected");
                stationControl.SetPhoneState(stationControl.PhoneUnConnected);
                //Debugging
                stationControl.Disp.DisplayMessage($"\nCurrent Phone state: {stationControl.PhoneState}");

            }
            else
            {
                stationControl.Disp.DisplayMessage("Can't disconnect phone, door is closed");
            }
        }
    }
}