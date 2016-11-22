
using System.Collections.Generic;
using Hearts.Extensions;
using Hearts.Model;
using NUnit.Framework;

namespace Hearts.Tests.Extensions
{
    [TestFixture]
    public class CardListOrderingExtensionTests
    {
        [TestFixture]
        public class DescendingWithinSuit
        {
            [Test]
            public void EmptyListReturnsEmptyList()
            {
                var cards = new List<Card>();

                var result = cards.Descending(Suit.Clubs);
                Assert.IsEmpty(result);
            }

            [Test]
            public void OneCardReturnsOneCard()
            {
                var cards = new List<Card> { Cards.TenOfDiamonds };

                var result = cards.Descending(Suit.Clubs).ToDebugString();
                Assert.AreEqual("T♦", result);
            }

            [Test]
            public void OneOfEachSuitIsReordered()
            {
                var cards = new List<Card>
                {
                    Cards.TenOfDiamonds,
                    Cards.TenOfSpades,
                    Cards.TenOfClubs,
                    Cards.TenOfHearts
                };

                var result = cards.Descending(Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades).ToDebugString();
                Assert.AreEqual("T♣,T♦,T♥,T♠", result);
            }

            [Test]
            public void TwoOfEachSuitIsReordered()
            {
                var cards = new List<Card>
                {
                    Cards.TwoOfHearts,
                    Cards.TenOfDiamonds,
                    Cards.TenOfSpades,
                    Cards.JackOfSpades,
                    Cards.EightOfDiamonds,
                    Cards.TenOfClubs,
                    Cards.TenOfHearts,
                    Cards.SevenOfClubs,
                };

                var result = cards.Descending(Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades).ToDebugString();
                Assert.AreEqual("T♣,7♣,T♦,8♦,T♥,2♥,J♠,T♠", result);
            }

            [Test]
            public void OneSuitSpecifiedBroughtToFrontRemainderInCDSHdescendingKindOrder()
            {
                var cards = new List<Card>
                {
                    Cards.TenOfDiamonds,
                    Cards.TenOfSpades,
                    Cards.TenOfClubs,
                    Cards.TenOfHearts
                };

                var result = cards.Descending(Suit.Clubs).ToDebugString();
                Assert.AreEqual("T♣,T♦,T♠,T♥", result);
            }
        }

        [TestFixture]
        public class AscendingWithinSuit
        {
            [Test]
            public void EmptyListReturnsEmptyList()
            {
                var cards = new List<Card>();

                var result = cards.Ascending(Suit.Clubs);
                Assert.IsEmpty(result);
            }

            [Test]
            public void OneCardReturnsOneCard()
            {
                var cards = new List<Card> { Cards.TenOfDiamonds };

                var result = cards.Ascending(Suit.Clubs).ToDebugString();
                Assert.AreEqual("T♦", result);
            }

            [Test]
            public void OneOfEachSuitIsReordered()
            {
                var cards = new List<Card>
                {
                    Cards.TenOfDiamonds,
                    Cards.TenOfSpades,
                    Cards.TenOfClubs,
                    Cards.TenOfHearts
                };

                var result = cards.Ascending(Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades).ToDebugString();
                Assert.AreEqual("T♣,T♦,T♥,T♠", result);
            }

            [Test]
            public void TwoOfEachSuitIsReordered()
            {
                var cards = new List<Card>
                {
                    Cards.TwoOfHearts,
                    Cards.TenOfDiamonds,
                    Cards.TenOfSpades,
                    Cards.JackOfSpades,
                    Cards.EightOfDiamonds,
                    Cards.TenOfClubs,
                    Cards.TenOfHearts,
                    Cards.SevenOfClubs,
                };

                var result = cards.Ascending(Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades).ToDebugString();
                Assert.AreEqual("7♣,T♣,8♦,T♦,2♥,T♥,T♠,J♠", result);
            }

            [Test]
            public void OneSuitSpecifiedBroughtToFrontRemainderInCDSHdescendingKindOrder()
            {
                var cards = new List<Card>
                {
                    Cards.TenOfDiamonds,
                    Cards.TenOfSpades,
                    Cards.TenOfClubs,
                    Cards.TenOfHearts
                };

                var result = cards.Ascending(Suit.Clubs).ToDebugString();
                Assert.AreEqual("T♣,T♦,T♠,T♥", result);
            }
        }
    }
}
