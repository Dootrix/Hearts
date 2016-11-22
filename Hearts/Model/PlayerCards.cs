using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Model
{
    public class PlayerCards
    {
        public IEnumerable<Card> Starting { get; set; }
        public IEnumerable<Card> Passed { get; set; }
        public IEnumerable<Card> PostPass { get; set; }
        public IEnumerable<Card> CurrentRemaining { get; set; }
        public IEnumerable<Card> LegalPlays { get; set; }
    }
}
