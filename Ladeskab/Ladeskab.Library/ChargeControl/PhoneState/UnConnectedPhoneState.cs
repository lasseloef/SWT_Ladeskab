using System;

namespace Ladeskab.Library.ChargeControl
{
    public class UnConnectedPhoneState : IPhoneState
    {
        public event EventHandler<EventArgs> ConnectionEvent;
        public event EventHandler<EventArgs> DisconnectionEvent;

        public void HandleConnectionTry(IUsbCharger usbCharger)
        {
            if (usbCharger.Door.Open)
            {
                usbCharger.SetPhoneState(usbCharger.PhoneConnected);

                //Send notice to StationControl, so it can display info
                OnConnection();
            }
            else
            {
                //usbCharger.Disp.DisplayMessage("Can't connect phone, door is closed");
            }
        }
        public void HandleDisconnectionTry(IUsbCharger usbCharger)
        {
            //Send notice to StationControl, so it can display info
            OnDisconnection();
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