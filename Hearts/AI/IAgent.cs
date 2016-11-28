namespace Hearts.AI
{
    public interface IAgent : IPassStrategy, IPlayStrategy
    {
        // A fixed (optionally arbitrary) name for your AI, that allows other AIs to adjust to it
        string AgentName { get; }
    }
}
