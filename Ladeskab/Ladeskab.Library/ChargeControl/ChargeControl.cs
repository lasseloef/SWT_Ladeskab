using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Library.ChargeControl
{
    public class ChargeControl : IChargeControl
    {
        public IUsbCharger UsbCharger { get; set; }

        public ChargeControl(IUsbCharger usbCharger)
        {
            UsbCharger = usbCharger;
            UsbCharger.CurrentValueEvent += OnUsbChargerCurrentValueEvent;
        }

        private void OnUsbChargerCurrentValueEvent(object sender, CurrentEventArgs args)
        {
            //Not charging
            if (args.Current == 0)
            {
                //Inform subscribers
                ChargerEventArgs eventArgs = new ChargerEventArgs();
                eventArgs.Type = ChargerEventType.NotCharging;

                ChargeEvent.Invoke(this, eventArgs);
            }
            //Completed charging
            else if (0 < args.Current && args.Current <= 5)
            {
                //Stop charging and inform subscribers
                UsbCharger.StopCharge();
                ChargerEventArgs eventArgs = new ChargerEventArgs();
                eventArgs.Type = ChargerEventType.FinishedCharging;

                ChargeEvent.Invoke(this,eventArgs);
            }
            //Charging normally
            else if (5 < args.Current && args.Current <= 500)
            {
                //Continue charging and inform subscribers
                ChargerEventArgs eventArgs = new ChargerEventArgs();
                eventArgs.Type = ChargerEventType.ChargingNormally;

                ChargeEvent.Invoke(this, eventArgs);
            }
            //Charge error
            else if (500 < args.Current)
            {
                //Stop charging and inform subscribers
                UsbCharger.StopCharge();
                ChargerEventArgs eventArgs = new ChargerEventArgs();
                eventArgs.Type = ChargerEventType.ChargingError;

                ChargeEvent.Invoke(this, eventArgs);
            }
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