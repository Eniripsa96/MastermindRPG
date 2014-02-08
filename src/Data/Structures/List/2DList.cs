using System;

namespace MastermindRPG.Data.Structures.List
{
    /// <summary>
    /// Holds a 2D list of items via Linked-List format
    /// </summary>
    class _2DRoomList<T>
    {
        /// <summary>
        /// The first item (at {0, 0})
        /// </summary>
        protected _2DNode<T> first;

        /// <summary>
        /// The last item
        /// </summary>
        protected _2DNode<T> last;

        /// <summary>
        /// The current list size
        /// </summary>
        protected int size;

        /// <summary>
        /// The horizontal size of the list
        /// </summary>
        protected int length;

        /// <summary>
        /// The vertical size of the list
        /// </summary>
        protected int width;

        /// <summary>
        /// Returns the item at the given coordinates
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        /// <returns>given item</returns>
        public T this[int x, int y]
        {
            get
            {
                _2DNode<T> node = first;
                for (int z = 0; z < x; ++z)
                    node = node.Right;
                for (int z = 0; z < y; ++z)
                    node = node.Down;
                return node.Data;
            }
        }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public _2DRoomList()
        {
            size = 0;
        }

        /// <summary>
        /// Adds another room to the map
        /// </summary>
        /// <param name="data">the room object</param>
        public void Add(T data)
        {
            // Make it the head if the map hasn't been started
            if (first == null)
            {
                first = new _2DNode<T>(data);
                last = first;
            }

            // Otherwise, append it
            else
            {
                last.Right = new _2DNode<T>(data);
                last = last.Right;
            }
            size++;
        }

        /// <summary>
        /// Arrange the linear list into a 2-dimensional list
        /// </summary>
        /// <param name="l">horizontal size of the list</param>
        /// <param name="w">vertical size of the list</param>
        public void Arrange(int l, int w)
        {
            length = l;
            width = w;
            _2DNode<T> temp = first;
            _2DNode<T> temp2 = first;
            for (int x = 0; x < l; ++x)
                temp2 = temp2.Right;
            for (int x = 0; x < l * w; ++x)
            {
                _2DNode<T> right = temp.Right;
                if (x % l == l - 1)
                    temp.Right = null;
                if (temp2 != null)
                {
                    temp.Down = temp2;
                    temp = right;
                    temp2 = temp2.Right;
                }
            }
        }
    }
}