using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ladeskab.Library.ChargeControl;
using Ladeskab.Library.StationControl;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class AvailableStateUnitTest
    {
        private AvailableState uut;
        private IControl controlSubstitute;

        [SetUp]
        public void LockedStateUnitTestSetup()
        {
            controlSubstitute = Substitute.For<IControl>();
            uut = new AvailableState();
            //controlSubstitute.ChargeControl.UsbCharger.SetPhoneState(controlSubstitute.ChargeControl.UsbCharger.PhoneUnConnected);
        }
        [Test]
        public void HandleOpenDoor_ChargeControlIrrelevant_DisplayCorrectStartMessage()
        {
            //ARRANGE
            //irrelevant

            //ACT
            uut.HandleOpenDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("Door is opening...");
        }

        [Test]
        public void HandleOpenDoor_ChargeControlIsConnected_DisplaysCorrectMessage()
        {
            //ARRANGE
            controlSubstitute.ChargeControl.UsbCharger.PhoneState =
                controlSubstitute.ChargeControl.UsbCharger.PhoneConnected;

            //ACT
            uut.HandleOpenDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("Please close the door");
        }

        [Test]
        public void HandleOpenDoor_ChargeControlIsConnected_SetsStateToDoorOpen()
        {
            //ARRANGE
            controlSubstitute.ChargeControl.UsbCharger.PhoneState =
                controlSubstitute.ChargeControl.UsbCharger.PhoneConnected;

            //ACT
            uut.HandleOpenDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Received().SetState(controlSubstitute.DoorOpen);
        }

        [Test]
        public void HandleOpenDoor_ChargeControlIsNotConnected_DisplaysCorrectMessage()
        {
            //ARRANGE
            controlSubstitute.ChargeControl.UsbCharger.PhoneState =
                controlSubstitute.ChargeControl.UsbCharger.PhoneUnConnected;

            //ACT
            uut.HandleOpenDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("Please connect a phone");
        }

        [Test]
        public void HandleOpenDoor_ChargeControlIsNotConnected_SetsStateToDoorOpen()
        {
            //ARRANGE
            controlSubstitute.ChargeControl.UsbCharger.PhoneState =
                controlSubstitute.ChargeControl.UsbCharger.PhoneUnConnected;

            //ACT
            uut.HandleOpenDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Received().SetState(controlSubstitute.DoorOpen);
        }
        

        [Test]
        public void HandleClosedDoor_Called_DoesNothing()
        {
            //ARRANGE
            //Arrange step completed in setup

            //ACT
            uut.HandleClosedDoor(controlSubstitute);

            //ASSERT
            //Assert that substitute received no calls
            controlSubstitute.Disp.Received().DisplayMessage("Door is already closed");
        }

        [Test]
        public void HandleRfid_Irrelevant_DisplayCorrectIDInMessage()
        {
            //ARRANGE
            //Irrelevant

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);
            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("RFID scanned with id:" + 12345);
        }

        [Test]
        public void HandleRfid_CalledWithDisconnectedPhone_DisplaysCorrectMessage()
        {
            //ARRANGE
            IPhoneState connected = Substitute.For<IPhoneState>();
            controlSubstitute.ChargeControl.UsbCharger.PhoneUnConnected.ReturnsForAnyArgs(connected);
            controlSubstitute.ChargeControl.UsbCharger.PhoneState.ReturnsForAnyArgs(connected);

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("ERROR: Phone not connected");
        }

        [Test]
        public void HandleRfid_CalledWithDisconnectedPhone_IdIsNotSaved()
        {
            //ARRANGE
            IPhoneState connected = Substitute.For<IPhoneState>();
            controlSubstitute.ChargeControl.UsbCharger.PhoneUnConnected.ReturnsForAnyArgs(connected);
            controlSubstitute.ChargeControl.UsbCharger.PhoneState.ReturnsForAnyArgs(connected);

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            Assert.That(controlSubstitute.OldId, Is.Not.EqualTo(12345));
        }

        [Test]
        public void HandleRfid_CalledWithDisconnectedPhone_DoorIsNotLocked()
        {
            //ARRANGE
            IPhoneState connected = Substitute.For<IPhoneState>();
            controlSubstitute.ChargeControl.UsbCharger.PhoneUnConnected.ReturnsForAnyArgs(connected);
            controlSubstitute.ChargeControl.UsbCharger.PhoneState.ReturnsForAnyArgs(connected);

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.Door.DidNotReceive().LockDoor();
        }

        [Test]
        public void HandleRfid_CalledWithDisconnectedPhone_DoesntStartCharging()
        {
            //ARRANGE
            IPhoneState connected = Substitute.For<IPhoneState>();
            controlSubstitute.ChargeControl.UsbCharger.PhoneUnConnected.ReturnsForAnyArgs(connected);
            controlSubstitute.ChargeControl.UsbCharger.PhoneState.ReturnsForAnyArgs(connected);

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.ChargeControl.DidNotReceive().StartCharge();
        }

        [Test]
        public void HandleRfid_CalledWithDisconnectedPhone_LoggerNotCalled()
        {
            //ARRANGE
            IPhoneState connected = Substitute.For<IPhoneState>();
            controlSubstitute.ChargeControl.UsbCharger.PhoneUnConnected.ReturnsForAnyArgs(connected);
            controlSubstitute.ChargeControl.UsbCharger.PhoneState.ReturnsForAnyArgs(connected);

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.Logger.DidNotReceiveWithAnyArgs().LogDoorLocked(default);
        }

        [Test]
        public void HandleRfid_CalledWithDisconnectedPhone_SetStateNotCalled()
        {
            //ARRANGE
            IPhoneState connected = Substitute.For<IPhoneState>();
            controlSubstitute.ChargeControl.UsbCharger.PhoneUnConnected.ReturnsForAnyArgs(connected);
            controlSubstitute.ChargeControl.UsbCharger.PhoneState.ReturnsForAnyArgs(connected);

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.DidNotReceiveWithAnyArgs().SetState(default);
        }

        [Test]
        public void HandleRfid_CalledWithConnectedPhone_IdIsSaved()
        {
            //ARRANGE
            IPhoneState connected = Substitute.For<IPhoneState>();
            controlSubstitute.ChargeControl.UsbCharger.PhoneConnected.ReturnsForAnyArgs(connected);
            controlSubstitute.ChargeControl.UsbCharger.PhoneState.ReturnsForAnyArgs(connected);

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            Assert.That(controlSubstitute.OldId, Is.EqualTo(12345));
        }

        [Test]
        public void HandleRfid_CalledWithConnectedPhone_DoorIsLocked()
        {
            //ARRANGE
            IPhoneState connected = Substitute.For<IPhoneState>();
            controlSubstitute.ChargeControl.UsbCharger.PhoneConnected.ReturnsForAnyArgs(connected);
            controlSubstitute.ChargeControl.UsbCharger.PhoneState.ReturnsForAnyArgs(connected);

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.Door.Received().LockDoor();
        }

        [Test]
        public void HandleRfid_CalledWithConnectedPhone_StartsCharging()
        {
            //ARRANGE
            IPhoneState connected = Substitute.For<IPhoneState>();
            controlSubstitute.ChargeControl.UsbCharger.PhoneConnected.ReturnsForAnyArgs(connected);
            controlSubstitute.ChargeControl.UsbCharger.PhoneState.ReturnsForAnyArgs(connected);

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.ChargeControl.Received().StartCharge();
        }

        [Test]
        public void HandleRfid_CalledWithConnectedPhone_LoggerLogdoorLockedCalledWithID()
        {
            //ARRANGE
            IPhoneState connected = Substitute.For<IPhoneState>();
            controlSubstitute.ChargeControl.UsbCharger.PhoneConnected.ReturnsForAnyArgs(connected);
            controlSubstitute.ChargeControl.UsbCharger.PhoneState.ReturnsForAnyArgs(connected);

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.Logger.Received().LogDoorLocked(12345);
        }

        [Test]
        public void HandleRfid_CalledWithConnectedPhone_SetStateToLocked()
        {
            //ARRANGE
            IPhoneState connected = Substitute.For<IPhoneState>();
            controlSubstitute.ChargeControl.UsbCharger.PhoneConnected.ReturnsForAnyArgs(connected);
            controlSubstitute.ChargeControl.UsbCharger.PhoneState.ReturnsForAnyArgs(connected);

            //ACT
            uut.HandleRfid(controlSubstitute, 12345);

            //ASSERT
            controlSubstitute.Received().SetState(controlSubstitute.Locked);
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
