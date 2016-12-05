using Hearts.AI;
using Hearts.Logging;
using Hearts.Model;
using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;
using System;
using Hearts.Console.Simulations;

namespace Hearts.Console
{
    public class Program
    {
        public static void Main()
        {
            if (!Settings.ShowFullOutput)
            {
                Log.Options = new SummaryOnlyLogOptions();
            }

            SimulationFactory
                .CreateSimulation()
                .Execute();    

            System.Console.ReadLine();
        }
    }
}
