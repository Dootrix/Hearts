using System;
using System.Collections;
using System.Collections.Generic;

namespace Hearts.Collections
{
    /// <summary>
    /// A list that only adds items if a predicate returns true.  This is useful if you want to avoid repetitive
    /// conditional checks in code when adding to a list.
    /// </summary>
    /// <typeparam name="T">The type of item in the list</typeparam>
    public class SelectiveList<T> : IList<T>
    {
        private readonly Func<ICollection<T>, T, bool> predicate;
        private readonly List<T> list = new List<T>();
 
        /// <summary>
        /// Initializes a new instance of the SelectiveList class.
        /// </summary>
        /// <param name="predicate">The predicate to apply on insert or add</param>
        public SelectiveList(Func<ICollection<T>, T, bool> predicate)
        {
            this.predicate = predicate;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            if (this.predicate(this.list.AsReadOnly(), item))
            {
                this.list.Add(item);    
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                // Applies the predicate
                this.Add(item);
            }
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return this.list.Remove(item);
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int IndexOf(T item)
        {
            return this.list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (this.predicate(this.list.AsReadOnly(), item))
            {
                this.list.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return this.list[index]; }
            set { this.list[index] = value; }
        }
    }
}