using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;
using Hearts.AI.Strategies;

namespace Hearts.AI
{
    public enum NoobCrusherVersion
    {
        v1,
        v2
    }

    public class NoobCrusher : IAgent
    {
        private readonly IPassStrategy passStrategy;
        private readonly IPlayStrategy followSuitStrategy;
        private readonly IPlayStrategy leadCardStrategy;
        private readonly IPlayStrategy followWithoutSuitStrategy;
        private readonly string agentName;

        public static NoobCrusher Create(NoobCrusherVersion version)
        {
            switch (version)
            {
                case NoobCrusherVersion.v1:
                    return new NoobCrusher("NoobCrusher AI v1", new PassHighestCards(), new FollowSuitStrategy(), new LeadCardStrategy(), new FollowWithoutSuitStrategy());
                case NoobCrusherVersion.v2:
                    return new NoobCrusher("NoobCrusher AI v2", new RiskReductionPassStrategy(), new FollowSuitStrategy(), new LeadCardStrategy(), new FollowWithoutSuitStrategy());
                default:
                    throw new NotImplementedException();
            }
        }

        public NoobCrusher(string agentName,
            IPassStrategy passStrategy,
            IPlayStrategy followSuitStrategy,
            IPlayStrategy leadCardStrategy,
            IPlayStrategy followWithoutSuitStrategy)
        {
            this.agentName = agentName;
            this.passStrategy = passStrategy; 
            this.followSuitStrategy = followSuitStrategy; 
            this.leadCardStrategy = leadCardStrategy; 
            this.followWithoutSuitStrategy = followWithoutSuitStrategy;
        }

        public string AgentName
        {
            get
            {
                return this.agentName;
            }
        }

        public IEnumerable<Card> ChooseCardsToPass(IEnumerable<Card> startingCards, Pass pass)
        {
            return this.passStrategy.ChooseCardsToPass(startingCards, pass);
        }

        public Card ChooseCardToPlay(
            GameState gameState,
            IEnumerable<Card> startingCards,
            IEnumerable<Card> availableCards,
            IEnumerable<Card> legalCards)
        {
            if (legalCards.Count() == 1)
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

        private Card GetNonLeadCard(GameState gameState, IEnumerable<Card> availableCards, IEnumerable<Card> legalCards)
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
