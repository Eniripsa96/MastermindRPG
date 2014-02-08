using System;
using System.Reflection;

using MastermindRPG;
using MastermindRPG.Data.Structures.List;

namespace MastermindRPG.AI.Othello
{
    /// <summary>
    /// Contains the scripts for finding
    /// the AI's "best" moves
    /// </summary>
    class AiScripts
    {
        # region constants

        /// <summary>
        /// Changes in horiontal and vertical positions for each direction 
        /// { up, up and right, right, down and right, down, down and left, left, up and left }
        /// </summary>
        private static readonly int[] xChanges = { 0, 1, 1, 1, 0, -1, -1, -1 };
        private static readonly int[] yChanges = { -1, -1, 0, 1, 1, 1, 0, -1 }; 

        /// <summary>
        /// Names of the actions (to check)
        /// </summary>
        private static readonly string[] actionNames = { "PlaceToken", "Betray", "Sacrifice", "DoubleStrike", "Negotiate", "Heal", "Rewind", "Void", "Offering", "Dominate" };

        /// <summary>
        /// Skills reflection path
        /// </summary>
        private static readonly string actionPrefix = "MastermindRPG.Creatures.Skills.";

        # endregion constants

        /// <summary>
        /// The computer's playing algorithm
        /// Determines where to play for the AI
        /// Possibly can be expanded upon to make a better AI 
        ///     (prioritizing corners or further considerations)
        /// 
        /// Current Process:
        ///  - Finds all possible moves
        ///  - Give each move a rating depending on what skill it is
        ///  - Counts how many tokens that can be lost next round for each move
        ///  - Weighs the options using (rating - loss), higher being better
        ///  - Keeps a list of the best option(s)
        ///  - Picks a move from the list of best options and uses it
        /// </summary>
        /// <param name="filter">The list of accepted actions</param>
        /// <returns>the location of the "best" move</returns>
        public static Move Run(params int[] filter)
        {
            // Records the best option when measured by ( captured pieces - vulnerable pieces )
            int best = 0;

            // Saves the board to refresh after checking
            int[] boardBackup = new int[100];
            Battle.Field.Board.CopyTo(boardBackup, 0);

            Boolean couldMove = false;

            // Finds where the computer can move
            SimpleList<Move> moves = FindPossibleMoves(2);

            // Records the locations of the best choices
            SimpleList<Move> bestMoves = new SimpleList<Move>();

            int hp = Battle.Player.Health;
            int hp2 = Battle.Foe.Health;
            Battle.ApEnabled(false);

            // Weigh each move according to the ( captured pieces - vulnerable pieces ) idea
            foreach (Move a in moves)
            {
                if (a.Action == 0)
                    couldMove = true;

                // Apply the filter
                if (filter.Length != 0)
                {
                    Boolean good = false;
                    foreach (int i in filter)
                        if (a.Action == i)
                            good = true;
                    if (!good)
                        continue;
                }

                Battle.X = a.Location % 10;
                Battle.Y = a.Location / 10;

                // Get the rating of the skill
                Type type = Type.GetType(actionPrefix + actionNames[a.Action]);
                int c = (int)type.InvokeMember("Rate", BindingFlags.InvokeMethod, null, null, null);

                // Get how many tokens can be lost to the human
                type.InvokeMember("Perform", BindingFlags.InvokeMethod, null, null, null);
                int l = FindPotentialLoss();
                Battle.ExtraTurns = 0;

                // Keep moves within 2 of the "best" and clear all old moves if the new move is much better
                if (c - l > best + 2 || bestMoves.Size == 0)
                {
                    best = c - l;
                    bestMoves.Clear();
                    bestMoves.Add(a);
                }
                else if (c - 1 >= best - 2)
                    bestMoves.Add(a);

                // Refresh the board after checking the spot
                boardBackup.CopyTo(Battle.Field.Board, 0);
            }

            // Restore the hps back to what it was before the tests
            Battle.Player.Damage(Battle.Player.Health - hp);
            Battle.Foe.Damage(Battle.Foe.Health - hp2);
            Battle.ApEnabled(true);

            // If the computer found a best option, return it
            if (bestMoves.Size > 0 && couldMove)
            {
                Move bestMove = bestMoves[new Random().Next(0, bestMoves.Size)];
                return bestMove;
            }
            return null;
        }

        /// <summary>
        /// Lists all the possible moves a player can make
        /// </summary>
        /// <returns>The list of move locations</returns>
        public static SimpleList<Move> FindPossibleMoves(int player, params int[] filter)
        {
            int tempX = Battle.X;
            int tempY = Battle.Y;

            int limit = 1;
            if (player == 2)
                limit = Battle.Foe.SkillLimit;
            if (filter.Length > 0)
                limit = filter[0];

            SimpleList<Move> list = new SimpleList<Move>();
            for (int a = 0; a < 100; ++a)
            {
                Battle.X = a % 10;
                Battle.Y = a / 10;

                for (int x = 0; x < limit; ++x)
                {
                    Type type = Type.GetType(actionPrefix + actionNames[x]);
                    if ((Boolean)type.InvokeMember("CanPerform", BindingFlags.InvokeMethod, null, null, null))
                        list.Add(new Move(a, x));
                }
            }

            Battle.X = tempX;
            Battle.Y = tempY;

            return list;
        }

        /// <summary>
        /// Finds all captured tokens if a token is placed at the given spot
        /// </summary>
        /// <param name="x">The horizontal coordinate</param>
        /// <param name="y">The vertical coordinate</param>
        /// <returns>the list of captured token locations</returns>
        private static SimpleList<int> CaptureList(int x, int y, int player)
        {
            SimpleList<int> spots = new SimpleList<int>();

            // Checks in all 8 directions, starting with North
            for (int d = 0; d < 8; ++d)
            {
                int searchX = x + xChanges[d];
                int searchY = y + yChanges[d];

                // Gets the list of possible moves
                SimpleList<int> possibleSpots = new SimpleList<int>();

                // Allow it to search only within the grid's range
                while (searchX >= 0 && searchX < 10 && searchY >= 0 && searchY < 10)
                {
                    int spot = Battle.Field.Board[10 * searchY + searchX];

                    // If it's an enemy piece, remember the location
                    if (spot == 3 - player)
                        possibleSpots.Add(10 * searchY + searchX);

                    // Stop when it's an owned piece
                    else if (possibleSpots.Size > 0 && spot == player)
                        break;

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
        /// Finds how many tokens the computer can lose to the player in one turn
        /// </summary>
        /// <returns>The maximum loss that can occurr</returns>
        private static int FindPotentialLoss()
        {
            int maxLoss = -1;
            Boolean first = true;
            SimpleList<Move> moves = FindPossibleMoves(1);

            // for each available move, count how many tokens the computer would lose when the player made that move
            // keeps only the highest number of captures
            foreach (Move i in moves)
            {
                if (i.Action != 0)
                    continue;

                int captures = CaptureList(i.Location % 10, i.Location / 10, 1).Size;
                if (captures > maxLoss || first)
                {
                    first = false;
                    maxLoss = captures;
                }
            }
            return maxLoss;
        }
    }
}
