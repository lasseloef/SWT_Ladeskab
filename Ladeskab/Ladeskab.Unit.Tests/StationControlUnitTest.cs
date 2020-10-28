using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using Ladeskab.Library.ChargeControl;
using Ladeskab.Library.Display;
using Ladeskab.Library.Door;
using Ladeskab.Library.Logger;
using Ladeskab.Library.RfidReader;
using Ladeskab.Library.StationControl;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class StationControlUnitTest
    {
        private StationControl _uut;
        private ILogger _loggerSubstitute;
        private IDisplay _displaySubstitute;
        private IDoor _doorSubstitute;
        private ILadeskabState _availableSubstitute;
        private ILadeskabState _doorOpenSubstitute;
        private ILadeskabState _lockedSubstitute;
        private IRfidReader _rfidReaderSubstitute;
        private IChargeControl _chargeControlSubstitute;

        [SetUp]
        public void StationControlUnitTestSetup()
        {
            _loggerSubstitute = Substitute.For<ILogger>();
            _displaySubstitute = Substitute.For<IDisplay>();
            _doorSubstitute = Substitute.For<IDoor>();
            _availableSubstitute = Substitute.For<ILadeskabState>();
            _doorOpenSubstitute = Substitute.For<ILadeskabState>();
            _lockedSubstitute = Substitute.For<ILadeskabState>();
            _rfidReaderSubstitute = Substitute.For<IRfidReader>();
            _chargeControlSubstitute = Substitute.For<IChargeControl>();


            _uut = new StationControl(_loggerSubstitute, _displaySubstitute, _doorSubstitute, _availableSubstitute, 
                _doorOpenSubstitute, _lockedSubstitute, _rfidReaderSubstitute, _chargeControlSubstitute);

            _uut.SetState(_availableSubstitute);
        }

        #region ctor

        [Test]
        public void ctor_Modules()
        {
            _uut = new StationControl(_loggerSubstitute, _displaySubstitute, _doorSubstitute, _rfidReaderSubstitute, _chargeControlSubstitute);

            Assert.That(_uut.Logger, Is.SameAs(_loggerSubstitute));
            Assert.That(_uut.Disp, Is.SameAs(_displaySubstitute));
            Assert.That(_uut.Door, Is.SameAs(_doorSubstitute));
            Assert.That(_uut.RfidReader, Is.SameAs(_rfidReaderSubstitute));
            Assert.That(_uut.ChargeControl, Is.SameAs(_chargeControlSubstitute));
        }
        #endregion

        #region Setters and getters

        [Test]
        public void OldIDGet_GetDefaultValue_Returns0()
        {
            //ARRANGE
            //Already completed in setup

            //ACT
            int result = _uut.OldId;

            //ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void OldIDSet_SetValue_ValueIsSet()
        {
            //ARRANGE
            _uut.OldId = 1234;

            //ACT
            int result = _uut.OldId;

            //ASSERT
            Assert.That(result, Is.EqualTo(1234));
        }

        [Test]
        public void DoorOpenGet_GetCalled_ReturnsSubstitute()
        {
            //ARRANGE
            //Already completed in setup

            //ACT
            ILadeskabState returnedState = _uut.DoorOpen;

            //ASSERT
            Assert.That(returnedState, Is.EqualTo(_doorOpenSubstitute));
        }

        //Setter for DoorOpen is private

        [Test]
        public void LockedGet_GetCalled_ReturnsSubstitute()
        {
            //ARRANGE
            //Already completed in setup

            //ACT
            ILadeskabState returnedState = _uut.Locked;

            //ASSERT
            Assert.That(returnedState, Is.EqualTo(_lockedSubstitute));
        }

        [Test]
        public void StateGet_GetCalled_ReturnsDefaultSubstitute()
        {
            //ARRANGE
            //Already completed in setup

            //ACT
            ILadeskabState returnedState = _uut.State;

            //ASSERT
            Assert.That(returnedState, Is.EqualTo(_availableSubstitute));
        }

        [Test]
        public void AvailableGet_GetCalled_ReturnsSubstitute()
        {
            //ARRANGE
            //Already completed in setup

            //ACT
            ILadeskabState returnedState = _uut.Available;

            //ASSERT
            Assert.That(returnedState, Is.EqualTo(_availableSubstitute));
        }

        [Test]
        public void RfidReaderGet_GetCalled_ReturnsSubstitute()
        {
            //ARRANGE
            //Already completed in setup

            //ACT
            IRfidReader returnedRfidReader = _uut.RfidReader;

            //ASSERT
            Assert.That(returnedRfidReader, Is.EqualTo(_rfidReaderSubstitute));
        }

        [Test]
        public void ChargeControlGet_GetCalled_ReturnsSubstitute()
        {
            //ARRANGE
            //Already completed in setup

            //ACT
            IChargeControl returnedChargeControl = _uut.ChargeControl;

            //ASSERT
            Assert.That(returnedChargeControl, Is.EqualTo(_chargeControlSubstitute));
        }

        [Test]
        public void LoggerGet_GetCalled_ReturnsSubstitute()
        {
            //ARRANGE
            //Already completed in setup

            //ACT
            ILogger returnedLogger = _uut.Logger;

            //ASSERT
            Assert.That(returnedLogger, Is.EqualTo(_loggerSubstitute));
        }

        [Test]
        public void DispGet_GetCalled_ReturnsSubstitute()
        {
            //ARRANGE
            //Already completed in setup

            //ACT
            IDisplay returnedDisplay = _uut.Disp;

            //ASSERT
            Assert.That(returnedDisplay, Is.EqualTo(_displaySubstitute));
        }

        [Test]
        public void DoorGet_GetCalled_ReturnsSubstitute()
        {
            //ARRANGE
            //Already completed in setup

            //ACT
            IDoor returnedDoor = _uut.Door;

            //ASSERT
            Assert.That(returnedDoor, Is.EqualTo(_doorSubstitute));
        }
        #endregion

        #region Start
        [Test]
        public void Start_StateEqualsAvailable_ReturnsTrue()
        {
            //ACT
            _uut.Start();

            //ASSERT
            Assert.AreEqual(_uut.State, _availableSubstitute);
        }

        [Test]
        public void Start_DoorIsUnlocked_ReturnsTrue()
        {
            //ACT
            _uut.Start();

            //ASSERT
            Assert.That(_uut.Door.Locked, Is.False);
        }

        [Test]
        public void Start_MessagedIsDisplayed_ReturnsTrue()
        {
            //ACT
            _uut.Start();

            //ASSERT
            _uut.Disp.ReceivedWithAnyArgs().DisplayMessage(default);
        }
        #endregion

        #region OnChargeControlChargeEvent
        [Test]
        public void OnChargeControlChargeEvent_Called_CallsHandleCharge()
        {
            //ARRANGE
            ChargerEventArgs chargerArgs = new ChargerEventArgs();

            //ACT
            _chargeControlSubstitute.ChargeEvent +=
                (sender, args) => _uut.OnChargeControlChargeEvent(new object(), chargerArgs);

            _chargeControlSubstitute.ChargeEvent += Raise.EventWith(new object(), chargerArgs);

            //ASSERT
            _uut.State.ReceivedWithAnyArgs().HandleCharge(default, default);

        }
        #endregion

        #region OnRfidReaderRfidRead
        [Test]
        public void OnRfidReaderRfidRead_Called_CallsHandleRfid()
        {
            //ARRANGE
            RfidReadEventArgs rfidArgs = new RfidReadEventArgs();

            //ACT
            _rfidReaderSubstitute.RfidReadEvent +=
                (sender, args) => _uut.OnRfidReaderRfidRead(new object(), rfidArgs);

            _rfidReaderSubstitute.RfidReadEvent += Raise.EventWith(new object(), rfidArgs);

            //ASSERT
            _uut.State.ReceivedWithAnyArgs().HandleRfid(default, default);
        }
        #endregion

        #region OnDoorOpened
        [Test]
        public void OnDoorOpened_Called_CallsHandleOpenDoor()
        {
            //ARRANGE
            EventArgs args = new EventArgs();

            //ACT
            _doorSubstitute.DoorOpenedEvent +=
                (sender, args) => _uut.OnDoorOpened(new object(), args);

            _doorSubstitute.DoorOpenedEvent += Raise.EventWith(new object(), args);

            //ASSERT
            _uut.State.ReceivedWithAnyArgs().HandleOpenDoor(default);
        }
        #endregion

        #region OnDoorClosed
        [Test]
        public void OnDoorClosed_Called_CallsHandleClosedDoor()
        {
            //ARRANGE
            EventArgs args = new EventArgs();

            //ACT
            _doorSubstitute.DoorClosedEvent +=
                (sender, args) => _uut.OnDoorClosed(new object(), args);

            _doorSubstitute.DoorClosedEvent += Raise.EventWith(new object(), args);

            //ASSERT
            _uut.State.ReceivedWithAnyArgs().HandleClosedDoor(default);
        }
        #endregion

        #region ConnectionEvents
        [Test]
        public void OnUnconnectedOnConnectionEvent_CalledThroughEvent_DisplaysCorrectMessage()
        {
            //ARRANGE
            EventArgs args = new EventArgs();

            //ACT
            _chargeControlSubstitute.UnConnectedConnectionEvent += (sender, args) => _uut.UnConnectedOnConnectionEvent(new object(), args);

            _chargeControlSubstitute.UnConnectedConnectionEvent += Raise.EventWith(new object(), args);

            //ASSERT
            _displaySubstitute.Received().DisplayMessage("Phone is connected");
        }

        [Test]
        public void OnUnconnectedOnDisconnectionEvent_CalledThroughEvent_DisplaysCorrectMessage()
        {
            //ARRANGE
            EventArgs args = new EventArgs();

            //ACT
            _chargeControlSubstitute.UnConnectedDisconnectionEvent += (sender, args) => _uut.UnConnectedOnDisconnectionEvent(new object(), args);

            _chargeControlSubstitute.UnConnectedDisconnectionEvent += Raise.EventWith(new object(), args);

            //ASSERT
            _displaySubstitute.Received().DisplayMessage("A phone is not connected");
        }

        [Test]
        public void OnConnectedOnConnectionEvent_CalledThroughEvent_DisplaysCorrectMessage()
        {
            //ARRANGE
            EventArgs args = new EventArgs();

            //ACT
            _chargeControlSubstitute.ConnectedConnectionEvent += (sender, args) => _uut.ConnectedOnConnectionEvent(new object(), args);

            _chargeControlSubstitute.ConnectedConnectionEvent += Raise.EventWith(new object(), args);

            //ASSERT
            _displaySubstitute.Received().DisplayMessage("A phone is already connected");
        }

        [Test]
        public void OnConnectedOnDisconnectionEvent_CalledThroughEvent_DisplaysCorrectMessage()
        {
            //ARRANGE
            EventArgs args = new EventArgs();

            //ACT
            _chargeControlSubstitute.ConnectedDisconnectionEvent += (sender, args) => _uut.ConnectedOnDisconnectionEvent(new object(), args);

            _chargeControlSubstitute.ConnectedDisconnectionEvent += Raise.EventWith(new object(), args);

            //ASSERT
            _displaySubstitute.Received().DisplayMessage("Phone is disconnected");
        }
        #endregion

        #region SetState

        [Test]
        public void SetState_StateIsLocked_ReturnsTrue()
        {
            _uut.SetState(_lockedSubstitute);

            Assert.That(_uut.State, Is.SameAs(_lockedSubstitute));
        }
        #endregion
    }
}
