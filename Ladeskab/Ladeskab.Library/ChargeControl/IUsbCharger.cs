using System;
using System.Dynamic;
using Ladeskab.Library.Door;
using Ladeskab.Library.StationControl;

namespace Ladeskab.Library.ChargeControl
{
    public class CurrentEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public double Current { set; get; }
    }

    public interface IUsbCharger
    {
        // Event triggered on new current value
        event EventHandler<CurrentEventArgs> CurrentValueEvent;

        public IPhoneState PhoneState { get; set; }
        public IPhoneState PhoneConnected { get; }
        public IPhoneState PhoneUnConnected { get; }

        // Direct access to the current current value
        double CurrentValue { get; }

        // Start charging
        void StartCharge();
        // Stop charging
        void StopCharge();

        public IControl Controller { get; set; }
    }
}