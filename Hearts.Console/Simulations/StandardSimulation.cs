
namespace Hearts.Console.Simulations
{
    public class StandardSimulation : ISimulation
    {
        public void Execute()
        {
            new Launcher()
                .ExecuteSimulations(new[] { BotHelper.GetGameBots() });
        }
    }
}
