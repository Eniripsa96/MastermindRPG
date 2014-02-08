using System;

using MastermindRPG.Data;
using MastermindRPG.Data.Structures.Stack;

namespace MastermindRPG.Creatures
{
    /// <summary>
    /// Enemy Class: Minion
    /// 
    /// Creates an enemy with minion stats
    /// </summary>
    class Minion : Enemy
    {
        /// <summary>
        /// List of moves to follow
        /// </summary>
        private Stack<int[]> moves;

        /// <summary>
        /// Room coordinates where this enemy is
        /// </summary>
        private int[] room;

        /// <summary>
        /// Position before moving
        /// </summary>
        private int[] previousPosition;

        /// <summary>
        /// Property for position that saves the previous position when changed
        /// </summary>
        public new int[] Position
        {
            get { return new int[] {horizontalPosition, verticalPosition}; }
            set 
            {
                previousPosition = new int[] { horizontalPosition, verticalPosition }; 
                horizontalPosition = value[0]; 
                verticalPosition = value[1];
            }
        }

        /// <summary>
        /// Property for moves
        /// </summary>
        public Stack<int[]> Moves
        {
            get { return moves; }
            set { moves = value; }
        }

        /// <summary>
        /// Property for room
        /// </summary>
        public int[] Room
        {
            get { return room; }
            set { room = value; }
        }

        /// <summary>
        /// Minion Constructor
        /// </summary>
        /// <param name="id">the area id</param>
        public Minion(int id)
            : base(
                id == -1 ? 999 : id * Constants.IntValue("minionHealthMultiplier") + Constants.IntValue("minionHealthBonus"),         // Health
                id == -1 ? 999 : id * Constants.IntValue("minionLevelMultiplier") + Constants.IntValue("minionLevelBonus"),            // Level
                id == -1 ?   0 : id * Constants.IntValue("minionExperienceMultiplier") + Constants.IntValue("minionExperienceBonus")) // Experience
        {
            moves = new Stack<int[]>();
        }

        /// <summary>
        /// Goes back to the previous position
        /// </summary>
        public void GoBack()
        {
            Position = previousPosition;
        }

        /// <summary>
        /// Compares to another minion (for collision detection)
        /// </summary>
        /// <param name="obj">other minion</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Minion m = (obj as Minion);
            if (m == null)
                throw new ArgumentException("Non-minions can't be compared to minions!");
            return (m.horizontalPosition == horizontalPosition && m.verticalPosition == verticalPosition && m.room[0] == room[0] && m.room[1] == room[1]);
        }
    }
}
