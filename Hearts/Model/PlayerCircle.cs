using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Model
{
    public class PlayerCircle
    {
        private List<Player> players;
        private Game gameState;

        public PlayerCircle(Game gameState)
        {
            this.players = new List<Player>();
            this.gameState = gameState;
        }

        public void AddPlayer(Player player)
        {
            var previousPlayer = this.players.LastOrDefault();
            player.PreviousPlayer = previousPlayer ?? player;
            player.NextPlayer = this.players.Count == 0 ? player : this.players.First();
            this.players.Add(player);

            if (previousPlayer != null)
            {
                previousPlayer.NextPlayer = player;
            }

            this.players.First().PreviousPlayer = player;
        }
        
        public List<Player> AllPlayers
        {
            get { return this.players; }
        }

        public Player FirstPlayer
        {
            get { return this.players.First(); }
        }
        
        public List<Player> GetOrderedPlayersStartingWith(Player initialPlayer)
        {
            var tempPlayers = new List<Player>();
            var player = initialPlayer;

            do
            {
                tempPlayers.Add(player);
                player = player.NextPlayer;
            } while (player != initialPlayer);

            return tempPlayers;
        }
    }
}
