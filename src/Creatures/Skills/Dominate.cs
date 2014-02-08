using System;

using MastermindRPG.Data;

namespace MastermindRPG.Creatures.Skills
{
    /// <summary>
    /// Skill Class: Dominate
    /// 
    /// Captures all enemy tokens horizontally
    /// and vertically of the target allied token,
    /// dealing one damage per token captured.
    /// </summary>
    class Dominate
    {
        /// <summary>
        /// Performs the skill
        /// </summary>
        /// <returns>true if it was successfully cast</returns>
        public static Boolean Perform()
        {
            if (!CanPerform())
                return false;

            // Cycles through column and row lining up with the currently
            // selected token, stealing all enemy tokens and dealing damage
            // per token stolen.
            for (int i = 0; i < 10; ++i)
            {
                if (Battle.Field[10 * Battle.Y + i] == 3 - Battle.CurrentTurn)
                {
                    Battle.Field[10 * Battle.Y + i] = Battle.CurrentTurn;
                    Battle.Damage(Constants.IntValue("skillDominateDamagePerToken"), false);
                }
                if (Battle.Field[10 * i + Battle.X] == 3 - Battle.CurrentTurn)
                {
                    Battle.Field[10 * i + Battle.X] = Battle.CurrentTurn;
                    Battle.Damage(Constants.IntValue("skillDominateDamagePerToken"), false);
                }
            }

            // Consume the appropriate amount of battle points
            Battle.ConsumeAp(Constants.IntValue("skillDominateAPCost"));

            return true;
        }

        /// <summary>
        /// Checks if the move can be performed
        /// </summary>
        /// <returns>can be performed</returns>
        public static Boolean CanPerform()
        {
            // Cannot cast the skill if the token is not an ally token or if the player doesn't
            // have enough action points
            if (Battle.Field[10 * Battle.Y + Battle.X] != Battle.CurrentTurn || Battle.Ap < Constants.IntValue("skillDominateAPCost"))
                return false;

            // The move must capture at least one token to work
            for (int i = 0; i < 10; ++i)
                if (Battle.Field[10 * Battle.Y + i] == 3 - Battle.CurrentTurn)
                    return true;
                else if (Battle.Field[10 * i + Battle.X] == 3 - Battle.CurrentTurn)
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
            int rating = -2 * Constants.IntValue("skillDominateAPCost");
            for (int i = 0; i < 10; ++i)
            {
                if (Battle.Field[10 * Battle.Y + i] == 3 - Battle.CurrentTurn)
                    rating += Constants.IntValue("skillDominateDamagePerToken");
                if (Battle.Field[10 * i + Battle.X] == 3 - Battle.CurrentTurn)
                    rating += Constants.IntValue("skillDominateDamagePerToken");
            }

            return rating;
        }
    }
}
