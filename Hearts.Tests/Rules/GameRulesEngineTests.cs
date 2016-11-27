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
        /// <summary>
        /// The lowest club should be playable.
        /// </summary>
        [TestFixture]
        public class InitialPlay
        {
            [Test]
            public void WhenTwoOfClubsInHandExpectOnlyTwoOfClubsPlayable()
            {
                var playableCards = new TestEnclosure()
                    .WithCardsInHand(Cards.Deck)
                    .GetPlayableCards();

                Assert.AreEqual("2♣", playableCards);
            }

            [Test]
            public void WhenNoTwoOfClubsInHandExpectOnlyThreeOfClubsPlayable()
            {
                var playableCards = new TestEnclosure()
                    .WithCardsInHand(Cards.Deck.ExceptCard(Cards.TwoOfClubs))
                    .GetPlayableCards();

                Assert.AreEqual("3♣", playableCards);
            }
        }

        [TestFixture]
        public class AfterTwoOfClubsPlayed
        {
            [Test]
            public void ExpectOnlyClubsPlayableIfHasClubsInHand()
            {
                var playableCards = new TestEnclosure()
                    .Play(Cards.TwoOfClubs)
                    .WithCardInHand(Cards.EightOfClubs)
                    .WithCardInHand(Cards.TenOfClubs)
                    .WithCardInHand(Cards.TenOfDiamonds)
                    .WithCardInHand(Cards.QueenOfSpades)
                    .WithCardInHand(Cards.SevenOfHearts)
                    .GetPlayableCards();

                Assert.AreEqual("8♣,T♣", playableCards);
            }

            [Test]
            public void ExpectHeartsNotUsuallyPlayable()
            {
                var playableCards = new TestEnclosure()
                    .Play(Cards.TwoOfClubs)
                    .WithCardInHand(Cards.TenOfDiamonds)
                    .WithCardInHand(Cards.FiveOfSpades)
                    .WithCardsInHand(Cards.Hearts)
                    .GetPlayableCards();

                Assert.AreEqual("T♦,5♠", playableCards);
            }

            [Test]
            public void ExpectQueenOfSpadesNotUsuallyPlayable()
            {
                var playableCards = new TestEnclosure()
                    .Play(Cards.TwoOfClubs)
                    .WithCardInHand(Cards.NineOfDiamonds)
                    .WithCardInHand(Cards.SixOfSpades)
                    .WithCardInHand(Cards.QueenOfSpades)
                    .GetPlayableCards();

                Assert.AreEqual("9♦,6♠", playableCards);
            }

            /// <summary>
            /// Extreme edge case
            /// </summary>
            [Test]
            public void OnlyQueenOfSpadesPlayableWhenOnlyQueenOfSpadesAndHearts()
            {
                var playableCards = new TestEnclosure()
                    .Play(Cards.TwoOfClubs)
                    .WithCardInHand(Cards.QueenOfSpades)
                    .WithCardsInHand(Cards.Hearts.ExceptCard(Cards.AceOfHearts))
                    .GetPlayableCards();

                Assert.AreEqual("Q♠", playableCards);
            }

            /// <summary>
            /// Extreme edge case
            /// </summary>
            [Test]
            public void HeartsPlayableWhenOnlyHearts()
            {
                var playableCards = new TestEnclosure()
                    .Play(Cards.TwoOfClubs)                    
                    .WithCardsInHand(Cards.Hearts)
                    .GetPlayableCards();

                Assert.AreEqual("2♥,3♥,4♥,5♥,6♥,7♥,8♥,9♥,T♥,J♥,Q♥,K♥,A♥", playableCards);
            }
        }

        private class TestEnclosure
        {
            private readonly List<Card> cardsInHand = new List<Card>();
            private readonly Player player;
            private readonly Round round;

            public TestEnclosure()
            {
                this.player = new Player("A"); // the player shouldn't matter
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

            public TestEnclosure Play(Card card)
            {
                this.round.Play(this.player, card);
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
