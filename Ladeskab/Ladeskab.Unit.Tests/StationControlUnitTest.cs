using System;
using System.Collections.Generic;
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
