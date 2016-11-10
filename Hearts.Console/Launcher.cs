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
            var game = new Game(CreateNoobs());
            var result = game.Play(0);
        }

        private static List<Player> CreateNoobs()
        {
            return new List<Player>
                {
                    new Player("A v1", new Noob1AiExampleAgent()),
                    new Player("B v1", new Noob1AiExampleAgent()),
                    new Player("C v1", new Noob1AiExampleAgent()),
                    new Player("D v2", new Noob2AiExampleAgent())
                };
        }
    }
}
