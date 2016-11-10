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

            System.Console.ReadLine();
        }

        private static List<Player> CreateNoobs()
        {
            return new List<Player>
                {
                    new Player("A", new TerribleRandomAiAgent()),
                    new Player("B", new TerribleRandomAiAgent()),
                    new Player("C", new TerribleRandomAiAgent()),
                    new Player("D", new TerribleRandomAiAgent())
                };
        }
    }
}
