using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Collections
{
    /// <summary>
    /// A list that only adds items if the item is not already in the list.
    /// </summary>
    /// <typeparam name="T">The type of item in the list</typeparam>
    public class UniqueList<T> : SelectiveList<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the UniqueList class.
        /// </summary>
        public UniqueList()
            : base((list, item) =>
            {
                return list.All(_ => _ != item);
            })
        {
        }

        /// <summary>
        /// Initializes a new instance of the UniqueList class.
        /// </summary>
        /// <param name="predicate">An additional predicate to apply on insert or add</param>
        public UniqueList(Func<ICollection<T>, T, bool> predicate)
            : base((list, item) =>
            {
                return list.All(_ => _ != item) && predicate(list, item);
            })
        {
        }
    }
}