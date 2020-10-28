using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Library.ChargeControl
{
    public interface IPhoneState
    {
        void HandleConnectionTry(IUsbCharger usbCharger);
        void HandleDisconnectionTry(IUsbCharger usbCharger);
    }
}
