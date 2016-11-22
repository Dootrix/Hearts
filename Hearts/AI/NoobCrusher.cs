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

        public Player Player { get; set; }

        public IEnumerable<Card> ChooseCardsToPass(GameState gameState)
        {
            var round = gameState.Game.CurrentRound;

            if (round.Pass == Hearts.Model.Pass.NoPass)
            {
                // Can skip any expensive logic as cards are irrelevant
                return gameState.StartingCards.Take(3);
            }

            return this.passStrategy.ChooseCardsToPass(gameState.StartingCards, round.Pass);
        }

        public Card ChooseCardToPlay(GameState gameState)
        {
            var round = gameState.Game.CurrentRound;

            if (gameState.LegalCards.Count() == 1)
                return gameState.LegalCards.Single();

            Card cardToPlay;

            if (round.IsLeadTurn)
            {
                cardToPlay = this.leadCardStrategy.ChooseCardToPlay(round, gameState.CurrentCards, gameState.LegalCards);
            }
            else
            {
                cardToPlay = this.GetNonLeadCard(round, gameState.CurrentCards, gameState.LegalCards);
            }

            return cardToPlay;
        }

        private Card GetNonLeadCard(Round round, IEnumerable<Card> availableCards, IEnumerable<Card> Legal)
        {
            var leadSuit = round.CurrentTrick.First().Card.Suit;

            Card cardToPlay;

            if (Legal.Any(x => x.Suit == leadSuit))
            {
                cardToPlay = this.followSuitStrategy.ChooseCardToPlay(round, availableCards, Legal);
            }
            else
            {
                cardToPlay = this.followWithoutSuitStrategy.ChooseCardToPlay(round, availableCards, Legal);
            }

            return cardToPlay;
        }
    }
}
