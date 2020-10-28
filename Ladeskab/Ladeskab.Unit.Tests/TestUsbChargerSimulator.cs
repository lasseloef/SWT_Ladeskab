using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ladeskab.Library.ChargeControl;
using Ladeskab.Library.StationControl;
using NSubstitute;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class TestUsbChargerSimulator
    {
        private UsbChargerSimulator _uut;
        private IControl controlSubstitute;
        private IPhoneState connectedState;
        private IPhoneState unconnectedState;
        [SetUp]
        public void Setup()
        {
            controlSubstitute = Substitute.For<IControl>();
            connectedState = Substitute.For<IPhoneState>();
            unconnectedState = Substitute.For<IPhoneState>();
            _uut = new UsbChargerSimulator(connectedState, unconnectedState, controlSubstitute);
            _uut.Controller = controlSubstitute;
        }

        
        [Test]
        public void ctor_IsConnected()
        {
            _uut = new UsbChargerSimulator();
            Assert.That(_uut.PhoneState, Is.EqualTo(_uut.PhoneUnConnected));
        }
        

        [Test]
        public void ctor_CurentValueIsZero()
        {
            _uut = new UsbChargerSimulator();
            Assert.That(_uut.CurrentValue, Is.Zero);
        }

        [Test]
        public void setAndGetController_Called_ControllerIsSetAndGet()
        {
            _uut.Controller = controlSubstitute;

            Assert.That(_uut.Controller, Is.EqualTo(controlSubstitute));
        }
        
        [Test]
        public void SimulateDisconnected_CallsStateHandleDisconnectionTry()
        {
            _uut.SimulateConnected(false);
            _uut.PhoneState.Received().HandleDisconnectionTry(_uut);
        }

        [Test]
        public void SimulateConnected_ReturnsConnected()
        {
            _uut.SimulateConnected(true);
            _uut.PhoneState.Received().HandleConnectionTry(_uut);
        }
        [Test]
        public void Started_WaitSomeTime_ReceivedSeveralValues()
        {
            _uut.PhoneState = _uut.PhoneConnected;
            int numValues = 0;
            _uut.CurrentValueEvent += (o, args) => numValues++;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(1100);

            Assert.That(numValues, Is.GreaterThan(4));
        }

        [Test]
        public void Started_WaitSomeTime_ReceivedChangedValue()
        {
            _uut.PhoneState = _uut.PhoneConnected;
            double lastValue = 1000;
            _uut.CurrentValueEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(300);

            Assert.That(lastValue, Is.LessThan(500.0));
        }

        [Test]
        public void StartedNoEventReceiver_WaitSomeTime_PropertyChangedValue()
        {
            _uut.PhoneState = _uut.PhoneConnected;
            _uut.StartCharge();

            System.Threading.Thread.Sleep(300);

            Assert.That(_uut.CurrentValue, Is.LessThan(500.0));
        }

        [Test]
        public void Started_WaitSomeTime_PropertyMatchesReceivedValue()
        {
            _uut.PhoneState = _uut.PhoneConnected;
            double lastValue = 1000;
            _uut.CurrentValueEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(1100);

            Assert.That(lastValue, Is.EqualTo(_uut.CurrentValue));
        }


        [Test]
        public void Started_SimulateOverload_ReceivesHighValue()
        {
            _uut.PhoneState = _uut.PhoneConnected;
            ManualResetEvent pause = new ManualResetEvent(false);
            double lastValue = 0;

            _uut.CurrentValueEvent += (o, args) =>
            {
                lastValue = args.Current;
                pause.Set();
            };

            // Start
            _uut.StartCharge();

            // Next value should be high
            _uut.SimulateOverload(true);

            // Reset event
            pause.Reset();

            // Wait for next tick, should send overloaded value
            pause.WaitOne(300);

            Assert.That(lastValue, Is.GreaterThan(500.0));
        }

        [Test]
        public void Started_SimulateDisconnected_ReceivesZero()
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            double lastValue = 1000;

            _uut.CurrentValueEvent += (o, args) =>
            {
                lastValue = args.Current;
                pause.Set();
            };


            // Start
            _uut.StartCharge();

            // Next value should be zero
            _uut.SimulateConnected(false);

            // Reset event
            pause.Reset();

            // Wait for next tick, should send disconnected value
            pause.WaitOne(300);

            Assert.That(lastValue, Is.Zero);
        }

        [Test]
        public void SimulateOverload_Start_ReceivesHighValueImmediately()
        {
            _uut.PhoneState = _uut.PhoneConnected;
            double lastValue = 0;

            _uut.CurrentValueEvent += (o, args) =>
            {
                lastValue = args.Current;
            };

            // First value should be high
            _uut.SimulateOverload(true);

            // Start
            _uut.StartCharge();

            // Should not wait for first tick, should send overload immediately

            Assert.That(lastValue, Is.GreaterThan(500.0));
        }

        [Test]
        public void SimulateDisconnected_Start_ReceivesZeroValueImmediately()
        {
            double lastValue = 1000;

            _uut.CurrentValueEvent += (o, args) =>
            {
                lastValue = args.Current;
            };

            // First value should be high
            _uut.SimulateConnected(false);

            // Start
            _uut.StartCharge();

            // Should not wait for first tick, should send zero immediately

            Assert.That(lastValue, Is.Zero);
        }

        [Test]
        public void StopCharge_IsCharging_ReceivesZeroValue()
        {
            double lastValue = 1000;
            _uut.CurrentValueEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(300);

            _uut.StopCharge();

            Assert.That(lastValue, Is.EqualTo(0.0));
        }

        [Test]
        public void StopCharge_IsCharging_PropertyIsZero()
        {
            _uut.StartCharge();

            System.Threading.Thread.Sleep(300);

            _uut.StopCharge();

            Assert.That(_uut.CurrentValue, Is.EqualTo(0.0));
        }

        [Test]
        public void StopCharge_IsCharging_ReceivesNoMoreValues()
        {
            double lastValue = 1000;
            _uut.CurrentValueEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(300);

            _uut.StopCharge();
            lastValue = 1000;

            // Wait for a tick
            System.Threading.Thread.Sleep(300);

            // No new value received
            Assert.That(lastValue, Is.EqualTo(1000.0));
        }



    }
}
