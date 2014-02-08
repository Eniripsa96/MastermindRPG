using System;

using MastermindRPG.Data;

namespace MastermindRPG.Creatures.Skills
{
    /// <summary>
    /// Skill Class: Negotiate
    /// 
    /// Steals three enemy tokens without
    /// dealing damage.
    /// </summary>
    class Negotiate
    {
        /// <summary>
        /// Performs the skill
        /// </summary>
        /// <returns>true if it was successfully cast</returns>
        public static Boolean Perform()
        {
            if (!CanPerform())
                return false;

            // Steals the target token
            Battle.Field[10 * Battle.Y + Battle.X] = Battle.CurrentTurn;

            // Give the player two extra turn if this is the first use
            if (Battle.ExtraTurns == 0)
                Battle.ExtraTurns = 2;

            // Else, deduct an extra turn and consume ap if it is the last turn
            else
            {
                Battle.ExtraTurns--;
                if (Battle.ExtraTurns == 0)
                {
                    Battle.ConsumeAp(Constants.IntValue("skillNegotiateAPCost"));
                    Battle.Damage(Constants.IntValue("skillNegotiateDamage"), false);
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the move can be performed
        /// </summary>
        /// <returns>can be performed</returns>
        public static Boolean CanPerform()
        {
            // Cannot cast the skill if the token is not an enemy token or if the player doesn't
            // have enough action points
            if (Battle.Field[10 * Battle.Y + Battle.X] != 3 - Battle.CurrentTurn || Battle.Ap < Constants.IntValue("skillNegotiateAPCost"))
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
            int bonus = 0;
            int x = Battle.X;
            int y = Battle.Y;
            if ((x == 0 || x == 9) && (y == 0 || y == 9))
                bonus = Constants.IntValue("battleRatingCorner");
            else if (x == 0 || x == 9 || y == 0 || y == 9)
                bonus = Constants.IntValue("battleRatingEdge");
            if (Math.Abs(x - y) == 8 || x + y == 1 || x + y == 17 || ((x == 1 || x == 8) && (y == 1 || y == 8)))
                bonus = Constants.IntValue("battleRatingNearCorner");

            return Constants.IntValue("skillNegotiateDamage") + 3 + -2 * Constants.IntValue("skillNegotiateAPCost") + bonus;
        }
    }
}
