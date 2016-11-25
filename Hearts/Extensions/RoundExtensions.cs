using Hearts.Model;
using System.Linq;

namespace Hearts.Extensions
{ 
    public static class RoundExtensions
    {
        public static bool HasCardBeenPlayed(this Round self, Card card, bool includeCurrentTrick = false)
        {
            var cards = self.PlayedTricks.SelectCards();
            if (includeCurrentTrick)
            {
                cards = cards.Union(self.CurrentTrick.SelectCards());
            }

            return cards.Any(_ => _ == card);
        }
    }
}