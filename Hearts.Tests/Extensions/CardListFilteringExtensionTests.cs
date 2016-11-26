using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;
using Hearts.Model;
using NUnit.Framework;

namespace Hearts.Tests.Extensions
{
    [TestFixture]
    public class CardListFilteringExtensionTests
    {
        [TestFixture]
        public class OfSuit
        {
            [Test]
            public void HeartsReturnsOnlyHearts()
            {
                var cards = new List<Card>()
                {
                    Cards.QueenOfClubs,
                    Cards.AceOfClubs,
                    Cards.NineOfClubs,
                    Cards.TenOfHearts,
                    Cards.NineOfDiamonds,
                    Cards.ThreeOfHearts
                };

                var result = cards.OfSuit(Suit.Hearts);
                Assert.AreEqual(2, result.Count());
            }

            [Test]
            public void ClubsReturnsOnlyClubs()
            {
                var cards = new List<Card>()
                {
                    Cards.QueenOfClubs,
                    Cards.AceOfClubs,
                    Cards.NineOfClubs,
                    Cards.TenOfHearts,
                    Cards.NineOfDiamonds,
                    Cards.ThreeOfHearts
                };

                var result = cards.OfSuit(Suit.Clubs);
                Assert.AreEqual(3, result.Count());
            }

            [Test]
            public void DiamondsReturnsOnlyDiamonds()
            {
                var cards = new List<Card>()
                {
                    Cards.QueenOfClubs,
                    Cards.AceOfClubs,
                    Cards.NineOfClubs,
                    Cards.TenOfHearts,
                    Cards.NineOfDiamonds,
                    Cards.ThreeOfHearts
                };

                var result = cards.OfSuit(Suit.Diamonds);
                Assert.AreEqual(1, result.Count());
            }

            [Test]
            public void SpadesReturnsOnlySpades()
            {
                var cards = new List<Card>()
                {
                    Cards.QueenOfClubs,
                    Cards.AceOfClubs,
                    Cards.NineOfClubs,
                    Cards.TenOfHearts,
                    Cards.NineOfDiamonds,
                    Cards.ThreeOfHearts,
                    Cards.TwoOfSpades,
                    Cards.ThreeOfSpades,
                    Cards.FourOfSpades,
                    Cards.FiveOfSpades
                };

                var result = cards.OfSuit(Suit.Spades);
                Assert.AreEqual(4, result.Count());
            }
        }

        [TestFixture]
        public class Hearts
        {
            [Test]
            public void HeartsReturnsOnlyHearts()
            {
                var cards = new List<Card>()
                {
                    Cards.QueenOfClubs,
                    Cards.AceOfClubs,
                    Cards.NineOfClubs,
                    Cards.TenOfHearts,
                    Cards.NineOfDiamonds,
                    Cards.ThreeOfHearts
                };

                var result = cards.Hearts();
                Assert.AreEqual(2, result.Count());
            }
        }

        [TestFixture]
        public class Clubs
        {
            [Test]
            public void ClubsReturnsOnlyClubs()
            {
                var cards = new List<Card>()
                {
                    Cards.QueenOfClubs,
                    Cards.AceOfClubs,
                    Cards.NineOfClubs,
                    Cards.TenOfHearts,
                    Cards.NineOfDiamonds,
                    Cards.ThreeOfHearts
                };

                var result = cards.Clubs();
                Assert.AreEqual(3, result.Count());
            }
        }

        [TestFixture]
        public class Diamonds
        {
            [Test]
            public void DiamondsReturnsOnlyDiamonds()
            {
                var cards = new List<Card>()
                {
                    Cards.QueenOfClubs,
                    Cards.AceOfClubs,
                    Cards.NineOfClubs,
                    Cards.TenOfHearts,
                    Cards.NineOfDiamonds,
                    Cards.ThreeOfHearts
                };

                var result = cards.Diamonds();
                Assert.AreEqual(1, result.Count());
            }
        }

        [TestFixture]
        public class Spades
        {
            [Test]
            public void SpadesReturnsOnlySpades()
            {
                var cards = new List<Card>()
                {
                    Cards.QueenOfClubs,
                    Cards.AceOfClubs,
                    Cards.NineOfClubs,
                    Cards.TenOfHearts,
                    Cards.NineOfDiamonds,
                    Cards.ThreeOfHearts,
                    Cards.TwoOfSpades,
                    Cards.ThreeOfSpades,
                    Cards.FourOfSpades,
                    Cards.FiveOfSpades
                };

                var result = cards.Spades();
                Assert.AreEqual(4, result.Count());
            }
        }
    }
}
