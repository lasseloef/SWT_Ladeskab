using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Library.ChargeControl;
using NUnit.Framework;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class ConnectedPhoneStateUnitTest
    {
        private ConnectedPhoneState uut;
        [SetUp]
        public void ConnectedPhoneStateUnitTestSetUp()
        {
            uut = new ConnectedPhoneState();
        }
    }
}
