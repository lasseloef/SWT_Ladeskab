using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
