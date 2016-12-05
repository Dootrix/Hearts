using System;

namespace Hearts.Events
{
    public class EventArg<T> : EventArgs
    {
        private readonly T data;

        public EventArg(T data)
        {
            this.data = data;
        }

        public T Data
        {
            get { return this.data; }
        }
    }
}
