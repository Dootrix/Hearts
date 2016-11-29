using System;
using Hearts.Randomisation;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Extensions
{
    public static class ListExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> self, Action<T, int> action)
        {
            var i = 0;
            foreach (var item in self)
            {
                action(item, i++);
            }
        }

        public static void Shuffle<T>(this IList<T> self)
        {
            var count = self.Count;
            var last = count - 1;

            for (var i = 0; i < last; ++i)
            {
                var r = StaticRandomAccessor.ControlledRandoms[0].Next(i, count);
                var tmp = self[i];
                self[i] = self[r];
                self[r] = tmp;
            }
        }

        public static void Shuffle<T>(this IList<T> self, IControlledRandom random)
        {
            var count = self.Count;
            var last = count - 1;

            for (var i = 0; i < last; ++i)
            {
                var r = random.Next(i, count);
                var tmp = self[i];
                self[i] = self[r];
                self[r] = tmp;
            }
        }
    }
}