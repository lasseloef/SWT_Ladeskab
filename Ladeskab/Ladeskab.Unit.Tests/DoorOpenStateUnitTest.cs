using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ladeskab.Library.ChargeControl;
using Ladeskab.Library.StationControl;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class DoorOpenStateUnitTest
    {
        private DoorOpenState uut;
        private IControl controlSubstitute;

        [SetUp]
        public void DoorOpenStateUnitTestSetup()
        {
            controlSubstitute = Substitute.For<IControl>();
            uut = new DoorOpenState();
        }

        [Test]
        public void HandleOpenDoor_Called_stationControlReceivedNoCalls()
        {
            //ARRANGE
            //Arrange step completed in setup

            //ACT
            uut.HandleOpenDoor(controlSubstitute);

            //ASSERT
            //Assert that substitute received no calls
            Assert.That(controlSubstitute.ReceivedCalls().Count(), Is.EqualTo(0));
        }

        [Test]
        public void HandleClosedDoor_Called_stationControlStateSetToAvailable()
        {
            //ARRANGE
            //Arrange step completed in setup

            //ACT
            uut.HandleClosedDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Received().SetState(controlSubstitute.Available);
        }

        [Test]
        public void HandleClosedDoor_Called_stationControlDispDisplaysCorrectMessage()
        {
            //ARRANGE
            //Arrange step completed in setup

            //ACT
            uut.HandleClosedDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("Scan RFID");
        }

        [Test]
        public void HandleRfid_Called_stationControlDispDisplaysCorrectMessage()
        {
            //ARRANGE
            //Arrange step completed in setup

            //ACT
            uut.HandleRfid(controlSubstitute, 0);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("Please close the door");
        }

        [Test]
        public void HandleCharge_CalledWithChargingNormally_MakesNoCalls()
        {
            //ARRANGE
            ChargerEventArgs args = new ChargerEventArgs();
            args.Type = ChargerEventType.ChargingNormally;

            //ACT
            uut.HandleCharge(controlSubstitute, args);

            //ASSERT
            //Assert that substitute received no calls
            Assert.That(controlSubstitute.ReceivedCalls().Count(), Is.EqualTo(0));
        }

        [Test]
        public void HandleCharge_CalledWithFinishedCharging_MakesNoCalls()
        {
            //ARRANGE
            ChargerEventArgs args = new ChargerEventArgs();
            args.Type = ChargerEventType.FinishedCharging;

            //ACT
            uut.HandleCharge(controlSubstitute, args);

            //ASSERT
            //Assert that substitute received no calls
            Assert.That(controlSubstitute.ReceivedCalls().Count(), Is.EqualTo(0));
        }

        [Test]
        public void HandleCharge_CalledWithNotCharging_MakesNoCalls()
        {
            //ARRANGE
            ChargerEventArgs args = new ChargerEventArgs();
            args.Type = ChargerEventType.NotCharging;

            //ACT
            uut.HandleCharge(controlSubstitute, args);

            //ASSERT
            //Assert that substitute received no calls
            Assert.That(controlSubstitute.ReceivedCalls().Count(), Is.EqualTo(0));
        }

        [Test]
        public void HandleCharge_CalledWithChargingError_DisplaysCorrectMessage()
        {
            //ARRANGE
            ChargerEventArgs args = new ChargerEventArgs();
            args.Type = ChargerEventType.ChargingError;

            //ACT
            uut.HandleCharge(controlSubstitute, args);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("ERROR: Charger overcurrent detected! Disabling charger...");

        }
    }
}
