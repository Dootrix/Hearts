using System;
using System.Collections.Generic;

namespace Hearts.AI
{
    public class AgentOptions
    {
        public AgentOptions()
        {
            this.IntentionalShootingEnabled = true;
            this.ShootingDisruptionEnabled = true;
            this.ParallelEnabled = true;
        }

        public bool IntentionalShootingEnabled { get; set; }
        public bool ShootingDisruptionEnabled { get; set; }
        public bool ParallelEnabled { get; set; }
    }
}
