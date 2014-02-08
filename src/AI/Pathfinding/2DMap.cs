using System;

namespace MastermindRPG.AI.Pathfinding
{
    /// <summary>
    /// Interface for maps compatible with the pathfinder
    /// </summary>
    public interface _2DMap
    {
        /// <summary>
        /// Returns the horizontal size of the map
        /// </summary>
        int HorizontalSize { get; }

        /// <summary>
        /// Returns the vertical size of the map
        /// </summary>
        int VerticalSize { get; }

        /// <summary>
        /// Returns if the given tile is walkable
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        /// <returns>true if it is walkable, false otherwise</returns>
        Boolean IsWalkable(int x, int y);
    }
}