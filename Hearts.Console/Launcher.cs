using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts;

namespace Hearts.Console
{
    public class Launcher
    { 
        public static void Main()
        {
            var game = new Game();

            var card = game.Players.First().Hand.First();

            var test = card.ToAbbreviation();
            int i = game.Players.First().Hand.Count();
        }
    }
}
