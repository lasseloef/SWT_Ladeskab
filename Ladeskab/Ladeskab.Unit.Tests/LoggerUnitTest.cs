using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Ladeskab.Library.Logger;
using System.IO;
using NSubstitute;

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
            uut.writer = new StringWriter();
        }

        [Test]
        public void LoggerDoorLock_IdIsCorrect()
        {
            uut.LogDoorLocked(123);
            Assert.That(result.ToString(), Is.EqualTo("LOG: Door locked by: 123\r\n"+ $"{DateTime.Now.ToLongDateString()} - {DateTime.Now.ToShortTimeString()}"));
        }
        [Test]
        public void LoggerDoorUnLock_IdIsCorrect()
        {
            uut.LogDoorUnlocked(123);
            Assert.That(result.ToString(), Is.EqualTo("LOG: Door unlocked by: 123\r\n"+ $"{DateTime.Now.ToLongDateString()} - {DateTime.Now.ToShortTimeString()}"));
        }

        [Test]
        public void LoggerDoorLock_StringWriterCalled_MessageAndTimeIsCorrect()
        {
            uut.LogDoorLocked(123);
            Assert.That(uut.writer.ToString(), Is.EqualTo("LOG: Door locked by: 123\r\n" + $"{DateTime.Now.ToLongDateString()} - {DateTime.Now.ToShortTimeString()}"));
        }

        [Test]
        public void LoggerDoorUnlock_StringWriterCalled_MessageAndTimeIsCorrect()
        {
            uut.LogDoorUnlocked(123);
            Assert.That(uut.writer.ToString(), Is.EqualTo("LOG: Door unlocked by: 123\r\n"+ $"{DateTime.Now.ToLongDateString()} - {DateTime.Now.ToShortTimeString()}"));
        }

    }
}
