using System.Collections.Generic;
using System.Linq;

namespace Hearts.Model
{
    public class GameState
    {
        public GameState(Player player, Game game, PlayerState cards)
        {
            this.Player = player;
            this.Game = game;
            this.StartingCards = cards.Starting.ToList();
            this.PostPassCards = cards.PostPass != null ? cards.PostPass.ToList() : Enumerable.Empty<Card>();
            this.CurrentCards = cards.Current != null ? cards.Current.ToList() : Enumerable.Empty<Card>();
            this.LegalCards = cards.Legal != null ? cards.Legal.ToList() : Enumerable.Empty<Card>();
        }

        public Player Player { get; private set; }

        public Game Game { get; private set; }

        public IEnumerable<Card> StartingCards { get; private set; }
        public IEnumerable<Card> PostPassCards { get; private set; }
        public IEnumerable<Card> CurrentCards { get; private set; }
        public IEnumerable<Card> LegalCards { get; private set; }
    }
}
