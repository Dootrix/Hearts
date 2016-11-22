using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Model
{
    public class GameState
    {
        public GameState(Player player, Game game, PlayerState cards)
        {
            this.Game = game;
            this.StartingCards = cards.Starting;
            this.PostPassCards = cards.PostPass;
            this.CurrentCards = cards.Current;
            this.LegalCards = cards.Legal;
        }

        public Player Player { get; private set; }

        public Game Game { get; private set; }

        public IEnumerable<Card> StartingCards { get; private set; }
        public IEnumerable<Card> PassedCards { get { return this.StartingCards.Except(this.PostPassCards); } }
        public IEnumerable<Card> PostPassCards { get; private set; }
        public IEnumerable<Card> CurrentCards { get; private set; }
        public IEnumerable<Card> LegalCards { get; private set; }
    }
}
