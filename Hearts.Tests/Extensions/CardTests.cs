using Hearts.Model;
using NUnit.Framework;

namespace Hearts.Tests.Extensions
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void ToHashProducesCorrectHash()
        {
            Assert.IsTrue(Cards.TwoOfClubs.GetHashCode() == 0);
            Assert.IsTrue(Cards.ThreeOfDiamonds.GetHashCode() == 14);
            Assert.IsTrue(Cards.KingOfSpades.GetHashCode() == 37);
            Assert.IsTrue(Cards.AceOfHearts.GetHashCode() == 51);
        }

        [Test]
        public void FromHashProducesCorrectCard()
        {
            Assert.IsTrue(new Card(0) == Cards.TwoOfClubs);
            Assert.IsTrue(new Card(14) == Cards.ThreeOfDiamonds);
            Assert.IsTrue(new Card(37) == Cards.KingOfSpades);
            Assert.IsTrue(new Card(51) == Cards.AceOfHearts);
        }
    }
}
