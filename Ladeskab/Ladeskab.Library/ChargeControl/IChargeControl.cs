﻿using System;

namespace Ladeskab.Library.ChargeControl
{
    public interface IChargeControl
    {
        bool IsConnected();
        void StartCharge();
        void StopCharge();
        event EventHandler<ChargerEventArgs> ChargeEvent;
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