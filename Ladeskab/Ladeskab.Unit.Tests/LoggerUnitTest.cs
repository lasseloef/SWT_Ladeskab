using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Ladeskab.Library.Logger;
using System.IO;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class LoggerUnitTest
    {
        private Logger uut;
        StringWriter result;
        [SetUp]

        public void setup()
        {
            result = new StringWriter();
            Console.SetOut(result);
            uut = new Logger();
        }

        [Test]
        public void LoggerDoorLock_IdIsCorrect()
        {
            uut.LogDoorLocked(123);
            Assert.That(result.ToString(), Is.EqualTo("Door locked by:123\r\n"));
        }
        [Test]
        public void LoggerDoorUnLock_IdIsCorrect()
        {
            uut.LogDoorUnlocked(123);
            Assert.That(result.ToString(), Is.EqualTo("Door unlocked by:123\r\n"));
        }

    }
}
