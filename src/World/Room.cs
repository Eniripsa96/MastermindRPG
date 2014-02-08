using System;

namespace MastermindRPG.World
{
    /// <summary>
    /// Contains the information for a room
    /// </summary>
    class Room
    {
        /// <summary>
        /// The horizontal size
        /// </summary>
        private int length;

        /// <summary>
        /// The vertical size
        /// </summary>
        private int width;

        /// <summary>
        /// The tilemap for the room
        /// </summary>
        private char[,] tiles;

        /// <summary>
        /// Returns the horizontal size of the room
        /// </summary>
        public int HorizontalSize
        {
            get { return length; }
        }

        /// <summary>
        /// Returns the vertical size of the room
        /// </summary>
        public int VerticalSize
        {
            get { return width; }
        }

        /// <summary>
        /// Returns the tile as the designated location
        /// </summary>
        /// <param name="x">Horizontal Coordinate</param>
        /// <param name="y">Vertical Coordinate</param>
        /// <returns></returns>
        public char this[int x, int y]
        {
            get
            {
                 return tiles[x, y];
            }
        }

        /// <summary>
        /// Constructs a room object
        /// </summary>
        /// <param name="length">The horizontal size</param>
        /// <param name="width">The vertical size</param>
        /// <param name="tiles">The tilemap</param>
        public Room(int length, int width, char[,] tiles)
        {
            this.length = length;
            this.width = width;
            this.tiles = tiles;
        }
    }
}
