using Hearts.Factories;
using Hearts.Model;
using Hearts.Randomisation;
using System.Collections.Generic;

namespace Hearts.Deal
{
    internal class Dealer
    {
        private readonly IFactory<Deck> factory;
        private readonly IDealAlgorithm dealAlgorithm;

        public Dealer(IFactory<Deck> factory, IDealAlgorithm dealAlgorithm, IControlledRandom random)
        {
            this.factory = factory;
            this.dealAlgorithm = dealAlgorithm;
            this.NewDeck();
        }

        public Deck Deck { get; private set; }

        public IEnumerable<CardHand> DealStartingHands(IEnumerable<Player> players, IControlledRandom random)
        {
            this.NewDeck();
            this.Deck.Shuffle(random);
            return this.dealAlgorithm.DealStartingHands(this.Deck, players);
        }

        private void NewDeck()
        {
            this.Deck = factory.Create();
        }
    }
}