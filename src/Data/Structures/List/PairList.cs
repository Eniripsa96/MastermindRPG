using System;
using System.Collections;
using System.Collections.Generic;

namespace MastermindRPG.Data.Structures.List
{
    /// <summary>
    /// A paired linked list class
    /// </summary>
    /// <typeparam name="T">key type</typeparam>
    /// <typeparam name="O">value type</typeparam>
    class PairList<T, O> : IEnumerable<PairListNode<T, O>>
    {
        /// <summary>
        /// Start of the list
        /// </summary>
        private PairListNode<T, O> front;

        /// <summary>
        /// End of the list
        /// </summary>
        private PairListNode<T, O> end;

        /// <summary>
        /// Size of the list
        /// </summary>
        private int size;

        /// <summary>
        /// Property for the size
        /// </summary>
        public int Size
        {
            get { return size; }
        }
        
        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        public O this[string key]
        {
            get
            {
                PairListNode<T, O> temp = null;
                try
                {
                    for (temp = front; !temp.Key.Equals(key); temp = temp.Next) ;
                }
                catch (Exception) { }
                return temp.Value;
            }
            set
            {
                PairListNode<T, O> temp;
                for (temp = front; !temp.Key.Equals(key); temp = temp.Next) ;
                temp.Value = value;
            }
        }

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>value</returns>
        public O this[int index]
        {
            get
            {
                PairListNode<T, O> temp = front;
                for (int i = 0; i < index; ++i)
                    temp = temp.Next;
                return temp.Value;
            }
            set
            {
                PairListNode<T, O> temp = front;
                for (int i = 0; i < index; ++i)
                    temp = temp.Next;
                temp.Value = value;
            }
        }

        /// <summary>
        /// Appends the element to the end of the list
        /// </summary>
        /// <param name="data">element</param>
        public void Add(T key, O value)
        {
            if (Empty())
            {
                front = new PairListNode<T, O>(key, value);
                end = front;
            }
            else
            {
                end.Next = new PairListNode<T, O>(key, value);
                end = end.Next;
            }
            size++;
        }

        /// <summary>
        /// Clears the list
        /// </summary>
        public void Clear()
        {
            front = null;
            size = 0;
        }

        /// <summary>
        /// Empty method
        /// </summary>
        /// <returns>list is empty</returns>
        public Boolean Empty()
        {
            return size == 0;
        }

        /// <summary>
        /// Enumeration method
        /// </summary>
        /// <returns></returns>
        public IEnumerator<PairListNode<T, O>> GetEnumerator()
        {
            var node = front;
            while (node != null)
            {
                yield return node;
                node = node.Next;
            }
        }

        /// <summary>
        /// Enumeration accessor
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
