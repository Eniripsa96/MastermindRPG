using System;

using MastermindRPG.Data;

namespace MastermindRPG.Creatures.Skills
{
    /// <summary>
    /// Skill Class: Rewind
    /// 
    /// Resets the board along with 
    /// both player's AP to default.
    /// </summary>
    class Rewind
    {
        /// <summary>
        /// Performs the skill
        /// </summary>
        /// <returns>true if it was successfully cast</returns>
        public static Boolean Perform()
        {
            if (!CanPerform())
                return false;

            // Resets the board and action points
            // for each player
            Battle.Reset();
            return true;
        }

        /// <summary>
        /// Checks if the move can be performed
        /// </summary>
        /// <returns>can be performed</returns>
        public static Boolean CanPerform()
        {
            // Cannot use the skill if the player
            // doesn't have enough action points
            if (Battle.Ap < Constants.IntValue("skillRewindAPCost"))
                return false;

            return true;
        }

        /// <summary>
        /// Rates the current skill based on how much damage 
        /// or healing it does versus ap cost
        /// </summary>
        /// <returns>rating index</returns>
        public static int Rate()
        {
            return -15;
        }
    }
}
