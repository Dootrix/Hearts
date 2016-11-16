using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;
using Hearts.AI.Strategies;

namespace Hearts.AI
{
    public class NoobCrusher : IAgent
    {
        private readonly IPassStrategy passStrategy;
        private readonly IPlayStrategy followSuitStrategy;
        private readonly IPlayStrategy leadCardStrategy;
        private readonly IPlayStrategy followWithoutSuitStrategy;

        public NoobCrusher()
        {
            this.passStrategy = new PassHighestCards();
            this.followSuitStrategy = new FollowSuitStrategy();
            this.leadCardStrategy = new LeadCardStrategy();
            this.followWithoutSuitStrategy = new FollowWithoutSuitStrategy();
        }

        public string AgentName
        {
            get
            {
                return "NoobCrusher AI v1";
            }
        }

        public List<Card> ChooseCardsToPass(List<Card> startingCards, Pass pass)
        {
            return this.passStrategy.ChooseCardsToPass(startingCards, pass);
        }

        public Card ChooseCardToPlay(
            Game gameState, 
            List<Card> startingCards, 
            List<Card> availableCards, 
            List<Card> legalCards)
        {
            if (legalCards.Count == 1)
                return legalCards.Single();

            Card cardToPlay;

            if (gameState.IsLeadTurn)
            {
                cardToPlay = this.leadCardStrategy.ChooseCardToPlay(gameState, availableCards, legalCards);
            }
            else
            {
                cardToPlay = this.GetNonLeadCard(gameState, availableCards, legalCards);
            }

            return cardToPlay;
        }

        private Card GetNonLeadCard(Game gameState, List<Card> availableCards, List<Card> legalCards)
        {
            var leadSuit = gameState.CurrentTrick.First().Card.Suit;

            Card cardToPlay;

            if (legalCards.Any(x => x.Suit == leadSuit))
            {
                cardToPlay = this.followSuitStrategy.ChooseCardToPlay(gameState, availableCards, legalCards);
            }
            else
            {
                cardToPlay = this.followWithoutSuitStrategy.ChooseCardToPlay(gameState, availableCards, legalCards);
            }

            return cardToPlay;
        }
    }
}
