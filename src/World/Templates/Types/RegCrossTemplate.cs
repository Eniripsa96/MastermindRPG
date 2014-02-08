using System;

namespace MastermindRPG.World.Templates.Types
{
    /// <summary>
    /// Template Type: RegCross
    /// 
    /// Creates a regular cross shaped room
    /// </summary>
    class RegCrossTemplate : CrossTemplate
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

            DrawEdges();

            int sideEdgeTop = (width - 1) / 2;
            int sideEdgeBottom = width / 2 + 1;
            int otherEdgeLeft = (length - 1) / 2;
            int otherEdgeRight = length / 2 + 1;

            // Print the inner edges
            for (int x = 1; x < otherEdgeLeft - 1; ++x)
            {
                tiles[x, sideEdgeTop - 1] = '═';
                tiles[x, sideEdgeBottom] = '═';
                tiles[length - 1 - x, sideEdgeTop - 1] = '═';
                tiles[length - 1 - x, sideEdgeBottom] = '═';
            }
            for (int y = 1; y < sideEdgeTop - 1; ++y)
            {
                tiles[otherEdgeLeft - 1, y] = '║';
                tiles[otherEdgeRight, y] = '║';
                tiles[otherEdgeLeft - 1, width - 1 - y] = '║';
                tiles[otherEdgeRight, width - 1 - y] = '║';
            }

            // Print the inner corners
            tiles[otherEdgeLeft - 1, sideEdgeTop - 1] = '╝';
            tiles[otherEdgeLeft - 1, sideEdgeBottom] = '╗';
            tiles[otherEdgeRight, sideEdgeTop - 1] = '╚';
            tiles[otherEdgeRight, sideEdgeBottom] = '╔';

            return tiles;
        }
    }
}
