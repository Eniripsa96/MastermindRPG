using System;

using MastermindRPG.Data;

namespace MastermindRPG.Creatures.Skills
{
    /// <summary>
    /// Skill Class: Void
    /// 
    /// Erases all tokens within a 3x3 area of the target area
    /// </summary>
    class Void
    {
        /// <summary>
        /// Performs the skill
        /// </summary>
        /// <returns>true if it was successfully cast</returns>
        public static Boolean Perform()
        {
            if (!CanPerform())
                return false;

            // Search through the 3x3 area around the target location
            // and remove each token found.
            for (int y = Battle.Y - 1; y <= Battle.Y + 1; ++y)
                for (int x = Battle.X - 1; x <= Battle.X + 1; ++x)
                    if (x >= 0 && y >= 0 && x < 10 && y < 10)
                        if (Battle.Field[10 * y + x] != 0)
                        {
                            Battle.Damage(Constants.IntValue("skillVoidDamagePerToken"), Battle.Field[10 * y + x] == Battle.CurrentTurn);
                            Battle.Field[10 * y + x] = 0;
                        }

            // Consume the appropriate amount of ap
            Battle.ConsumeAp(Constants.IntValue("skillVoidAPCost"));

            return true;
        }

        /// <summary>
        /// Checks if the move can be performed
        /// </summary>
        /// <returns>can be performed</returns>
        public static Boolean CanPerform()
        {
            // Cannot use the skill if the player doesn't
            // have enough stat points.
            if (Battle.Ap < Constants.IntValue("skillVoidAPCost"))
                return false;

            // This must remove at least one token to work
            for (int y = Battle.Y - 1; y <= Battle.Y + 1; ++y)
                for (int x = Battle.X - 1; x <= Battle.X + 1; ++x)
                    if (x >= 0 && y >= 0 && x < 10 && y < 10)
                        if (Battle.Field[10 * y + x] != 0)
                            return true;

            return false;
        }

        /// <summary>
        /// Rates the current skill based on how much damage 
        /// or healing it does versus ap cost
        /// </summary>
        /// <returns>rating index</returns>
        public static int Rate()
        {
            int rating = 0;
            int loss = 0;
            for (int y = Battle.Y - 1; y <= Battle.Y + 1; ++y)
                for (int x = Battle.X - 1; x <= Battle.X + 1; ++x)
                    if (x >= 0 && y >= 0 && x < 10 && y < 10)
                        if (Battle.Field[10 * y + x] == Battle.CurrentTurn)
                        {
                            rating--;
                            loss++;
                        }
                        else if (Battle.Field[10 * y + x] == 3 - Battle.CurrentTurn)
                            rating++;

            // Make sure it doesn't surrender all remaining tokens
            if (Battle.Field.CountTokens(Battle.CurrentTurn) <= loss)
                return -999;

            return rating - 2 * Constants.IntValue("skillVoidAPCost");
        }
    }
}
