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
