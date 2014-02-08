using System;

namespace MastermindRPG.Data.Structures.Stack
{
    /// <summary>
    /// A generic, singly linked node
    /// Derived from E9 (Fall 2012) by David Schwartz
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class StackNode<T>
    {
        /// <summary>
        /// The node's data
        /// </summary>
        private T data;

        /// <summary>
        /// A reference to the next node
        /// </summary>
        private StackNode<T> next;

        /// <summary>
        /// A property for the node's data
        /// </summary>
        public T Data {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// A property for the node's next
        /// </summary>
        public StackNode<T> Next {
            get { return next; }
            set { next = value; }
        }

        /// <summary>
        /// Constructs a node with a data element, and null for the next node
        /// </summary>
        /// <param name="data">The data element</param>
        public StackNode(T data) : this(data, null) { }

        /// <summary>
        /// Constructs a node with a data element and a next node
        /// </summary>
        /// <param name="data">The data element</param>
        /// <param name="next">The next node</param>
        public StackNode(T data, StackNode<T> next) {
            this.data = data;
            this.next = next;
        }
    }
}
