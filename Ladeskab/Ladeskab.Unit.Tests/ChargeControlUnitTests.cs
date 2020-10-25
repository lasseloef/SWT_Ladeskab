﻿using System;
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

        [Test]
        public void OnUsbChargerCurrentValueEvent_Invoked_InvokesChargeEvent()
        {
            //ARRANGE
            CurrentEventArgs args = new CurrentEventArgs();

            //ACT
            UsbChargerSubstitute.CurrentValueEvent += Raise.EventWith(new object(), args);

            //ASSERT
            Assert.That(chargerEventInvoked, Is.True);
        }
    }
}
