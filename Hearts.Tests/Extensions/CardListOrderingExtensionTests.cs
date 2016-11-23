
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
        public class Ascending
        {
            [Test]
            public void EmptyListReturnsEmptyList()
            {
                var cards = new List<Card>();

                var result = cards.Ascending();
                Assert.IsEmpty(result);
            }

            [Test]
            public void OneCardReturnsOneCard()
            {
                var cards = new List<Card> { Cards.TenOfDiamonds };

                var result = cards.Ascending().ToDebugString();
                Assert.AreEqual("T♦", result);
            }

            [Test]
            public void OneOfEachSuitIsReordered()
            {
                var cards = new List<Card>
                {
                    Cards.TenOfDiamonds,
                    Cards.JackOfSpades,
                    Cards.TwoOfClubs,
                    Cards.AceOfHearts
                };

                var result = cards.Ascending().ToDebugString();
                Assert.AreEqual("2♣,T♦,J♠,A♥", result);
            }
        }

        [TestFixture]
        public class Descending
        {
            [Test]
            public void EmptyListReturnsEmptyList()
            {
                var cards = new List<Card>();

                var result = cards.Descending();
                Assert.IsEmpty(result);
            }

            [Test]
            public void OneCardReturnsOneCard()
            {
                var cards = new List<Card> { Cards.TenOfDiamonds };

                var result = cards.Descending().ToDebugString();
                Assert.AreEqual("T♦", result);
            }

            [Test]
            public void OneOfEachSuitIsReordered()
            {
                var cards = new List<Card>
                {
                    Cards.TenOfDiamonds,
                    Cards.JackOfSpades,
                    Cards.TwoOfClubs,
                    Cards.AceOfHearts
                };

                var result = cards.Descending().ToDebugString();
                Assert.AreEqual("A♥,J♠,T♦,2♣", result);
            }
        }

        [TestFixture]
        public class TwoThenDescendingClubs
        {
            [Test]
            public void EmptyListReturnsEmptyList()
            {
                var cards = new List<Card>();

                var result = cards.TwoThenDescendingClubs();
                Assert.IsEmpty(result);
            }

            [Test]
            public void OneNonClubCardReturnsEmptyList()
            {
                var cards = new List<Card> { Cards.TenOfDiamonds };

                var result = cards.TwoThenDescendingClubs().ToDebugString();
                Assert.IsEmpty(result);
            }

            [Test]
            public void OneOfEachSuitOnlyClubRemains()
            {
                var cards = new List<Card>
                {
                    Cards.TenOfDiamonds,
                    Cards.JackOfSpades,
                    Cards.TwoOfClubs,
                    Cards.AceOfHearts
                };

                var result = cards.TwoThenDescendingClubs().ToDebugString();
                Assert.AreEqual("2♣", result);
            }

            [Test]
            public void AllClubsReorderedWithTwoAtFront()
            {
                var cards = new List<Card>
                {
                    Cards.KingOfClubs,
                    Cards.TwoOfClubs,
                    Cards.AceOfClubs,
                    Cards.EightOfClubs,
                    Cards.NineOfClubs,
                    Cards.JackOfClubs,
                    Cards.TenOfClubs,
                    Cards.QueenOfClubs
                };

                var result = cards.TwoThenDescendingClubs().ToDebugString();
                Assert.AreEqual("2♣,A♣,K♣,Q♣,J♣,T♣,9♣,8♣", result);
            }
        }
        
        [TestFixture]
        public class GroupBySuitAmountDescending
        {
            [Test]
            public void EmptyListReturnsEmptyList()
            {
                var cards = new List<Card>();

                var result = cards.GroupBySuitAmountDescending();
                Assert.IsEmpty(result);
            }

            [Test]
            public void OneCardReturnsOneCard()
            {
                var cards = new List<Card> { Cards.TenOfDiamonds };

                var result = cards.GroupBySuitAmountDescending().ToDebugString();
                Assert.AreEqual("T♦", result);
            }

            [Test]
            public void FourThreeTwoOneOfSuitsIsReordered()
            {
                var cards = new List<Card>
                {
                    Cards.TenOfSpades,
                    Cards.NineOfSpades,
                    Cards.TwoOfSpades,

                    Cards.AceOfDiamonds,
                    Cards.FiveOfDiamonds,
                    Cards.ThreeOfDiamonds,
                    Cards.KingOfDiamonds,

                    Cards.TenOfClubs,
                    Cards.NineOfClubs,

                    Cards.JackOfHearts
                };

                var result = cards.GroupBySuitAmountDescending().ToDebugString();
                Assert.AreEqual("A♦,5♦,3♦,K♦,T♠,9♠,2♠,T♣,9♣,J♥", result);
            }
        }

        [TestFixture]
        public class GroupBySuitAmountAscending
        {
            [Test]
            public void EmptyListReturnsEmptyList()
            {
                var cards = new List<Card>();

                var result = cards.GroupBySuitAmountAscending();
                Assert.IsEmpty(result);
            }

            [Test]
            public void OneCardReturnsOneCard()
            {
                var cards = new List<Card> { Cards.TenOfDiamonds };

                var result = cards.GroupBySuitAmountAscending().ToDebugString();
                Assert.AreEqual("T♦", result);
            }

            [Test]
            public void FourThreeTwoOneOfSuitsIsReordered()
            {
                var cards = new List<Card>
                {
                    Cards.TenOfSpades,
                    Cards.NineOfSpades,
                    Cards.TwoOfSpades,

                    Cards.AceOfDiamonds,
                    Cards.FiveOfDiamonds,
                    Cards.ThreeOfDiamonds,
                    Cards.KingOfDiamonds,

                    Cards.TenOfClubs,
                    Cards.NineOfClubs,

                    Cards.JackOfHearts
                };

                var result = cards.GroupBySuitAmountAscending().ToDebugString();
                Assert.AreEqual("J♥,T♣,9♣,T♠,9♠,2♠,A♦,5♦,3♦,K♦", result);
            }
        }

        [TestFixture]
        public class GroupBySuitDescending
        {
            [Test]
            public void EmptyListReturnsEmptyList()
            {
                var cards = new List<Card>();

                var result = cards.GroupBySuitDescending(Suit.Clubs);
                Assert.IsEmpty(result);
            }

            [Test]
            public void OneCardReturnsOneCard()
            {
                var cards = new List<Card> { Cards.TenOfDiamonds };

                var result = cards.GroupBySuitDescending(Suit.Clubs).ToDebugString();
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

                var result = cards.GroupBySuitDescending(Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades).ToDebugString();
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

                var result = cards.GroupBySuitDescending(Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades).ToDebugString();
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

                var result = cards.GroupBySuitDescending(Suit.Clubs).ToDebugString();
                Assert.AreEqual("T♣,T♦,T♠,T♥", result);
            }
        }

        [TestFixture]
        public class GroupBySuitAscending
        {
            [Test]
            public void EmptyListReturnsEmptyList()
            {
                var cards = new List<Card>();

                var result = cards.GroupBySuitAscending(Suit.Clubs);
                Assert.IsEmpty(result);
            }

            [Test]
            public void OneCardReturnsOneCard()
            {
                var cards = new List<Card> { Cards.TenOfDiamonds };

                var result = cards.GroupBySuitAscending(Suit.Clubs).ToDebugString();
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

                var result = cards.GroupBySuitAscending(Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades).ToDebugString();
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

                var result = cards.GroupBySuitAscending(Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades).ToDebugString();
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

                var result = cards.GroupBySuitAscending(Suit.Clubs).ToDebugString();
                Assert.AreEqual("T♣,T♦,T♠,T♥", result);
            }
        }
    }
}
