using Hearts.Extensions;
using Hearts.Model;
using NUnit.Framework;

namespace Hearts.Tests.Extensions
{
    [TestFixture]
    public class KindExtensionTests
    {
        [TestFixture]
        public class ToAbbreviation
        {
            [Test]
            public void TwoReturnsTwoDigit()
            {
                var abbr = Kind.Two.ToAbbreviation();

                Assert.AreEqual("2", abbr);
            }

            [Test]
            public void JackReturnsJCharacter()
            {
                var abbr = Kind.Jack.ToAbbreviation();

                Assert.AreEqual("J", abbr);
            }
        }
    }
}
