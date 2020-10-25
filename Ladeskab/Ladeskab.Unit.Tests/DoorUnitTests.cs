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
        private bool DoorOpenedEventRaised;
        private bool DoorClosedEventRaised;


        [SetUp]
        public void DoorUnitTestsSetUp()
        {
            DoorOpenedEventRaised = false;
            DoorClosedEventRaised = false;

            uut = new Door();

            uut.DoorOpenedEvent += (o, e) => { DoorOpenedEventRaised = true; };
            uut.DoorClosedEvent += (o, e) => { DoorClosedEventRaised = true; };
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
        public void UnlockDoor_CalledWithLockedDoor_DoorIsUnlocked()
        {
            //ARRANGE
            uut.CloseDoor();
            uut.LockDoor();

            //ACT
            uut.UnlockDoor();

            //ASSERT
            Assert.That(uut.Locked, Is.False);
        }

        [Test]
        public void OpenDoor_CalledWithUnlockedDoor_DoorIsOpened()
        {
            //ARRANGE
            uut.UnlockDoor();
            
            //ACT
            uut.OpenDoor();

            //ASSERT
            Assert.That(uut.Open, Is.True);
        }

        [Test]
        public void OpenDoor_CalledWithUnlockedDoor_DoorOpenedEventRaised()
        {
            //ARRANGE
            uut.UnlockDoor();

            //ACT
            uut.OpenDoor();

            //ASSERT
            Assert.That(DoorOpenedEventRaised, Is.True);
        }

        [Test]
        public void OpenDoor_CalledWithLockedDoor_DoorIsNotOpened()
        {
            //ARRANGE
            uut.LockDoor();

            //ACT
            uut.OpenDoor();

            //ASSERT
            Assert.That(uut.Open, Is.False);
        }

        [Test]
        public void OpenDoor_CalledWithLockedDoor_DoorOpenedEventRaised()
        {
            //ARRANGE
            uut.LockDoor();

            //ACT
            uut.OpenDoor();

            //ASSERT
            Assert.That(DoorOpenedEventRaised, Is.False);
        }

        [Test]
        public void CloseDoor_CalledWithOpenDoor_DoorIsClosed()
        {
            //ARRANGE
            uut.UnlockDoor();
            uut.OpenDoor();

            //ACT
            uut.CloseDoor();

            //ASSERT
            Assert.That(uut.Open, Is.False);
        }

        [Test]
        public void CloseDoor_CalledWithOpenDoor_DoorClosedEventRaised()
        {
            //ARRANGE
            uut.UnlockDoor();
            uut.OpenDoor();

            //ACT
            uut.CloseDoor();

            //ASSERT
            Assert.That(DoorClosedEventRaised, Is.True);
        }
    }
}
