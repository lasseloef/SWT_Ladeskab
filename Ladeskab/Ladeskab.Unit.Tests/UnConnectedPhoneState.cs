using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Library.ChargeControl;
using Ladeskab.Library.StationControl;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class UnConnectedPhoneStateUnitTest
    {
        private IUsbCharger usbChargerSubstitute;
        private ILadeskabState ladeskabState;
        private ILadeskabState openDoorState;
        private ILadeskabState closedDoorState;
        private UnConnectedPhoneState uut;
        
        [SetUp]
        public void UnConnectedPhoneStateUnitTestSetUp()
        {
            usbChargerSubstitute = Substitute.For<IUsbCharger>();
            openDoorState = Substitute.For<ILadeskabState>();
            closedDoorState = Substitute.For<ILadeskabState>();
            usbChargerSubstitute.Controller.DoorOpen = openDoorState;
            usbChargerSubstitute.Controller.Available = closedDoorState;

            uut = new UnConnectedPhoneState();
        }

        [Test]
        public void HandleConnectionTry_CalledWithOpenDoor_RaisesConnectionEvent()
        {
            //ARRANGE
            EventArgs args = new EventArgs();
            usbChargerSubstitute.Controller.State = openDoorState;

            bool raisedEvent = false;
            uut.ConnectionEvent += (sender, args) => raisedEvent = true;

            //ACT
            uut.HandleConnectionTry(usbChargerSubstitute);

            //ASSERT
            Assert.That(raisedEvent, Is.True);
        }

        [Test]
        public void HandleConnectionTry_CalledWithOpenDoor_ChangesPhoneState()
        {
            //ARRANGE
            EventArgs args = new EventArgs();
            usbChargerSubstitute.Controller.State = openDoorState;

            bool raisedEvent = false;
            uut.ConnectionEvent += (sender, args) => raisedEvent = true;

            //ACT
            uut.HandleConnectionTry(usbChargerSubstitute);

            //ASSERT
            Assert.That(usbChargerSubstitute.PhoneState, Is.EqualTo(usbChargerSubstitute.PhoneConnected));
        }

        [Test]
        public void HandleConnectionTry_CalledWithClosedDoor_RaisesNoEvent()
        {
            //ARRANGE
            EventArgs args = new EventArgs();
            usbChargerSubstitute.Controller.State = closedDoorState;

            bool raisedEvent = false;
            uut.ConnectionEvent += (sender, args) => raisedEvent = true;

            //ACT
            uut.HandleConnectionTry(usbChargerSubstitute);

            //ASSERT
            Assert.That(raisedEvent, Is.False);
        }

        [Test]
        public void HandleDisconnectionTry_CalledWithOpenDoor_RaisesDisconnectionEvent()
        {
            //ARRANGE
            EventArgs args = new EventArgs();
            usbChargerSubstitute.Controller.State = openDoorState;

            bool raisedEvent = false;
            uut.DisconnectionEvent += (sender, args) => raisedEvent = true;

            //ACT
            uut.HandleDisconnectionTry(usbChargerSubstitute);

            //ASSERT
            Assert.That(raisedEvent, Is.True);
        }

        [Test]
        public void HandleDisconnectionTry_CalledWithClosedDoor_RaisesNoEvent()
        {
            //ARRANGE
            EventArgs args = new EventArgs();
            usbChargerSubstitute.Controller.State = closedDoorState;

            bool raisedEvent = false;
            uut.DisconnectionEvent += (sender, args) => raisedEvent = true;

            //ACT
            uut.HandleDisconnectionTry(usbChargerSubstitute);

            //ASSERT
            Assert.That(raisedEvent, Is.False);
        }
    }
}
