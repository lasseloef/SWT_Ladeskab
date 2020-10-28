using System;
using System.Timers;
using Ladeskab.Library.Door;
using Ladeskab.Library.StationControl;

namespace Ladeskab.Library.ChargeControl
{
    public class UsbChargerSimulator : IUsbCharger
    {
        public IPhoneState PhoneState { get; set; }
        public IPhoneState PhoneConnected { get; private set; }
        public IPhoneState PhoneUnConnected { get; private set; }
        public IControl Controller { get; set; }

        // Constants
        private const double MaxCurrent = 500.0; // mA
        private const double FullyChargedCurrent = 2.5; // mA
        private const double OverloadCurrent = 750; // mA
        private const int ChargeTimeMinutes = 20; // minutes
        private const int CurrentTickInterval = 250; // ms

        public event EventHandler<CurrentEventArgs> CurrentValueEvent;

        public double CurrentValue { get; private set; }


        private bool _overload;
        private bool _charging;
        private System.Timers.Timer _timer;
        private int _ticksSinceStart;

        public UsbChargerSimulator()
        {
            CurrentValue = 0.0;
            _overload = false;

            _timer = new System.Timers.Timer();
            _timer.Enabled = false;
            _timer.Interval = CurrentTickInterval;
            _timer.Elapsed += TimerOnElapsed;

            //Must know about the controller, since a phone shouldn't be able to be connected or disconnected when door is closed.

            //Phone States
            PhoneConnected = new ConnectedPhoneState();
            PhoneUnConnected = new UnConnectedPhoneState();
            PhoneState = PhoneUnConnected;
        }

        public UsbChargerSimulator(IPhoneState connected, IPhoneState unConnected, IControl controller)
        {
            CurrentValue = 0.0;
            _overload = false;

            _timer = new System.Timers.Timer();
            _timer.Enabled = false;
            _timer.Interval = CurrentTickInterval;
            _timer.Elapsed += TimerOnElapsed;

            //Phone States
            PhoneConnected = connected;
            PhoneUnConnected = unConnected;
            PhoneState = PhoneUnConnected;

        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            // Only execute if charging
            if (_charging)
            {
                _ticksSinceStart++;
                if (PhoneState == PhoneConnected && !_overload)
                {
                    double newValue = MaxCurrent - 
                                      _ticksSinceStart * (MaxCurrent - FullyChargedCurrent) / (ChargeTimeMinutes * 60 * 1000 / CurrentTickInterval);
                    CurrentValue = Math.Max(newValue, FullyChargedCurrent);
                }
                else if (PhoneState == PhoneConnected && _overload)
                {
                    CurrentValue = OverloadCurrent;
                }
                else if (PhoneState == PhoneUnConnected)
                {
                    CurrentValue = 0.0;
                }

                OnNewCurrent();
            }
        }

        public void SimulateConnected(bool connected)
        {
            if (!connected)
            {
                PhoneState.HandleConnectionTry(this);
            }
            else
            {
                PhoneState.HandleDisconnectionTry(this);
            }   
        }

        public void SimulateOverload(bool overload)
        {
            _overload = overload;
        }

        public void StartCharge()
        {
            // Ignore if already charging
            if (!_charging)
            {
                if (PhoneState == PhoneConnected && !_overload)
                {
                    CurrentValue = 500;
                }
                else if (PhoneState == PhoneConnected && _overload)
                {
                    CurrentValue = OverloadCurrent;
                }
                else if (PhoneState == PhoneUnConnected)
                {
                    CurrentValue = 0.0;
                }

                OnNewCurrent();
                _ticksSinceStart = 0;

                _charging = true;

                _timer.Start();
            }
        }

        public void StopCharge()
        {
            _timer.Stop();

            CurrentValue = 0.0;
            OnNewCurrent();

            _charging = false;
        }

        private void OnNewCurrent()
        {
            CurrentValueEvent?.Invoke(this, new CurrentEventArgs() {Current = this.CurrentValue});
        }


        public void SetPhoneState(IPhoneState state)
        {
            PhoneState = state;
        }
    }
}
