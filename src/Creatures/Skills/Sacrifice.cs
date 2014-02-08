using System;

using MastermindRPG.Data;

namespace MastermindRPG.Creatures.Skills
{
    /// <summary>
    /// Skill Class: Sacrifice
    /// 
    /// Gives up the token pointed to by
    /// the Battle class and deals damage
    /// to the enemy
    /// </summary>
    class Sacrifice 
    {
        /// <summary>
        /// Performs the skill
        /// </summary>
        /// <returns>true if it was successfully cast</returns>
        public static Boolean Perform()
        {
            if (!CanPerform())
                return false;

            // Give the token to the enemy and heal the user after subtracting the
            // appropriate amount of action points
            Battle.ConsumeAp(Constants.IntValue("skillSacrificeAPCost"));
            Battle.Field[10 * Battle.Y + Battle.X] = 3 - Battle.CurrentTurn;
            Battle.Damage(Constants.IntValue("skillSacrificeDamage"), false);

            return true;
        }

        /// <summary>
        /// Checks if the move can be performed
        /// </summary>
        /// <returns>can be performed</returns>
        public static Boolean CanPerform()
        {
            // Cannot cast the skill if the token is not owned or if the player doesn't have
            // enough action points
            if (Battle.Field[10 * Battle.Y + Battle.X] != Battle.CurrentTurn || Battle.Ap < Constants.IntValue("skillSacrificeAPCost"))
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
            // Make sure there aren't too few tokens
            if (Battle.Field.CountTokens(Battle.CurrentTurn) <= 7)
                return -999;

            int bonus = 0;
            int x = Battle.X;
            int y = Battle.Y;
            if ((x == 0 || x == 9) && (y == 0 || y == 9))
                bonus = Constants.IntValue("battleRatingCorner");
            else if (x == 0 || x == 9 || y == 0 || y == 9)
                bonus = Constants.IntValue("battleRatingEdge");
            if (Math.Abs(x - y) == 8 || x + y == 1 || x + y == 17 || ((x == 1 || x == 8) && (y == 1 || y == 8)))
                bonus = Constants.IntValue("battleRatingNearCorner");

            return Constants.IntValue("skillSacrificeDamage") - 2 * Constants.IntValue("skillSacrificeAPCost") - bonus;
        }
    }
}
