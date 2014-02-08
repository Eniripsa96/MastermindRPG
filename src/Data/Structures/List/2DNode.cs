using System;

namespace MastermindRPG.Data.Structures.List
{
    /// <summary>
    /// Generic 2-dimensional node
    /// </summary>
    /// <typeparam name="T">Type (Room)</typeparam>
    class _2DNode<T>
    {
        /// <summary>
        /// The next item vertically
        /// </summary>
        private _2DNode<T> down;

        /// <summary>
        /// The next item horizontally
        /// </summary>
        private _2DNode<T> right;

        /// <summary>
        /// The data
        /// </summary>
        private T data;

        /// <summary>
        /// Property for the next item vertically
        /// </summary>
        public _2DNode<T> Down
        {
            get { return down; }
            set { down = value; }
        }

        /// <summary>
        /// Property for the next item horizontally
        /// </summary>
        public _2DNode<T> Right
        {
            get { return right; }
            set { right = value; }
        }

        /// <summary>
        /// Property for the data
        /// </summary>
        public T Data
        {
            get { return data; }
        }

        /// <summary>
        /// Constructs the node with the given data
        /// </summary>
        /// <param name="d">data</param>
        public _2DNode(T d)
        {
            data = d;
        }
    }
}
