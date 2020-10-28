using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Library.StationControl.PhoneState
{
    public interface IPhoneState
    {
        void HandleConnectionTry(IControl stationControl);
        void HandleDisconnectionTry(IControl stationControl);
    }

    public class ConnectedPhoneState : IPhoneState
    {
        public void HandleConnectionTry(IControl stationControl)
        {


        }

        public void HandleDisconnectionTry(IControl stationControl)
        {

        }
    }

    public class UnConnectedPhoneState : IPhoneState
    {
        public void HandleConnectionTry(IControl stationControl)
        {
            if (stationControl.Door.Open)
            {
                stationControl.Disp.DisplayMessage($"Phone is connected");
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
