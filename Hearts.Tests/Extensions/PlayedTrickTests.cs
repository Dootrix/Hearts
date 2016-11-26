using System.Collections.Generic;
using Hearts.Extensions;
using Hearts.Model;
using NUnit.Framework;

namespace Hearts.Tests.Extensions
{
    [TestFixture]
    public class PlayedTrickTests
    {
        [TestFixture]
        public class LeadSuit
        {
            [Test]
            public void LeadClubsReturnsClubs()
            {
                var cards = new List<PlayedCard>
                {
                    new PlayedCard(null, Cards.TwoOfClubs),
                    new PlayedCard(null, Cards.TwoOfDiamonds),
                    new PlayedCard(null, Cards.TwoOfHearts),
                    new PlayedCard(null, Cards.TwoOfSpades)
                };

                var result = cards.LeadSuit();

                Assert.AreEqual(Suit.Clubs, result);
            }

            [Test]
            public void LeadDiamondsReturnsDiamonds()
            {
                var cards = new List<PlayedCard>
                {
                    new PlayedCard(null, Cards.TwoOfDiamonds),
                    new PlayedCard(null, Cards.TwoOfClubs),
                    new PlayedCard(null, Cards.TwoOfHearts),
                    new PlayedCard(null, Cards.TwoOfSpades)
                };

                var result = cards.LeadSuit();

                Assert.AreEqual(Suit.Diamonds, result);
            }

            [Test]
            public void LeadHeartsReturnsHearts()
            {
                var cards = new List<PlayedCard>
                {
                    new PlayedCard(null, Cards.TwoOfHearts),
                    new PlayedCard(null, Cards.TwoOfDiamonds),
                    new PlayedCard(null, Cards.TwoOfClubs),
                    new PlayedCard(null, Cards.TwoOfSpades)
                };

                var result = cards.LeadSuit();

                Assert.AreEqual(Suit.Hearts, result);
            }

            [Test]
            public void LeadSpadesReturnsSpades()
            {
                var cards = new List<PlayedCard>
                {
                    new PlayedCard(null, Cards.TwoOfSpades),
                    new PlayedCard(null, Cards.TwoOfHearts),
                    new PlayedCard(null, Cards.TwoOfDiamonds),
                    new PlayedCard(null, Cards.TwoOfClubs)
                };

                var result = cards.LeadSuit();

                Assert.AreEqual(Suit.Spades, result);
            }
        }

        [TestFixture]
        public class Winner
        {
            [Test]
            public void LeadSpadesReturnsHighestSpades()
            {
                var cards = new List<PlayedCard>
                {
                    new PlayedCard(null, Cards.TwoOfSpades),
                    new PlayedCard(null, Cards.TwoOfHearts),
                    new PlayedCard(null, Cards.FourOfSpades),
                    new PlayedCard(null, Cards.TwoOfClubs)
                };

                var result = cards.WinningCard();

                Assert.AreEqual(Cards.FourOfSpades, result);
            }
        }
    }
}
