using System;

namespace Ladeskab.Library.ChargeControl
{
    public interface IChargeControl
    {
        void StartCharge();
        void StopCharge();
        event EventHandler<ChargerEventArgs> ChargeEvent;
        public event EventHandler<EventArgs> UnConnectedConnectionEvent;
        public event EventHandler<EventArgs> UnConnectedDisconnectionEvent;
        public event EventHandler<EventArgs> ConnectedConnectionEvent;
        public event EventHandler<EventArgs> ConnectedDisconnectionEvent;

        public IUsbCharger UsbCharger { get; }

    }

    public class ChargerEventArgs : EventArgs
    {
        public ChargerEventType Type { get; set; }
    }

    public enum ChargerEventType
    {
        NotCharging,
        ChargingNormally,
        FinishedCharging,
        ChargingError
    }
}