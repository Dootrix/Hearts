using Hearts.Extensions;
using Hearts.Model;
using Hearts.Rules;
using NUnit.Framework;
using System.Collections.Generic;

namespace Hearts.Tests.Rules
{
    [TestFixture]
    public class GameRulesEngineTests
    {
        [Test]
        public void ExpectOnlyTwoOfClubsPlayableInitially()
        {
            var playableCards = new TestEnclosure()
                .WithCardsInHand(Cards.Deck)
                .GetPlayableCards();

            Assert.AreEqual("2♣", playableCards);
        }

        [TestFixture]
        public class AfterTwoOfClubsPlayed
        {
            [Test]
            public void ExpectOnlyClubsPlayableIfHasClubsInHand()
            {
                var playableCards = new TestEnclosure()
                    .Play(new Player("A"), Cards.TwoOfClubs)
                    .WithCardInHand(Cards.EightOfClubs)
                    .WithCardInHand(Cards.TenOfDiamonds)
                    .WithCardInHand(Cards.QueenOfSpades)
                    .WithCardInHand(Cards.SevenOfHearts)
                    .GetPlayableCards();

                Assert.AreEqual("8♣", playableCards);
            }

            [Test]
            public void ExpectHeartsNotPlayableUnlessNoChoice()
            {
                var playableCards = new TestEnclosure()
                    .Play(new Player("A"), Cards.TwoOfClubs)
                    .WithCardInHand(Cards.TenOfDiamonds)
                    .WithCardInHand(Cards.FiveOfSpades)
                    .WithCardsInHand(Cards.Hearts)
                    .GetPlayableCards();

                Assert.AreEqual("T♦,5♠", playableCards);
            }

            [Test]
            public void ExpectQueenOfSpadesNotPlayableUnlessNoChoice()
            {
                var playableCards = new TestEnclosure()
                    .Play(new Player("A"), Cards.TwoOfClubs)
                    .WithCardInHand(Cards.NineOfDiamonds)
                    .WithCardInHand(Cards.SixOfSpades)
                    .WithCardInHand(Cards.QueenOfSpades)
                    .GetPlayableCards();

                Assert.AreEqual("9♦,6♠", playableCards);
            }
        }

        private class TestEnclosure
        {
            private readonly List<Card> cardsInHand = new List<Card>();
            private readonly Round round;

            public TestEnclosure()
            {
                this.round = new Round(numberOfPlayers: 4, roundNumber: 1);
            }

            public TestEnclosure WithCardsInHand(IEnumerable<Card> cards)
            {
                this.cardsInHand.AddRange(cards);
                return this;
            }

            public TestEnclosure WithCardInHand(Card card)
            {
                this.cardsInHand.Add(card);
                return this;
            }

            public TestEnclosure Play(Player player, Card card)
            {
                this.round.Play(player, card);
                return this;
            }

            public string GetPlayableCards()
            {
                var engine = new GameRulesEngine();
                return engine.GetPlayableCards(this.cardsInHand, this.round).ToDebugString();
            }
        }
    }
}
