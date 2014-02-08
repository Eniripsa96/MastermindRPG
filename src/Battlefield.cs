using System;

using MastermindRPG.Data;
using MastermindRPG.GUI;

namespace MastermindRPG
{
    /// <summary>
    /// Contains the board data for
    /// battles
    /// </summary>
    partial class Battlefield
    {
        /// <summary>
        /// Tokens for each player
        /// </summary>
        private readonly char[] tokens = { Constants.CharValue("battleEmptyToken"), Constants.CharValue("battlePlayerToken"), Constants.CharValue("battleEnemyToken") };

        /// <summary>
        /// Colors for each player
        /// </summary>
        private readonly ConsoleColor[] colors = { ConsoleColor.White, Constants.ColorValue("playerColor"), Constants.ColorValue("enemyColor") };
        
        /// <summary>
        /// The list of tokens in play by position
        /// </summary>
        private int[] board;

        /// <summary>
        /// Returns the token at the given location
        /// </summary>
        /// <param name="x">The board location (x % 10 = horizontal coordinate, x / 10 = vertical coordinate)</param>
        /// <returns></returns>
        public int this[int x]
        {
            get { return board[x]; }
            set { board[x] = value; }
        }

        /// <summary>
        /// Property for the board
        /// </summary>
        public int[] Board
        {
            get { return board; }
            set { board = value; }
        }

        /// <summary>
        /// Battlefield constructor
        /// </summary>
        public Battlefield()
        {
            board = new int[100];
            Reset();
        }

        /// <summary>
        /// Draws the tokens on the board
        /// </summary>
        public void Draw()
        {
            for (int x = 0; x < 10; ++x)
                for (int y = 0; y < 10; ++y)
                {
                    int player = board[10 * y + x];
                    ConsoleTools.Draw(x * 4 + 14, y * 2 + 4, tokens[player], colors[player]);
                }
        }

        /// <summary>
        /// Resets the board to default configuration
        /// </summary>
        public void Reset()
        {
            for (int x = 0; x < 100; ++x)
                board[x] = 0;
            board[44] = 1;
            board[45] = 2;
            board[54] = 2;
            board[55] = 1;
        }

        /// <summary>
        /// Counts how many tokens are owned by the given player
        /// </summary>
        /// <param name="player">the player to count for</param>
        /// <returns>the number of tokens</returns>
        public int CountTokens(int player)
        {
            int count = 0;
            foreach (int token in board)
                if (token == player)
                    ++count;
            return count;
        }

        /// <summary>
        /// Returns the color associated with the given tile
        /// </summary>
        /// <param name="x">tile id</param>
        /// <returns>Color</returns>
        public ConsoleColor GetColor(int x)
        {
            return colors[board[x]];
        }
    }
}
