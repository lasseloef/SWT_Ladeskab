using System;

namespace Ladeskab.Library.ChargeControl
{
    public class CurrentEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public double Current { set; get; }
    }

    public class ConnectionEventArgs : EventArgs
    {
        public bool Connection { set; get; }
    }

    public interface IUsbCharger
    {
        // Event triggered on new current value
        event EventHandler<CurrentEventArgs> CurrentValueEvent;
        event EventHandler<ConnectionEventArgs> ConnectedEvent;

        // Direct access to the current current value
        double CurrentValue { get; }

        // Require connection status of the phone
        bool Connected { get; }

        // Start charging
        void StartCharge();
        // Stop charging
        void StopCharge();
    }
}