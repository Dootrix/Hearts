using System.Collections.Generic;
using Hearts.Model;

namespace Hearts.AI
{
    // Indicates that an agent is capable of attempting to prevent a shoot, and permits this strategy being disabled
    public interface ISupportsShootingDisruptionOption
    {
        bool ShootingDisruptionEnabled { get; set; }
    }
}