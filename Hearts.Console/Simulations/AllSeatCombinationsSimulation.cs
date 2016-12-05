using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Extensions;

namespace Hearts.Console.Simulations
{
    public class AllSeatCombinationsSimulation : ISimulation
    {
        public void Execute()
        {
            var seatingCombinations = BotHelper.GetGameBots().Permute().ToArray();

            new Launcher()
                .ExecuteSimulations(seatingCombinations);
        }
    }
}
