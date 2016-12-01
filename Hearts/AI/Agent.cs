using System;
using System.Collections.Generic;
using Hearts.Reflection;

namespace Hearts.AI
{
    public static class Agent
    {
        public static IEnumerable<IAgent> GetAvailableAgents()
        {
            var availableAgents = new List<IAgent>();

            foreach (var agentType in GetAvailableAgentTypes())
            {
                var agent = CreateAgent(agentType);

                if (agent != null)
                {
                    availableAgents.Add(agent);
                }
            }

            return availableAgents;
        }

        public static IEnumerable<Type> GetAvailableAgentTypes()
        {
            return AppDomain.CurrentDomain.ResolveInterfaceImplementations<IAgent>(true);
        }

        public static IAgent CreateAgent(Type agentType)
        {
            IAgent agent = null;
            try
            {
                agent = Activator.CreateInstance(agentType) as IAgent;
            }
            catch (MemberAccessException)
            {
            }

            return agent;
        }
    }
}
