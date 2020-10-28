using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Library.ChargeControl
{
    public interface IPhoneState
    {
        public event EventHandler<EventArgs> ConnectionEvent;
        public event EventHandler<EventArgs> DisconnectionEvent;

        void HandleConnectionTry(IUsbCharger usbCharger);
        void HandleDisconnectionTry(IUsbCharger usbCharger);
    }
}
