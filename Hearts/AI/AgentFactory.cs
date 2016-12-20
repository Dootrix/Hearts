using System;
using System.Collections.Generic;

namespace Hearts.AI
{
    public class AgentFactory
    {
        private readonly Func<IAgent> factoryMethod;
        private readonly AgentOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentFactory"/> class.
        /// </summary>
        /// <param name="factoryMethod">A function that creates an IAgent of your intended bot.</param>
        public AgentFactory(Func<IAgent> factoryMethod)
        {
            this.factoryMethod = factoryMethod;
            this.options = new AgentOptions();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentFactory"/> class.
        /// </summary>
        /// <param name="factoryMethod">A function that creates an IAgent of your intended bot.</param>
        /// <param name="options">The options.</param>
        public AgentFactory(Func<IAgent> factoryMethod, AgentOptions options)
        {
            this.factoryMethod = factoryMethod;
            this.options = options;
        }

        public AgentOptions Options { get { return this.options; } }

        public string AgentName { get { return this.CreateAgentInstance().AgentName; } }

        public IAgent CreateAgentInstance()
        {
            var agent = this.factoryMethod();
            
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
