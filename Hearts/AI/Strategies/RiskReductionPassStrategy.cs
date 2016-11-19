using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;
using Hearts.Extensions;

namespace Hearts.AI.Strategies
{
    public class RiskReductionPassStrategy : IPassStrategy
    {
        public IEnumerable<Card> ChooseCardsToPass(IEnumerable<Card> startingCards, Pass pass)
        {
            var cardRisks = this.GetCardRisks(startingCards).ToList();

            var cardsToPass = GetBestPath(new List<CardRisk>(), cardRisks)
                .Select(x => x.Card)
                .ToList();

            return cardsToPass;
        }

        private List<CardRisk> GetBestPath(List<CardRisk> currentPath, IEnumerable<CardRisk> cardRisks)
        {
            List<CardRisk> bestRiskReductionPath = null;
            double bestRiskReduction = 0;

            if(currentPath.Count < 3)
            {
                foreach (var cardRisk in GetNextPaths(cardRisks))
                {
                    var newPath = new List<CardRisk>(currentPath.Concat(new[] { cardRisk }));

                    var bestSubPath = GetBestPath(newPath, cardRisks.Except(new[] { cardRisk }));
                    double bestSubPathRisk = bestSubPath.Sum(x => x.Risk);

                    if (bestRiskReductionPath == null || bestSubPathRisk > bestRiskReduction)
                    {
                        bestRiskReductionPath = bestSubPath;
                        bestRiskReduction = bestSubPathRisk;
                    }
                }

                return bestRiskReductionPath;
            }
            else
            {
                return currentPath;
            }
        }

        private IEnumerable<CardRisk> GetNextPaths(IEnumerable<CardRisk> cardRisks)
        {
            var nextPaths = new List<CardRisk>();

            var suitGroupings = cardRisks
                .GroupBy(x => x.Card.Suit);

            foreach (var suitGrouping in suitGroupings)
            {
                nextPaths.Add(suitGrouping.OrderByDescending(cr => cr.Card.Kind).First());
            }

            return nextPaths;
        }      

        private IEnumerable<CardRisk> GetCardRisks(IEnumerable<Card> cards)
        {
            // on average there will be (13 / number of player) cards played for each suit 
            // i.e. for a 4 player game, 3.25 cards played per suit and the player will typically 
            // have 3.25 card of each suit.
            // there will of course be some spread in the distributions.

            // So typically only 3 cards of each suit will need to be played.
            // So as a very rough calculation, lets analyse the risk of winning the
            // trick and possibly picking up points for the 3 lowest cards of
            // each suit.

            // 1st analyse clubs, but as the 1st club basically cannot receive points,
            // only analyse the first two.
            var risks = GetCardRisks(cards, Suit.Clubs, 2);
            risks.AddRange(GetCardRisks(cards, Suit.Diamonds, 3));
            risks.AddRange(GetCardRisks(cards, Suit.Spades, 3));
            risks.AddRange(GetCardRisks(cards, Suit.Hearts, 3));

            return risks;
        }

        private List<CardRisk> GetCardRisks(IEnumerable<Card> cards, Suit suit, int numberOfRiskyCards)
        {
            var cardRisks = new List<CardRisk>();

            var cardsToAnalyse = cards
                .Where(x => x.Suit == suit)
                .OrderBy(x => x.Kind)
                .Take(numberOfRiskyCards);

            foreach (var cardToAnalyse in cardsToAnalyse)
            {
                // assuming a linear scale ... risk 0 - 1 of winning the trick.
                // assume only 1 heart played if we win.
                double risk = Convert.ToDouble(Convert.ToInt32(cardToAnalyse.Kind) - 2.0) / 12.0;

                // but if we're playing hearts and we win, typically we'll get 4 points (assuming 4 players).
                if(suit == Suit.Hearts) risk *= 4;

                // typically we would receive the Q of spades so 13 points for playing a high Queen.
                // TODO - if Qspades owned, then Kspades, Aspades are not extra risky to play.
                if (GetHighSpades().Contains(cardToAnalyse)) risk *= 13;

                cardRisks.Add(new CardRisk { Card = cardToAnalyse, Risk = risk });
            }

            // Assuming that later we play the lower cards first, then
            // these high cards are almost zero risk!
            var otherZeroRisks = cards
                .Where(x => x.Suit == suit)
                .OrderBy(x => x.Kind)
                .Skip(numberOfRiskyCards)
                .Select(x => new CardRisk { Card = x, Risk = 0 });

            cardRisks.AddRange(otherZeroRisks);

            return cardRisks;
        }

        private IEnumerable<Card> GetHighSpades()
        {
            return new List<Card> {
                    new Card(Kind.Queen, Suit.Spades),
                    new Card(Kind.King, Suit.Spades),
                    new Card(Kind.Ace, Suit.Spades),
                };
        }

        private class CardRisk
        {
            public Card Card { get; set; }

            public double Risk { get; set; }
        }
    }
}
