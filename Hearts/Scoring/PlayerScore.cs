using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;

namespace Hearts.Scoring
{
    public class PlayerScore
    {
        public Player Player { get; set; }
        public int Score { get; set; }
        public int Moonshots { get; set; }
    }
}
