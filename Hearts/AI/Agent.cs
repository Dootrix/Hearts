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
            var agentTypes = AppDomain.CurrentDomain.ResolveInterfaceImplementations<IAgent>(true);

            foreach (var agentType in agentTypes)
            {
                IAgent agent = null;
                try
                {
                    agent = Activator.CreateInstance(agentType) as IAgent;
                }
                catch (MemberAccessException)
                {
                }

                if (agent != null)
                {
                    availableAgents.Add(agent);
                }
            }

            return availableAgents;
        }
    }
}
