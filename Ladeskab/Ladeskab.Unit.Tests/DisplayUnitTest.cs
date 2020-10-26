using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Library.Display;
using System.IO;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class DisplayUnitTest
    {
        private Display uut;
        StringWriter result;
        [SetUp]
        public void setup()
        {
            result = new StringWriter();
            Console.SetOut(result);
            uut = new Display();
        }
        #region DisplayMessage

        [Test]
        public void DisplayMessage_DisplaysCorrect_message()
        {
            uut.DisplayMessage("Test");
            Assert.That(result.ToString(), Is.EqualTo("Test\r\n"));
            
        }
        #endregion
    }
}
