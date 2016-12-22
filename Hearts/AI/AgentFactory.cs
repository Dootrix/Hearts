using System;
using System.Collections.Generic;

namespace Hearts.AI
{
    public class AgentFactory
    {
        private readonly AgentOptions options;
        private readonly Type botType;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentFactory"/> class.
        /// </summary>
        /// <param name="factoryMethod">A function that creates an IAgent of your intended bot.</param>
        public AgentFactory(Type botType)
        {
            this.botType = botType;
            this.options = new AgentOptions();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentFactory"/> class.
        /// </summary>
        /// <param name="factoryMethod">A function that creates an IAgent of your intended bot.</param>
        /// <param name="options">The options.</param>
        public AgentFactory(Type botType, AgentOptions options)
        {
            this.botType = botType;
            this.options = options;
        }

        public AgentOptions Options { get { return this.options; } }

        public string AgentName { get { return this.Create().AgentName; } }

        public IAgent Create()
        {
            var agent = Agent.CreateAgent(this.botType);
            
            var parallelAgent = agent as ISupportsParallelOption;

            if (parallelAgent != null)
            {
                parallelAgent.ParallelEnabled = this.options.ParallelEnabled;
            }

            var shootingAgent = agent as ISupportsIntentionalShootingOption;

            if (shootingAgent != null)
            {
                shootingAgent.IntentionalShootingEnabled = this.options.ParallelEnabled;
            }

            var shootDisruptingAgent = agent as ISupportsShootingDisruptionOption;

            if (shootDisruptingAgent != null)
            {
                shootDisruptingAgent.ShootingDisruptionEnabled = this.options.ParallelEnabled;
            }

            return agent;
        }
    }
}
