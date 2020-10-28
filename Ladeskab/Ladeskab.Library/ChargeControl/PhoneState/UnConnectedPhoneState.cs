using System;

namespace Ladeskab.Library.ChargeControl
{
    public class UnConnectedPhoneState : IPhoneState
    {
        public event EventHandler<EventArgs> ConnectionEvent;
        public event EventHandler<EventArgs> DisconnectionEvent;

        public void HandleConnectionTry(IUsbCharger usbCharger)
        {
            if (usbCharger.Controller.State == usbCharger.Controller.DoorOpen)
            {
                usbCharger.PhoneState = usbCharger.PhoneConnected;
                OnConnection();
            }
        }
        public void HandleDisconnectionTry(IUsbCharger usbCharger)
        {
            if (usbCharger.Controller.State == usbCharger.Controller.DoorOpen)
            {
                OnDisconnection();
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