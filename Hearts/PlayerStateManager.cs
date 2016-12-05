using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts
{
    public class PlayerStateManager
    {
        private readonly Dictionary<Player, PlayerState> playerStateLookup = new Dictionary<Player, PlayerState>();

        public void SetStartingHands(IEnumerable<CardHand> startingHands)
        {
            this.playerStateLookup.Clear();

            foreach (var startingHand in startingHands)
            {
                var playerState = new PlayerState
                {
                    Starting = startingHand.ToArray(),
                    Current = startingHand
                };

                this.playerStateLookup[startingHand.Owner] = playerState;
            }
        }

        public void SetPostPassHands(IEnumerable<CardHand> postPassHands)
        {
            foreach (var postPassHand in postPassHands)
            {
                var playerState = this.playerStateLookup[postPassHand.Owner];
                playerState.PostPass = postPassHand.ToList();
                playerState.Current = postPassHand;
            }
        }

        public PlayerState GetPlayerState(Player player)
        {
            return this.playerStateLookup[player];
        }

        public IEnumerable<CardHand> GetCurrentHands()
        {
            return this.playerStateLookup
                .Select(x => new CardHand(x.Key, x.Value.Current))
                .ToArray();
        }

        public int GetRemainingCardCount()
        {
            return this.GetCurrentHands()
                .Select(x => x.Count())
                .Sum();                
        }
    }
}
