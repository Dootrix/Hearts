using Hearts.Logging;
using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Rules
{
    public class GameRulesEngine
    {
        private IGameRule[] rules;

        public GameRulesEngine()
        {
            this.rules = new IGameRule[] 
            {
                new MustLeadLowestClubOnFirstPlayRule(),
                new MustFollowSuitIfPossibleRule(),
                new CannotLeadHeartsUntilBrokenRule(),
                new CannotPlayQueenSpadesOrHeartsOnFirstHandIfPossibleRule()
            };
        }

        public IEnumerable<Card> GetPlayableCards(IEnumerable<Card> cardsInHand, Round round)
        {
            var filteredCards = cardsInHand;

            foreach (var rule in this.rules)
            {
                if (rule.Applies(round))
                {
                    filteredCards = rule.FilterCards(filteredCards, round);

                    if (!filteredCards.Any())
                    {
                        Log.OutOfCardsException();
                    }
                }
            }

            return filteredCards;
        }
    }
}
