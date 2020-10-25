using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Library.Door;
using Microsoft.VisualStudio.TestPlatform;
using NUnit.Framework;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class DoorUnitTests
    {
        private Door uut;

        [SetUp]
        public void DoorUnitTestsSetUp()
        {
            uut = new Door();
        }

        [Test]
        public void LockDoor_CalledWithClosedDoor_DoorIsLocked()
        {
            //ARRANGE
            uut.CloseDoor();

            //ACT
            uut.LockDoor();

            //ASSERT
            Assert.That(uut.Locked, Is.True);
        }

        [Test]
        public void LockDoor_CalledWithOpenDoor_DoorIsNotLocked()
        {
            //ARRANGE
            uut.OpenDoor();

            //ACT
            uut.LockDoor();

            //ASSERT
            Assert.That(uut.Locked, Is.False);
        }

        [Test]
        public void UnlockDoor_Called_DoorIsUnlocked()
        {
            //ARRANGE
            //Arrange completed in setup

            //ACT
            uut.UnlockDoor();

            //ASSERT
            Assert.That(uut.Locked, Is.False);
        }
    }
}
