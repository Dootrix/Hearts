using Hearts.Extensions;
using Hearts.Model;
using NUnit.Framework;

namespace Hearts.Tests.Extensions
{
    [TestFixture]
    public class SuitExtensionTests
    {
        [TestFixture]
        public class ToAbbreviation
        {
            [Test]
            public void HeartsReturnsHeartSymbol()
            {
                var abbr = Suit.Hearts.ToAbbreviation();

                Assert.AreEqual("♥", abbr);
            }

            [Test]
            public void ClubsReturnsClubSymbol()
            {
                var abbr = Suit.Clubs.ToAbbreviation();

                Assert.AreEqual("♣", abbr);
            }

            [Test]
            public void DiamondsReturnsDiamondSymbol()
            {
                var abbr = Suit.Diamonds.ToAbbreviation();

                Assert.AreEqual("♦", abbr);
            }

            [Test]
            public void SpadesReturnsSpadeSymbol()
            {
                var abbr = Suit.Spades.ToAbbreviation();

                Assert.AreEqual("♠", abbr);
            }
        }
    }
}
