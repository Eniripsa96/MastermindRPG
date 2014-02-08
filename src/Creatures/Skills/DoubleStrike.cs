using System;

using MastermindRPG.Data;

namespace MastermindRPG.Creatures.Skills
{
    /// <summary>
    /// Skill Class: Double Strike
    /// 
    /// Places a token at location pointed to
    /// by the Battle class then allows the
    /// user to go again.
    /// </summary>
    class DoubleStrike
    {
        /// <summary>
        /// Performs the skill
        /// </summary>
        /// <returns>true if it was successfully cast</returns>
        public static Boolean Perform()
        {
            if (!CanPerform())
                return false;

            PlaceToken.Perform();

            // Give the player an extra turn if this is the first use
            if (Battle.ExtraTurns == 0)
                Battle.ExtraTurns = 1;

            // Else, end the extra turn and consume the ap cost
            else
            {
                Battle.ExtraTurns--;
                Battle.ConsumeAp(Constants.IntValue("skillDoubleStrikeAPCost"));
            }

            return true;
        }

        /// <summary>
        /// Checks if the move can be performed
        /// </summary>
        /// <returns>can be performed</returns>
        public static Boolean CanPerform()
        {
            // Cannot use the skill if the player doesn't have
            // enough action points
            if (Battle.Ap < Constants.IntValue("skillDoubleStrikeAPCost"))
                return false;

            return PlaceToken.CanPerform();
        }

        /// <summary>
        /// Rates the current skill based on how much damage 
        /// or healing it does versus ap cost
        /// </summary>
        /// <returns>rating index</returns>
        public static int Rate()
        {
            return 2 * (PlaceToken.Rate() - Constants.IntValue("skillDoubleStrikeAPCost"));
        }
    }
}
