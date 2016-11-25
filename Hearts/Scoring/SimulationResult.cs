using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;

namespace Hearts.Scoring
{
    public class SimulationResult
    {
        public SimulationResult()
        {
            this.GameResults = new List<GameResult>();
        }

        public List<GameResult> GameResults { get; set; }
    }
}
