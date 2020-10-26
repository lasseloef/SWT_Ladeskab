using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Library.StationControl;
using NUnit.Framework;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class DoorOpenStateUnitTest
    {
        private DoorOpenState uut;

        [SetUp]
        public void DoorOpenStateUnitTestSetup()
        {
            uut = new DoorOpenState();
        }
    }
}
