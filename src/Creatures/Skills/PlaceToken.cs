using System;

using MastermindRPG.Data;
using MastermindRPG.Data.Structures.List;
using MastermindRPG.World;

namespace MastermindRPG.Creatures.Skills
{
    /// <summary>
    /// Skill Class: Place Token
    /// 
    /// Places a token at the location
    /// pointed to by the Battle class
    /// and captures all appropriate
    /// tokens.
    /// </summary>
    class PlaceToken
    {
        /// <summary>
        /// Directions for checking captures 
        /// { up, up and right, right, down and right, down, down and left, left, up and left }
        /// </summary>
        private static readonly int[] xChanges = { 0, 1, 1, 1, 0, -1, -1, -1 };
        private static readonly int[] yChanges = { -1, -1, 0, 1, 1, 1, 0, -1 }; 

        /// <summary>
        /// Finds the list of locations that will be captured if a token was placed at the current location by the given player
        /// </summary>
        /// <param name="player">The player placing the token</param>
        /// <returns>The list of captured token locations</returns>
        private static SimpleList<int> FindCapturedTokens()
        {
            int player = Battle.CurrentTurn;
            SimpleList<int> spots = new SimpleList<int>();

            // Checks in all 8 directions, starting with North
            for (int d = 0; d < 8; ++d)
            {
                int searchX = Battle.X + xChanges[d];
                int searchY = Battle.Y + yChanges[d];

                // Gets the list of possible moves
                SimpleList<int> possibleSpots = new SimpleList<int>();

                // Allow it to search only within the grid's range
                while (searchX >= 0 && searchX < 10 && searchY >= 0 && searchY < 10)
                {
                    int spot = Battle.Field[10 * searchY + searchX];

                    // If it's an enemy piece, remember the location
                    if (spot == 3 - player)
                        possibleSpots.Add(10 * searchY + searchX);

                    // Stop when it's an owned piece
                    else if (possibleSpots.Size > 0 && spot == player)
                    {
                        break;
                    }

                    // Stop and clear the list if it's an empty piece
                    else
                    {
                        possibleSpots.Clear();
                        break;
                    }
                    searchX += xChanges[d];
                    searchY += yChanges[d];
                }

                // If the loop ended because of going out of range, clear the list
                if (searchX < 0 || searchX > 9 || searchY < 0 || searchY > 9)
                    possibleSpots.Clear();

                // Add all the valid spots found that weren't cleared
                foreach (int i in possibleSpots)
                    spots.Add(i);
            }
            return spots;
        }

        /// <summary>
        /// Performs the skill
        /// </summary>
        /// <returns>true if it was successfully cast</returns>
        public static Boolean Perform ()
        {
            if (!CanPerform())
                return false;

            // Places the token and captures all appropriate tokens
            foreach (int i in FindCapturedTokens())
            {
                Battle.Field[i] = Battle.CurrentTurn;
                Battle.Damage(Constants.IntValue("placeTokenDamagePerToken"), false);
            }
            Battle.Field[10 * Battle.Y + Battle.X] = Battle.CurrentTurn;

            return true;
        }

        /// <summary>
        /// Checks if the move can be performed
        /// </summary>
        /// <returns>can be performed</returns>
        public static Boolean CanPerform()
        {
            // Cannot cast the skill if the location is owned
            if (Battle.Field[10 * Battle.Y + Battle.X] != 0)
                return false;

            // Cannot cast the skill if there are no captured tokens resulting from the placement
            if (FindCapturedTokens().Size == 0)
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

            return FindCapturedTokens().Size * Constants.IntValue("placeTokenDamagePerToken") + bonus;
        }
    }
}
