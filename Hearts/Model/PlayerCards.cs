using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Model
{
    public class PlayerCards
    {
        public IEnumerable<Card> StartingHands { get; set; }
        public IEnumerable<Card> PassedCards { get; set; }
        public IEnumerable<Card> PostPassHands { get; set; }
        public IEnumerable<Card> RemainingCards { get; set; }
        public IEnumerable<Card> LegalCards { get; set; }
    }
}
