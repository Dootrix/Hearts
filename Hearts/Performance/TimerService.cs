using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Performance
{
    public class TimerService : ITimerRecord
    {
        private readonly Dictionary<Player, List<int>> passTimings;
        private readonly Dictionary<Player, List<int>> playTimings;

        public TimerService(IEnumerable<Bot> bots)
        {
            this.passTimings = bots.ToDictionary(i => i.Player, i => new List<int>());
            this.playTimings = bots.ToDictionary(i => i.Player, i => new List<int>());
        }

        void ITimerRecord.RecordPassTime(Player player, long milliseconds)
        {
            this.passTimings[player].Add(Convert.ToInt32(milliseconds));
        }

        void ITimerRecord.RecordPlayTime(Player player, long milliseconds)
        {
            this.playTimings[player].Add(Convert.ToInt32(milliseconds));
        }

        public ITimer StartNewPlayTimer(Player player)
        {
            return new PlayTimer(player, this);
        }

        public double GetAveragePlayTiming(Player player)
        {
            return this.playTimings[player].Average();
        }

        public double GetAveragePassTiming(Player player)
        {
            return this.passTimings[player].Average();
        }

        public ITimer StartNewPassTimer(Player player)
        {
            return new PassTimer(player, this);
        }
    }

    public interface ITimerRecord
    {
        void RecordPassTime(Player player, long milliseconds);
        void RecordPlayTime(Player player, long milliseconds);
    }
    
}
