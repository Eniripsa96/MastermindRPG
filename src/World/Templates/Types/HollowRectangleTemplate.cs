using System;

namespace MastermindRPG.World.Templates.Types
{
    /// <summary>
    /// Template Type: Hollow Rectangle
    /// 
    /// Creates a hollow rectangle room layout
    /// </summary>
    class HollowRectangleTemplate : RectangleTemplate
    {
        /// <summary>
        /// Creates the hollow rectangle layout
        /// </summary>
        /// <param name="dim">dimensions of the template</param>
        /// <returns>the room layout</returns>
        public new static char[,]  Create(params int[] dim)
        {
            length = dim[0];
            width = dim[1];

            Initialize();

            // Draws the outside rectangle
            Draw(length, width);

            // Draws the inner rectangle
            Draw(length - 2, width - 2);

            return tiles;
        }
    }
}
