using Hearts.Model;
using System.Diagnostics;

namespace Hearts.Performance
{
    class PlayTimer : ITimer
    {
        private readonly Stopwatch stopwatch;
        private readonly Player player;
        private readonly ITimerRecord timerRecord;

        public PlayTimer(Player player, ITimerRecord timerRecord)
        {
            this.player = player;
            this.timerRecord = timerRecord;
            this.stopwatch = Stopwatch.StartNew();
        }

        public void Stop()
        {
            this.stopwatch.Stop();
            this.timerRecord.RecordPlayTime(this.player, this.stopwatch.ElapsedMilliseconds);
        }
    }
}
