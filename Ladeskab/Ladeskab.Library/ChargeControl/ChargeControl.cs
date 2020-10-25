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
            throw new NotImplementedException();
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
