using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
