using System;
using System.Collections.Generic;

namespace Hearts.Collections
{
    /// <summary>
    /// A list that only adds non-null items.
    /// </summary>
    /// <typeparam name="T">The type of item in the list</typeparam>
    public class NonNullList<T> : SelectiveList<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the NonNullList class.
        /// </summary>
        public NonNullList()
            : base((list, item) => item != null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NonNullList class.
        /// </summary>
        /// <param name="predicate">An additional predicate to apply on insert or add</param>
        public NonNullList(Func<ICollection<T>, T, bool> predicate)
            : base((list, item) => item != null && predicate(list, item))
        {
        }
    }
}