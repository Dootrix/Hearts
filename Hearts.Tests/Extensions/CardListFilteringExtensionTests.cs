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

        [TestFixture]
        public class ContiguousHighest
        {
            [Test]
            public void AllCardsReturnsAll()
            {
                var startingCards = Cards.Hearts;
                var result = startingCards.ContiguousHighest();

                CollectionAssert.AreEqual(startingCards, result);
            }

            [Test]
            public void MissingJackReturnsAceKingQueen()
            {
                var startingCards = Cards.Hearts.ExceptCard(Cards.JackOfHearts).Descending();
                var result = startingCards.ContiguousHighest();

                Assert.AreEqual(3, result.Count());
                Assert.AreEqual("A♥,K♥,Q♥", result.ToDebugString());
            }

            [Test]
            public void MissingJackUnorderedReturnsSameOrderTwoToTen()
            {
                var startingCards = new List<Card>
                {
                    Cards.TwoOfHearts, 
                    Cards.ThreeOfHearts, 
                    Cards.FourOfHearts, 
                    Cards.FiveOfHearts, 
                    Cards.SevenOfHearts, 
                    Cards.SixOfHearts, 
                    Cards.EightOfHearts, 
                    Cards.NineOfHearts, 
                    Cards.TenOfHearts,  
                    Cards.KingOfHearts,
                    Cards.QueenOfHearts, 
                    Cards.AceOfHearts
                };
                var result = startingCards.ContiguousHighest();

                Assert.AreEqual(3, result.Count());
                Assert.AreEqual("K♥,Q♥,A♥", result.ToDebugString());
            }

            [Test]
            public void MixedSuitsReturnsContiguousHighestOfEach()
            {
                var startingCards = new List<Card>
                {
                    Cards.TwoOfHearts, 
                    Cards.ThreeOfHearts,  
                    Cards.FiveOfHearts, 
                    Cards.TwoOfSpades, 
                    Cards.ThreeOfSpades, 
                    Cards.FiveOfSpades, 
                };
                var result = startingCards.ContiguousHighest();

                Assert.AreEqual(2, result.Count());
                Assert.AreEqual("5♥,5♠", result.ToDebugString());
            }
        }

        [TestFixture]
        public class ContiguousLowest
        {
            [Test]
            public void AllCardsReturnsAll()
            {
                var startingCards = Cards.Hearts;
                var result = startingCards.ContiguousLowest();

                CollectionAssert.AreEqual(startingCards, result);
            }

            [Test]
            public void MissingJackReturnsTwoToTen()
            {
                var startingCards = Cards.Hearts.ExceptCard(Cards.JackOfHearts).Descending();
                var result = startingCards.ContiguousLowest();

                Assert.AreEqual(9, result.Count());
                Assert.AreEqual("T♥,9♥,8♥,7♥,6♥,5♥,4♥,3♥,2♥", result.ToDebugString());
            }

            [Test]
            public void MissingJackUnorderedReturnsSameOrderTwoToTen()
            {
                var startingCards = new List<Card>
                {
                    Cards.TwoOfHearts, 
                    Cards.ThreeOfHearts, 
                    Cards.FourOfHearts, 
                    Cards.FiveOfHearts, 
                    Cards.SevenOfHearts, 
                    Cards.SixOfHearts, 
                    Cards.EightOfHearts, 
                    Cards.NineOfHearts, 
                    Cards.TenOfHearts,  
                    Cards.KingOfHearts,
                    Cards.QueenOfHearts, 
                    Cards.AceOfHearts
                };
                var result = startingCards.ContiguousLowest();

                Assert.AreEqual(9, result.Count());
                Assert.AreEqual("2♥,3♥,4♥,5♥,7♥,6♥,8♥,9♥,T♥", result.ToDebugString());
            }

            [Test]
            public void MixedSuitsReturnsContiguousLowestOfEach()
            {
                var startingCards = new List<Card>
                {
                    Cards.TwoOfHearts, 
                    Cards.ThreeOfHearts,  
                    Cards.FiveOfHearts, 
                    Cards.TwoOfSpades, 
                    Cards.ThreeOfSpades, 
                    Cards.FiveOfSpades, 
                };
                var result = startingCards.ContiguousLowest();

                Assert.AreEqual(4, result.Count());
                Assert.AreEqual("2♥,3♥,2♠,3♠", result.ToDebugString());
            }
        }
    }
}
