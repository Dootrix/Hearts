using Hearts.AI;
using Hearts.Extensions;
using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Console
{
    class BotHelper
    {
        public static IEnumerable<Bot> GetGameBots()
        {
            var chosenBots = Settings.GameBots();
            var availableBots = Agent.GetAvailableAgents()
                .OrderBy(x => x.AgentName)
                .ToList();

            while (chosenBots.Count < 4)
            {
                System.Console.WriteLine("Please pick a bot for seat {0}", chosenBots.Count + 1);
                
                availableBots.ForEach((item, i) =>
                {
                    System.Console.WriteLine("{0}: {1}", i + 1, item.AgentName);
                });
                
                var request = System.Console.ReadLine();
                var chosenBot = request.ToNullableInt(null);
                
                if (chosenBot.HasValue && chosenBot.Value <= availableBots.Count)
                {
                    chosenBots.Add(new AgentFactory(() => Agent.CreateAgent(availableBots[chosenBot.Value - 1].GetType()), new AgentOptions()));
                }

                System.Console.WriteLine();
            }

            return chosenBots;
        }
    }
}
