using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Library.RfidReader;
using NUnit.Framework;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class RfidReaderUnitTest
    {
        private RfidReader _uut;

        private bool _rfidReadEventInvoked;
        private RfidReadEventArgs _rfidArgs;

        [SetUp]
        public void RfidReaderUnitTestSetup()
        {
            _uut = new RfidReader();

            //Handling RfidReadEventArgs
            _rfidReadEventInvoked = false;
            _rfidArgs = null;
            _uut.RfidReadEvent += (o, e) =>
            {
                _rfidReadEventInvoked = true;
                _rfidArgs = e;
            };
        }

        #region RfidReadEvent

        [Test]
        public void RfidReadEvent_MethodCalled_InvokesRfidReadEvent()
        {
            //ACT
            _uut.RfidRead(1);

            //ASSERT
            Assert.That(_rfidReadEventInvoked, Is.True);
        }
        #endregion

        #region RfidRead
        [Test]
        public void RfidRead_RfidArgsIdIsEqualToParameter_ReturnsTrue()
        {
            _uut.RfidRead(1);

            Assert.AreEqual(_rfidArgs.id, 1);
        }
        #endregion
    }
}
