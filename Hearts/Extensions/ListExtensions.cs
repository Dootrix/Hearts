using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Extensions
{
    public static class ListExtensions
    {
        private static readonly Random PrivateRandom = new Random();

        public static T Random<T>(this IEnumerable<T> self)
        {
            return self.ElementAt(PrivateRandom.Next(0, self.Count())); // TODO: check Next functions same as Range
        }

        public static void Shuffle<T>(this IList<T> self)
        {
            var count = self.Count;
            var last = count - 1;

            for (var i = 0; i < last; ++i)
            {
                var r = PrivateRandom.Next(i, count); // TODO: check Next functions same as Range
                var tmp = self[i];
                self[i] = self[r];
                self[r] = tmp;
            }
        }

        public static List<T> RandomOrder<T>(this IEnumerable<T> self)
        {
            var list = self.ToList();

            var count = list.Count;
            var last = count - 1;

            for (var i = 0; i < last; ++i)
            {
                var r = PrivateRandom.Next(i, count); // TODO: check Next functions same as Range
                var tmp = list[i];
                list[i] = list[r];
                list[r] = tmp;
            }

            return list;
        }
    }
}