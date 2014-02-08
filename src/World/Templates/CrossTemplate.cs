using System;

namespace MastermindRPG.World.Templates
{
    /// <summary>
    /// Template Type: Cross
    /// 
    /// Creates a room with a cross shape (part of it)
    /// 
    /// Contains the shared drawing portions 
    /// between normal crosses and round crosses
    /// </summary>
    class CrossTemplate : TemplateType
    {
        /// <summary>
        /// Draws the outside edges of the cross shapes
        /// </summary>
        protected static void DrawEdges()
        {
            // Locations for the ends
            int sideEdgeTop = (width - 1) / 2;
            int sideEdgeBottom = width / 2 + 1;
            int otherEdgeLeft = (length - 1) / 2;
            int otherEdgeRight = length / 2 + 1;

            // Print the outside edges
            for (int y = sideEdgeTop; y < sideEdgeBottom; ++y)
            {
                for (int x = 0; x < length; x += length - 1)
                {
                    tiles[x, y] = '║';
                }
            }
            for (int x = otherEdgeLeft; x < otherEdgeRight; ++x)
            {
                for (int y = 0; y < width; y += width - 1)
                {
                    tiles[x, y] = '═';
                }
            }

            // print the outside corners
            tiles[0, sideEdgeTop - 1] = '╔';
            tiles[0, sideEdgeBottom] = '╚';
            tiles[length - 1, sideEdgeTop - 1] = '╗';
            tiles[length - 1, sideEdgeBottom] = '╝';
            tiles[otherEdgeLeft - 1, 0] = '╔';
            tiles[otherEdgeRight, 0] = '╗';
            tiles[otherEdgeLeft - 1, width - 1] = '╚';
            tiles[otherEdgeRight, width - 1] = '╝';
        }
    }
}
