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
            var previousPlayer = this.players.Last();
            player.NextPlayer = this.players.Count == 0 ? player : this.players.First();
            this.players.Add(player);

            // TODO: Tony, what on Earth does this mean?
            /*
            if let previousPlayer = previousPlayer {
                previousPlayer.nextPlayer = player
            }

            self.eventQueue.playerAddedToGame(player.name)
            }

           var firstPlayer : Player? {
            get { return self.players.first
            */
        }
    }
}

var allPlayers : [Player] {
        get { return self.players }
    }
    
    func getPlayersStartingWith(initialPlayer: Player) -> [Player] {
        
        var players: [Player] = []
var player = initialPlayer


        repeat {
            players.append(player)
            player = player.nextPlayer
        } while player != initialPlayer
        
        return players
    }
}
}