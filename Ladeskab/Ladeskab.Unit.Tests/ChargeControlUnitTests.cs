using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Library.ChargeControl;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class ChargeControlUnitTests
    {
        private ChargeControl uut;
        private IUsbCharger UsbChargerSubstitute;

        private bool chargerEventInvoked;
        private ChargerEventArgs chargerEventArgs;

        [SetUp]
        public void ChargeControlUnitTestsSetup()
        {
            
            UsbChargerSubstitute = Substitute.For<IUsbCharger>();
            uut = new ChargeControl(UsbChargerSubstitute);

            //Handling ChargeEvents
            chargerEventInvoked = false;
            chargerEventArgs = null;
            uut.ChargeEvent += (o, e) =>
            {
                chargerEventInvoked = true;
                chargerEventArgs = e;
            };
        }

        #region IsConnected
        /*
        [Test]
        public void IsConnected_UsbChargerReturnsConnected_ReturnsConnected()
        {
            //ARRANGE
            UsbChargerSubstitute.Connected.Returns(true);


            //ACT
            bool result = uut.IsConnected();

            //ASSERT
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsConnected_UsbChargerReturnsNotConnected_ReturnsNotConnected()
        {
            //ARRANGE
            UsbChargerSubstitute.Connected.Returns(false);

            //ACT
            bool result = uut.IsConnected();

            //ASSERT
            Assert.That(result, Is.False);
        }

        */
        #endregion

        #region StartCharge
        [Test]
        public void StartCharge_Called_CallsUsbChargerStartCharge()
        {
            //ARRANGE
            //Arrange completed in setup

            //ACT
            uut.StartCharge();

            //ASSERT
            UsbChargerSubstitute.Received().StartCharge();
        }

        #endregion

        #region StopCharge
        [Test]
        public void StopCharge_Called_CallsUsbChargerStopCharge()
        {
            //ARRANGE
            //Arrange completed in setup

            //ACT
            uut.StopCharge();

            //ASSERT
            UsbChargerSubstitute.Received().StopCharge();
        }

        #endregion

        #region OnUsbChargerCurrentValueEvent

        #region NotCharging
        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithZero_InvokesChargeEvent()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 0;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventInvoked, Is.True);
        }

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithZero_InvokedEventTypeIsNotCharging()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 0;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventArgs.Type, Is.EqualTo(ChargerEventType.NotCharging));
        }

        #endregion

        #region FinishedChargingLowerValueBound
        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithOne_InvokesChargeEvent()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 1;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventInvoked, Is.True);
        }

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithOne_InvokedEventTypeIsFinishedCharging()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 1;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventArgs.Type, Is.EqualTo(ChargerEventType.FinishedCharging));
        }

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithOne_CalledUsbChargerStopCharge()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 1;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            UsbChargerSubstitute.Received().StopCharge();
        }

        #endregion

        #region FinishedChargingUpperValueBound

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithFive_InvokesChargeEvent()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 5;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventInvoked, Is.True);
        }

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithFive_InvokedEventTypeIsFinishedCharging()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 5;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventArgs.Type, Is.EqualTo(ChargerEventType.FinishedCharging));
        }

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithFive_CalledUsbChargerStopCharge()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 5;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            UsbChargerSubstitute.Received().StopCharge();
        }

        #endregion

        #region NormalChargingLowerValueBound
        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithSix_InvokesChargeEvent()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 6;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventInvoked, Is.True);
        }

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithSix_InvokedEventTypeIsChargingNormally()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 6;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventArgs.Type, Is.EqualTo(ChargerEventType.ChargingNormally));
        }
        #endregion

        #region NormalChargingUpperValueBound
        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithFiveHundred_InvokesChargeEvent()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 500;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventInvoked, Is.True);
        }

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithFiveHundred_InvokedEventTypeIsChargingNormally()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 500;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventArgs.Type, Is.EqualTo(ChargerEventType.ChargingNormally));
        }
        #endregion

        #region ChargingErrorLowerBound
        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithFiveHundredAndOne_InvokesChargeEvent()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 501;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventInvoked, Is.True);
        }

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithFiveHundredAndOne_InvokedEventTypeIsChargingError()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 501;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventArgs.Type, Is.EqualTo(ChargerEventType.ChargingError));
        }

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithFiveHundredAndOne_CalledUsbChargerStopCharge()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = 501;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            UsbChargerSubstitute.Received().StopCharge();
        }

        #endregion

        #region ChargingErrorUpperBound
        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithIntMax_InvokesChargeEvent()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = int.MaxValue;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventInvoked, Is.True);
        }

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithIntMax_InvokedEventTypeIsChargingError()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = int.MaxValue;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventArgs.Type, Is.EqualTo(ChargerEventType.ChargingError));
        }

        [Test]
        public void OnUsbChargerCurrentValueEvent_InvokedWithIntMax_CalledUsbChargerStopCharge()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();
            args.Current = int.MaxValue;

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            UsbChargerSubstitute.Received().StopCharge();
        }
        #endregion

        #endregion
    }
}