using System;

namespace Ladeskab.Library.ChargeControl
{
    public class ConnectedPhoneState : IPhoneState
    {
        public event EventHandler<EventArgs> ConnectionEvent;
        public event EventHandler<EventArgs> DisconnectionEvent;
        public void HandleConnectionTry(IUsbCharger usbCharger)
        {
            if (usbCharger.Door.Locked)
            {
            }
            else
            {
                //Send notice to StationControl, so it can display info
                OnConnection();
            }
        }

        public void HandleDisconnectionTry(IUsbCharger usbCharger)
        {
            if (usbCharger.Door.Open)
            {
                usbCharger.SetPhoneState(usbCharger.PhoneUnConnected);

                //Send notice to StationControl, so it can display info
                OnDisconnection();
            }
            else
            {
               // usbCharger.Disp.DisplayMessage("Can't disconnect phone, door is closed");
            }
        }

        private void OnConnection()
        {
            ConnectionEvent?.Invoke(this, new EventArgs());
        }

        private void OnDisconnection()
        {
            DisconnectionEvent?.Invoke(this, new EventArgs());
        }
    }
}