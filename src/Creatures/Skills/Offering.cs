using System;

using MastermindRPG.Data;

namespace MastermindRPG.Creatures.Skills
{
    /// <summary>
    /// Skill Class: Offering
    /// 
    /// Surrenders all of the player's target tokens
    /// except for the target one, healing them for
    /// one per two tokens surrendered
    /// </summary>
    class Offering
    {
        /// <summary>
        /// Performs the skill
        /// </summary>
        /// <returns>true if it was successfully cast</returns>
        public static Boolean Perform()
        {
            if (!CanPerform())
                return false;

            int count = 0;

            // Search through the board and surrender all of the
            // tokens owned by the player, healing them for every other
            // token.
            for (int y = 0; y < 10; ++y)
                for (int x = 0; x < 10; ++x)
                    if (Battle.Field[10 * y + x] == Battle.CurrentTurn && (x != Battle.X || y != Battle.Y))
                    {
                        Battle.Field[10 * y + x] = 3 - Battle.CurrentTurn;

                        count++;
                        if (count % Constants.IntValue("skillOfferingDamageDivisor") == 0)
                            Battle.Damage(Constants.IntValue("skillOfferingDamagePerToken"), true);
                    }

            // Consume the appropriate amount of ap
            Battle.ConsumeAp(Constants.IntValue("skillOfferingAPCost"));

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
            if (Battle.Field[10 * Battle.Y + Battle.X] != Battle.CurrentTurn || Battle.Ap < Constants.IntValue("skillOfferingAPCost"))
                return false;

            // There must be at least one surrendered token for this to work
            for (int y = 0; y < 10; ++y)
                for (int x = 0; x < 10; ++x)
                    if (Battle.Field[10 * y + x] == Battle.CurrentTurn && (x != Battle.X || y != Battle.Y))
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
            // Make sure using it doesn't set up an instant-loss
            int location = 10 * Battle.Y + Battle.X;
            if (location % 10 != 9 && location % 10 != 0)
            {
                if (location >= 11 && location < 89)
                    if (Battle.Field[location - 11] != 0 && Battle.Field[location + 11] == 0
                        || Battle.Field[location - 11] == 0 && Battle.Field[location + 11] != 0)
                        return -999;
                if (location >= 9 && location < 91)
                    if (Battle.Field[location - 9] != 0 && Battle.Field[location + 9] == 0
                        || Battle.Field[location - 9] == 0 && Battle.Field[location + 9] != 0)
                        return -999;
            }
            if ((location / 10) % 9 != 0 && location >= 10 && location < 90)
                if (Battle.Field[location - 10] == 0 && Battle.Field[location + 10] != 0
                    || Battle.Field[location - 10] != 0 && Battle.Field[location + 10] == 0)
                    return -999;
            if ((location % 10) % 9 != 0 && location >= 1 && location < 99)
                if (Battle.Field[location - 1] == 0 && Battle.Field[location + 1] != 0
                    || Battle.Field[location - 1] != 0 && Battle.Field[location + 1] == 0)
                    return -999;

            // If it doesn't, rate it normally
            int rating = 0;

            for (int y = 0; y < 10; ++y)
                for (int x = 0; x < 10; ++x)
                    if (Battle.Field[10 * y + x] == Battle.CurrentTurn && (x != Battle.X || y != Battle.Y))
                        rating -= Constants.IntValue("skillOfferingDamagePerToken");

            int bonus = 0;
            int i = Battle.X;
            int j = Battle.Y;
            if ((i == 0 || i == 9) && (j == 0 || j == 9))
                bonus = Constants.IntValue("battleRatingCorner");
            else if (i == 0 || i == 9 || j == 0 || j == 9)
                bonus = Constants.IntValue("battleRatingEdge");
            if (Math.Abs(i - j) == 8 || i + j == 1 || i + j == 17 || ((i == 1 || i == 8) && (j == 1 || j == 8)))
                bonus = Constants.IntValue("battleRatingNearCorner");

            return rating / Constants.IntValue("skillOfferingDamageDivisor") - 2 * Constants.IntValue("skillOfferingAPCost") + bonus;
        }
    }
}
