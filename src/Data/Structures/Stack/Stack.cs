using System;

namespace MastermindRPG.Data.Structures.Stack
{
    /// <summary>
    /// A node based implementation of a stack
    /// </summary>
    /// <typeparam name="T">The data type the NodeStack holds</typeparam>
    class Stack<T>
    {
        /// <summary>
        /// A reference to the top element in the stack
        /// </summary>
        private StackNode<T> top;

        /// <summary>
        /// Adds an element to the stack
        /// </summary>
        /// <param name="data">The element to add</param>
        public void Push(T data)
        {
            if (Empty())
                top = new StackNode<T>(data);
            else
            {
                StackNode<T> temp = top;
                top = new StackNode<T>(data);
                top.Next = temp;
            }
        }

        /// <summary>
        /// Quietly removes the top element on the stack
        /// </summary>
        /// <exception>Throws an UnderflowException if the stack is empty</exception>
        public T Pop()
        {
            if (Empty())
                throw new Exception("Stack is empty!");
            else
            {
                T t = top.Data;
                top = top.Next;
                return t;
            }
        }

        /// <summary>
        /// Indicates whether the stack is empty or not
        /// </summary>
        /// <returns>True if the stack is empty, false otherwise</returns>
        public bool Empty()
        {
            return top == null;
        }

        /// <summary>
        /// Returns if the list is almost empty or is empty
        /// </summary>
        /// <returns>true if size is less than 2, false otherwise</returns>
        public Boolean NearEmpty()
        {
            if (top == null)
                return true;
            else
                return top.Next == null;
        }

        /// <summary>
        /// Clears the stack
        /// </summary>
        public void Clear()
        {
            top = null;
        }
    }
}
