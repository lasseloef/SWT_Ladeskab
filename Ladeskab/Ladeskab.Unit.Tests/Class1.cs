using System;
using NUnit.Framework;

namespace Ladeskab.Unit.Tests
{

    [TestFixture]
    public class Class1
    {
        [SetUp]
        public void TestSetup()
        {

        }

        [Test]
        public void TestThatAlwaysPasses()
        {
            Assert.That(true);
        }
    }
}
