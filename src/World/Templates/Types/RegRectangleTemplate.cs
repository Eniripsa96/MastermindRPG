using System;

namespace MastermindRPG.World.Templates.Types
{
    /// <summary>
    /// Template Type: RegRectangle
    /// 
    /// Creates a rectangular room layout
    /// </summary>
    class RegRectangleTemplate : RectangleTemplate
    {
        /// <summary>
        /// Creates the room layout
        /// </summary>
        /// <param name="dim">dimensions of the template</param>
        /// <returns>the room layout</returns>
        public new static char[,] Create(params int[] dim)
        {
            length = dim[0];
            width = dim[1];

            Initialize();
            
            Draw(length, width);
            
            return tiles;
        }
    }
}
