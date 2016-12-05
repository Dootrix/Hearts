using System.Collections.Generic;
using System.Linq;

namespace Hearts.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> elements)
        {
            var orderedElements = elements.ToArray();

            if (orderedElements.Length == 1)
            {
                yield return new[] { orderedElements[0] };
            }
            else
            {
                foreach (var element in orderedElements)
                {
                    foreach (var subElements in Permute(elements.Except(new[] { element })))
                    {
                        yield return new[] { element }.Concat(subElements);
                    }
                }
            }
        }
    }
}
