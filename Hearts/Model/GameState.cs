using System.Collections.Generic;
using System.Linq;

namespace Hearts.Model
{
    public class GameState
    {
        private readonly PlayerState playerState;

        public GameState(Player player, Game game, PlayerState playerState)
        {
            this.Player = player;
            this.Game = game;
            this.playerState = playerState;
        }

        public Player Player { get; private set; }

        public Game Game { get; private set; }

        public IEnumerable<Card> StartingCards { get { return this.playerState.Starting; }  }
        public IEnumerable<Card> PostPassCards { get { return this.playerState.PostPass; } }
        public IEnumerable<Card> CurrentCards { get { return this.playerState.Current; } }
        public IEnumerable<Card> LegalCards { get { return this.playerState.Legal; } }
    }
}
