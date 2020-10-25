using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Library.ChargeControl
{
    public class ChargeControl : IChargeControl
    {
        public IUsbCharger UsbCharger { get; set; }

        public ChargeControl()
        {
        }

        public bool IsConnected()
        {
            return UsbCharger.Connected;
        }

        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ChargerEventArgs> ChargeEvent;
    }
}
