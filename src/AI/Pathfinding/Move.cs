using System;

namespace MastermindRPG.AI.Pathfinding
{
    /// <summary>
    /// Move object
    /// </summary>
    class Move
    {
        /// <summary>
        /// horizontal coordinate
        /// </summary>
        private int x;

        /// <summary>
        /// vertical coordinate
        /// </summary>
        private int y;

        /// <summary>
        /// horizontal coordinate property
        /// </summary>
        public int X
        {
            get { return x; }
        }

        /// <summary>
        /// vertical coordinate property
        /// </summary>
        public int Y
        {
            get { return y; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        public Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
