using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Library.ChargeControl;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Tests
{
    [TestFixture]
    public class ChargeControlUnitTests
    {
        ChargeControl uut;
        private IUsbCharger UsbChargerSubstitute;

        [SetUp]
        public void ChargeControlUnitTestsSetup()
        {
            UsbChargerSubstitute = Substitute.For<IUsbCharger>();
            uut = new ChargeControl();
        }

        
    }
}
