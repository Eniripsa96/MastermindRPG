using System;

namespace MastermindRPG.Data.Structures.List
{
    /// <summary>
    /// A simple node with basic storage
    /// </summary>
    /// <typeparam name="T">Node type</typeparam>
    class PairListNode<T, O>
    {
        /// <summary>
        /// Next node in the list
        /// </summary>
        private PairListNode<T, O> next;

        /// <summary>
        /// Key of this pair
        /// </summary>
        private T key;
        
        /// <summary>
        /// Data of this pair
        /// </summary>
        private O value;

        /// <summary>
        /// Key property
        /// </summary>
        public T Key
        {
            get { return key; }
        }

        /// <summary>
        /// Value property
        /// </summary>
        public O Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Next property
        /// </summary>
        public PairListNode<T, O> Next
        {
            get { return next; }
            set { next = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Node's data</param>
        public PairListNode(T key, O value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
