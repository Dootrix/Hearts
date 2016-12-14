using System.Collections.Generic;
using Hearts.Model;

namespace Hearts.AI
{
    // Indicates that an agent is capable of intentional attempting to shoot the moon, and permits disabling this functionality
    public interface ISupportsIntentionalShootingOption
    {
        bool IntentionalShootingEnabled { get; set; }
    }
}