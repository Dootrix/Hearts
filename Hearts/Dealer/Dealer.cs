using System.Collections.Generic;

namespace Hearts
{
    public class Dealer
    {
        private IFactory<Deck> factory;
        private IDealAlgorithm dealAlgorithm;

        public Dealer(IFactory<Deck> factory, IDealAlgorithm dealAlgorithm)
        {
            this.factory = factory;
            this.dealAlgorithm = dealAlgorithm;
            this.NewDeck();
        }

        public Deck Deck { get; private set; }

        public void DealStartingHands(IEnumerable<Player> players)
        {
            this.NewDeck();
            this.Deck.Shuffle();
            this.dealAlgorithm.DealStartingHands(this.Deck, players);
        }

        private void NewDeck()
        {
            this.Deck = factory.Create();
        }
    }
}