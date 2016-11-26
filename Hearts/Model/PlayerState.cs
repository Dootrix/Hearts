using System.Collections.Generic;
using System.Linq;

namespace Hearts.Model
{
    public class PlayerState
    {
        public IEnumerable<Card> Starting { get; set; }
        public IEnumerable<Card> Passed { get { return this.Starting.Except(this.PostPass); } }
        public IEnumerable<Card> PostPass { get; set; }
        public IEnumerable<Card> Current { get; set; }
        public IEnumerable<Card> Legal { get; set; }
    }
}
