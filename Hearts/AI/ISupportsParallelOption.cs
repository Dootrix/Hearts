using System.Collections.Generic;
using Hearts.Model;

namespace Hearts.AI
{
    // Indicates an agent uses parallel processing internally, and allows disabling of this (thereby forcing it to run synchronously)
    public interface ISupportsParallelOption
    {
        bool ParallelEnabled { get; set; }
    }
}