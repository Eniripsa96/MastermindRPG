using System;

using MastermindRPG.Data;

namespace MastermindRPG.Creatures.Skills
{
    /// <summary>
    /// Skill Class: Betray
    /// 
    /// Steals the token pointed to by the Battle class
    /// and damages the opponent
    /// </summary>
    class Betray
    {
        /// <summary>
        /// Performs the skill
        /// </summary>
        /// <returns>true if it was successfully cast</returns>
        public static Boolean Perform()
        {
            if (!CanPerform())
                return false;

            // Steal the token and deal damage to the enemy after consuming
            // the appropriate amount of ap
            Battle.ConsumeAp(Constants.IntValue("skillBetrayAPCost"));
            Battle.Field[10 * Battle.Y + Battle.X] = Battle.CurrentTurn;
            Battle.Damage(Constants.IntValue("skillBetrayDamage"), false);

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
            if (Battle.Field[10 * Battle.Y + Battle.X] != 3 - Battle.CurrentTurn || Battle.Ap < Constants.IntValue("skillBetrayAPCost"))
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
                bonus = Constants.IntValue("battleRatingCorner") / 2;
            else if (x == 0 || x == 9 || y == 0 || y == 9)
                bonus = Constants.IntValue("battleRatingCorner") / 2;
            if (Math.Abs(x - y) == 8 || x + y == 1 || x + y == 17 || ((x == 1 || x == 8) && (y == 1 || y == 8)))
                bonus = Constants.IntValue("battleRatingNearCorner") / 2;

            return Constants.IntValue("skillBetrayDamage") - 2 * Constants.IntValue("skillBetrayAPCost") + bonus;
        }
    }
}
