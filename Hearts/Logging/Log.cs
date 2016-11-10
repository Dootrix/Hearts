using Hearts.Model;
using Hearts.Scoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Logging
{
    public static class Log
    {
        public static void StartingHands(Dictionary<Player, List<Card>> hands)
        {
            Console.WriteLine("Starting Hands:");

            foreach (var hand in hands)
            {
                Console.WriteLine(hand.Key.DebuggerDisplay);
            }

            Console.WriteLine(string.Empty);
        }

        public static void HandsAfterPass(Dictionary<Player, List<Card>> hands)
        {
            Console.WriteLine("Post-Pass Hands:");

            foreach (var hand in hands)
            {
                Console.WriteLine(hand.Key.DebuggerDisplay);
            }

            Console.WriteLine(string.Empty);
        }

        public static void TrickSummary(PlayedTrick trick)
        {
            foreach (var playedCard in trick.Cards)
            {
                Console.WriteLine(playedCard.Key.Name + " plays " + playedCard.Value.DebuggerDisplay + (trick.Winner == playedCard.Key ? " Winner" : string.Empty));
            }

            Console.WriteLine(string.Empty);
        }

        public static void IllegalPass(Player player, IEnumerable<Card> cards)
        {
            Console.WriteLine(player.Name + " made an ILLEGAL PASS! (" + string.Join(", ", cards.Select(i => i.DebuggerDisplay)) + ") - " + player.DebuggerDisplay);
        }

        public static void IllegalPlay(Player player, Card card)
        {
            Console.WriteLine(player.Name + " played an ILLEGAL CARD! (" + card.DebuggerDisplay + ") - " + player.DebuggerDisplay);
        }

        public static void PointsForRound(Dictionary<Player, int> scores)
        {
            foreach (var score in scores)
            {
                Console.WriteLine(score.Key.Name + ": " + score.Value + "pts");
            }

            Console.WriteLine(string.Empty);
        }
    }
}
