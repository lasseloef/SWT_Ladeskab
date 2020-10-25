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
        ChargeControl uut;
        private IUsbCharger UsbChargerSubstitute;

        [SetUp]
        public void ChargeControlUnitTestsSetup()
        {
            UsbChargerSubstitute = Substitute.For<IUsbCharger>();
            uut = new ChargeControl();
            uut.UsbCharger = UsbChargerSubstitute;
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
    }
}
