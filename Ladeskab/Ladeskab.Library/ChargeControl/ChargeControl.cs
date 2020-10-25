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
            UsbCharger.StartCharge();
        }

        public void StopCharge()
        {
            UsbCharger.StopCharge();
        }

        public event EventHandler<ChargerEventArgs> ChargeEvent;
    }
}
