using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Rules
{
    public class CardPlayEvaluator
    {
        private IGameRule[] rules;

        public CardPlayEvaluator()
        {
            this.rules = new IGameRule[] {
                new MustLeadLowestClubOnFirstPlayRule(),
                new MustFollowSuitIfPossibleRule(),
                new CannotLeadHeartsUntilBrokenRule(),
                new CannotPlayQueenSpadesOrHeartsOnFirstHandIfPossibleRule()
            };
        }

        public IEnumerable<Card> GetPossibleCards(IEnumerable<Card> cards, Game gameState)
        {
            var filteredCards = cards;

            foreach (var rule in this.rules)
            {
                if (rule.Applies(gameState))
                {
                    filteredCards = rule.FilterCards(filteredCards, gameState);
                }
            }

            return filteredCards;
        }
    }
}
