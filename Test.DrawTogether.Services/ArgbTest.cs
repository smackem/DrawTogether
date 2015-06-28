using DrawTogether.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DrawTogether.Services
{
    [TestFixture]
    public class ArgbTest
    {
        [Test]
        public void TestConstruction()
        {
            var argb = Argb.FromArgb(1, 2, 3, 4);

            Assert.That(argb.A, Is.EqualTo(1));
            Assert.That(argb.R, Is.EqualTo(2));
            Assert.That(argb.G, Is.EqualTo(3));
            Assert.That(argb.B, Is.EqualTo(4));
            Assert.That(argb.Value, Is.EqualTo(0x01020304));

            argb = Argb.FromArgb(0x01020304);
            Assert.That(argb.A, Is.EqualTo(1));
            Assert.That(argb.R, Is.EqualTo(2));
            Assert.That(argb.G, Is.EqualTo(3));
            Assert.That(argb.B, Is.EqualTo(4));
            Assert.That(argb.Value, Is.EqualTo(0x01020304));
        }

        [Test]
        public void TestIntensity()
        {
            var argb = Argb.FromArgb(0xff, 0xff, 0xff, 0xff);
            Assert.That(argb.Intensity, Is.EqualTo(1.0));

            argb = Argb.FromArgb(0);
            Assert.That(argb.Intensity, Is.EqualTo(0.0));
        }
    }
}
