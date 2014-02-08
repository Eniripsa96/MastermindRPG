using System;

namespace MastermindRPG.World
{
    /// <summary>
    /// Base class for maps
    /// 
    /// Derived Classes:
    ///     Area
    ///     Town
    /// </summary>
    interface Map
    {
        /// <summary>
        /// Returns the ID of the map
        /// </summary>
        int Id
        {
            get;
        }

        /// <summary>
        /// Create the map using a seed
        /// </summary>
        void Generate();

        /// <summary>
        /// Change between rooms in the map
        /// </summary>
        /// <param name="direction">the direction moving towards</param>
        void ChangeRoom(int[] direction);

        /// <summary>
        /// Display the map on the console window
        /// </summary>
        void Draw();

        /// <summary>
        /// Perform checks between moves
        /// 
        /// (Such as enemy encounters or entering buildings)
        /// </summary>
        void MoveAction();

        /// <summary>
        /// Refres the room display
        /// </summary>
        void Refresh();

        void Tick();

        /// <summary>
        /// Checks for walkable tiles
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        /// <returns>true if it can be walked on</returns>
        Boolean? WalkableTile(int x, int y);
    }
}
