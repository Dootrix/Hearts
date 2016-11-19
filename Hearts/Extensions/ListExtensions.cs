using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Extensions
{
    public static class ListExtensions
    {
        // TODO: Allow seeding of this randomization
        private static readonly Random PrivateRandom = new Random();

        public static T Random<T>(this IEnumerable<T> self)
        {
            return self.ElementAt(PrivateRandom.Next(0, self.Count()));
        }

        public static IEnumerable<T> RandomSelection<T>(this IEnumerable<T> values, int nRandomElements)
        {
            var valueList = values.ToList();

            if (nRandomElements >= valueList.Count)
            {
                nRandomElements = valueList.Count - 1;
            }
            
            int[] indexes = Enumerable.Range(0, valueList.Count).ToArray();            
            var results = new List<T>();
            
            for (int i = 0; i < nRandomElements; i++)
            {
                int j = PrivateRandom.Next(i, valueList.Count);

                // Swap the values.
                int temp = indexes[i];
                indexes[i] = indexes[j];
                indexes[j] = temp;
                
                results.Add(valueList[indexes[i]]);
            }
            
            return results;
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