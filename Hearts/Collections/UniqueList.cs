using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Collections
{
    public class UniqueList<T> : SelectiveList<T>
        where T : class
    {
        public UniqueList()
            : base((list, item) =>
            {
                return list.All(_ => _ != item);
            })
        {
        }

        public UniqueList(Func<ICollection<T>, T, bool> predicate)
            : base((list, item) =>
            {
                return list.All(_ => _ != item) && predicate(list, item);
            })
        {
        }
    }
}