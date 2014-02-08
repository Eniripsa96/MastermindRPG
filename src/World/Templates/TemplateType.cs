using System;

namespace MastermindRPG.World.Templates
{
    /// <summary>
    /// Template base class
    /// 
    /// Contains the data and initializer for 
    /// the different types of templates
    /// 
    /// Derived Classes:
    ///     CrossTemplate
    ///         RegCrossTemplate
    ///         RoundCrossTemplate
    ///     Diamond Template
    ///     RectangleTemplate
    ///         HollowRectangleTemplate
    ///         RegRectangleTemplate
    ///     Other1Template
    ///     Other2Template
    ///     TownTemplate
    /// </summary>
    class TemplateType
    {
        /// <summary>
        /// Room horizontal size
        /// </summary>
        protected static int length;

        /// <summary>
        /// Room vertical size
        /// </summary>
        protected static int width;

        /// <summary>
        /// Room layout
        /// </summary>
        protected static char[,] tiles;

        /// <summary>
        /// Initializes the room's character grid
        /// </summary>
        protected static void Initialize()
        {
            // Creates the grid
            tiles = new char[length, width];

            // Fill it with space (so it isn't null)
            for (int x = 0; x < length; ++x)
            {
                for (int y = 0; y < width; ++y)
                {
                    tiles[x, y] = ' ';
                }
            }
        }

        /// <summary>
        /// Creates the template with the given parameters
        /// </summary>
        /// <param name="parameters">{width, length, reflection}</param>
        /// <returns>the resulting map</returns>
        public static char[,] Create(params int[] dim) { return null; }
    }
}
