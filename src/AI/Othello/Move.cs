using System;

namespace MastermindRPG.AI.Othello
{
    /// <summary>
    /// Holds the data for a move
    /// </summary>
    class Move
    {
        /// <summary>
        /// Location of the move
        /// </summary>
        private int location;

        /// <summary>
        /// ID of the associated skill
        /// </summary>
        private int action;

        /// <summary>
        /// Returns the location
        /// </summary>
        public int Location
        {
            get { return location; }
        }

        /// <summary>
        /// Returns the ID for the associated skill
        /// </summary>
        public int Action
        {
            get { return action; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">location of the move</param>
        /// <param name="action">associated skill ID</param>
        public Move(int location, int action)
        {
            this.location = location;
            this.action = action;
        }
    }
}
