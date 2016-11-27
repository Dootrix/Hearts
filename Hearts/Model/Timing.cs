using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Model
{
    public class Timing
    {
        public Timing(IEnumerable<Bot> bots)
        {
            this.PassTimings = bots.ToDictionary(i => i.Player, i => new List<int>());
            this.PlayTimings = bots.ToDictionary(i => i.Player, i => new List<int>());
        }

        public Dictionary<Player, List<int>> PassTimings;
        public Dictionary<Player, List<int>> PlayTimings;
    }
}
