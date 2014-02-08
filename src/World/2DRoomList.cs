using System;

using MastermindRPG.Data.Structures.List;

namespace MastermindRPG.World
{
    /// <summary>
    /// 2-Dimensional list for the map
    /// </summary>
    class _2DRoomList : _2DRoomList<Room>
    {
        /// <summary>
        /// Access the token with the given coordinates
        /// </summary>
        /// <param name="x">map horizontal coordinate</param>
        /// <param name="y">map vertical coordinate</param>
        /// <param name="ii">tile horizontal coordinate</param>
        /// <param name="jj">tile vertical coordinate</param>
        /// <returns>tile token</returns>
        public char this[int x, int y, int ii, int jj]
        {
            get
            {
                // Move between rooms if the tile coordinates are out of bounds
                // Return an empty character if the room coordinates go out of range
                while (ii < 0)
                {
                    x--;
                    if (x < 0)
                        return ' ';
                    ii += this[x, y].HorizontalSize;
                }
                while (ii >= this[x, y].HorizontalSize)
                {
                    ii -= this[x, y].HorizontalSize;
                    x++;
                    if (x == length)
                        return ' ';
                }
                while (jj < 0)
                {
                    y--;
                    if (y < 0)
                        return ' ';
                    jj += this[x, y].VerticalSize;
                }
                while (jj >= this[x, y].VerticalSize)
                {
                    jj -= this[x, y].VerticalSize;
                    y++;
                    if (y == width)
                        return ' ';
                }

                // If the room coordinates were out of bounds and not caught, return the empty character
                if (x < 0 || x >= length)
                    return ' ';
                if (y < 0 || y >= width)
                    return ' ';

                // Return the tile
                return this[x, y][ii, jj];
            }
        }
    }
}
