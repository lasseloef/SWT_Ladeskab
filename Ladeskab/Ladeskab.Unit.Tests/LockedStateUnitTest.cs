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
    public class LockedStateUnitTest

    {
        private LockedState uut;
        private IControl controlSubstitute;

        [SetUp]
        public void LockedStateUnitTestSetup()
        {
            controlSubstitute = Substitute.For<IControl>();
            uut = new LockedState();
        }

        [Test]
        public void HandleOpenDoor_Called_stationControlDispDisplaysCorrectMessage()
        {
            //ARRANGE
            //Arrange step completed in setup

            //ACT
            uut.HandleOpenDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("ERROR: Door is locked!");
        }

        [Test]
        public void HandleClosedDoor_Called_stationCntrolDispDisplaysCorrectMessage()
        {
            //ARRANGE
            //step completed in setup

            //ACT
            uut.HandleClosedDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("Door is already locked");
        }

        [Test]
        public void HandleRfid_CalledAnyId_stationControlDispDisplaysCorrectMessage()
        {
            //ARRANGE
            //Step completed in setup

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("RFID scanned with id:" + 12345);
        }

        [Test]
        public void HandleRfid_CalledWithMismatchingId_stationControlDispDisplaysCorrectMessage()
        {
            //ARRANGE
            controlSubstitute.OldId = 12345;

            //ACT
            uut.HandleRfid(controlSubstitute, 12344);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("ERROR: RFID doesn't match!");
        }

        [Test]
        public void HandleRfid_CalledWithMismatchingId_stationControlChargeControlDidNotReceiveStopCharge()
        {
            //ARRANGE
            controlSubstitute.OldId = 12345;

            //ACT
            uut.HandleRfid(controlSubstitute, 12344);

            //ASSERT
            controlSubstitute.ChargeControl.DidNotReceive().StopCharge();
        }

        [Test]
        public void HandleRfid_CalledWithMismatchingId_stationControlDoorDidNotReceiveUnlockDoor()
        {
            //ARRANGE
            controlSubstitute.OldId = 12345;

            //ACT
            uut.HandleRfid(controlSubstitute, 12344);

            //ASSERT
            controlSubstitute.Door.DidNotReceive().UnlockDoor();
        }

        [Test]
        public void HandleRfid_CalledWithMismatchingId_stationControlLoggerDidNotReceiveLogDoorUnlocked()
        {
            //ARRANGE
            controlSubstitute.OldId = 12345;

            //ACT
            uut.HandleRfid(controlSubstitute, 12344);

            //ASSERT
            controlSubstitute.Logger.DidNotReceiveWithAnyArgs().LogDoorUnlocked(default);
        }

        [Test]
        public void HandleRfid_CalledWithMismatchingId_stationControlDidNotReceiveSetState()
        {
            //ARRANGE
            controlSubstitute.OldId = 12345;

            //ACT
            uut.HandleRfid(controlSubstitute, 12344);

            //ASSERT
            controlSubstitute.DidNotReceiveWithAnyArgs().SetState(default);
        }

        [Test]
        public void HandleRfid_CalledWithMatchingId_stationControlDispDisplaysCorrectMessage()
        {
            //ARRANGE
            controlSubstitute.OldId = 12345;

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("Door unlocked, please remove phone");
        }

        [Test]
        public void HandleRfid_CalledWithMatchingId_stationControlChargeControlReceivedStopCharge()
        {
            //ARRANGE
            controlSubstitute.OldId = 12345;

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.ChargeControl.Received().StopCharge();
        }

        [Test]
        public void HandleRfid_CalledWithMatchingId_stationControlDoorReceivedUnlockDoor()
        {
            //ARRANGE
            controlSubstitute.OldId = 12345;

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.Door.Received().UnlockDoor();
        }

        [Test]
        public void HandleRfid_CalledWithMatchingId_stationControlLoggerReceivedLogDoorUnlockedWithId()
        {
            //ARRANGE
            controlSubstitute.OldId = 12345;

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.Logger.Received().LogDoorUnlocked(12345);
        }
        [Test]
        public void HandleRfid_CalledWithMatchingId_stationControlReceivedSetStateToAvailable()
        {
            //ARRANGE
            controlSubstitute.OldId = 12345;

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.Received().SetState(controlSubstitute.Available);
        }

        [Test]
        public void HandleCharge_CalledWithChargingNormally_DisplaysCorrectMessage()
        {
            //ARRANGE
            ChargerEventArgs args = new ChargerEventArgs();
            args.Type = ChargerEventType.ChargingNormally;

            //ACT
            uut.HandleCharge(controlSubstitute, args);

            //ASSERT
            //Assert that substitute received no calls
            controlSubstitute.Disp.Received().DisplayMessage("Charging in progress...");
        }

        [Test]
        public void HandleCharge_CalledWithFinishedCharging_DisplaysCorrectMessage()
        {
            //ARRANGE
            ChargerEventArgs args = new ChargerEventArgs();
            args.Type = ChargerEventType.FinishedCharging;

            //ACT
            uut.HandleCharge(controlSubstitute, args);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("Phone charging complete. Please scan RFID tag and remove phone");
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
