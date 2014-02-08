using System;

namespace MastermindRPG.Data.Structures.List
{
    /// <summary>
    /// A simple node with basic storage
    /// </summary>
    /// <typeparam name="T">Node type</typeparam>
    class SimpleNode<T>
    {
        /// <summary>
        /// Next node in the list
        /// </summary>
        private SimpleNode<T> next;

        /// <summary>
        /// Data of this node
        /// </summary>
        private T data;

        /// <summary>
        /// Data property
        /// </summary>
        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// Next property
        /// </summary>
        public SimpleNode<T> Next
        {
            get { return next; }
            set { next = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Node's data</param>
        public SimpleNode(T data)
        {
            this.data = data;
        }
    }
}
