using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts;
using Hearts.Extensions;
using Hearts.Model;
using Hearts.AI;

namespace Hearts.Console
{
    public class Launcher
    { 
        public static void Main()
        {
            var game = new Game();

            game.AddPlayer(CreateNoob());
            game.AddPlayer(CreateNoob());
            game.AddPlayer(CreateNoob());
            game.AddPlayer(CreateNoob());

            game.Play(0);

            //var card = game.Players.First().RemainingCards.First();
            //string test1 = card.ToString();
            //string test2 = card.DebuggerDisplay;
            //int remainingCardCount = game.Players.First().RemainingCards.Count();
        }

        private static Player CreateNoob()
        {
            return new Player(new NoobAiExampleAgent());
        }
    }
}
