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
        }

        [Test]
        public void HandleOpenDoor_ChargeControlIsConnected_DisplaysCorrectMessage()
        {
            //ARRANGE
            controlSubstitute.ChargeControl.IsConnected().Returns(true);

            //ACT
            uut.HandleOpenDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("Please close the door");
        }

        [Test]
        public void HandleOpenDoor_ChargeControlIsConnected_SetsStateToDoorOpen()
        {
            //ARRANGE
            controlSubstitute.ChargeControl.IsConnected().Returns(true);

            //ACT
            uut.HandleOpenDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Received().SetState(controlSubstitute.DoorOpen);
        }

        [Test]
        public void HandleOpenDoor_ChargeControlIsNotConnected_DisplaysCorrectMessage()
        {
            //ARRANGE
            controlSubstitute.ChargeControl.IsConnected().Returns(false);

            //ACT
            uut.HandleOpenDoor(controlSubstitute);

            //ASSERT
            controlSubstitute.Disp.Received().DisplayMessage("Please connect a phone");
        }

        [Test]
        public void HandleOpenDoor_ChargeControlIsNotConnected_SetsStateToDoorOpen()
        {
            //ARRANGE
            controlSubstitute.ChargeControl.IsConnected().Returns(false);

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
            Assert.That(controlSubstitute.ReceivedCalls().Count(), Is.EqualTo(0));
        }
    }
}
