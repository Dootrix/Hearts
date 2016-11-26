using Hearts.Randomisation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Extensions
{
    public static class ListExtensions
    {
        public static T Random<T>(this IEnumerable<T> self)
        {
            return self.ElementAt(StaticRandomAccessor.ControlledRandom.Next(0, self.Count()));
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
                int j = StaticRandomAccessor.ControlledRandom.Next(i, valueList.Count);

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
                var r = StaticRandomAccessor.ControlledRandom.Next(i, count); // TODO: check Next functions same as Range
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
                var r = StaticRandomAccessor.ControlledRandom.Next(i, count); // TODO: check Next functions same as Range
                var tmp = list[i];
                list[i] = list[r];
                list[r] = tmp;
            }

            return list;
        }
    }
}