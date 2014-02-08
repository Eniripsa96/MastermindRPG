using System;

namespace MastermindRPG.World.Templates.Types
{
    /// <summary>
    /// Template Type: RoundCross
    /// 
    /// Creates a room template with a rounded cross shape
    /// (Plus sign with a buldged middle)
    /// </summary>
    class RoundCrossTemplate : CrossTemplate
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
            for (int x = 1; x < otherEdgeLeft - 2; ++x)
            {
                tiles[x, sideEdgeTop - 1] = '═';
                tiles[x, sideEdgeBottom] = '═';
                tiles[length - 1 - x, sideEdgeTop - 1] = '═';
                tiles[length - 1 - x, sideEdgeBottom] = '═';
            }
            for (int y = 1; y < sideEdgeTop - 2; ++y)
            {
                tiles[otherEdgeLeft - 1, y] = '║';
                tiles[otherEdgeRight, y] = '║';
                tiles[otherEdgeLeft - 1, width - 1 - y] = '║';
                tiles[otherEdgeRight, width - 1 - y] = '║';
            }

            // print the inner corners
            tiles[otherEdgeLeft - 2, sideEdgeTop - 1] = '╝';
            tiles[otherEdgeLeft - 1, sideEdgeTop - 2] = '╝';

            tiles[otherEdgeLeft - 2, sideEdgeBottom] = '╗';
            tiles[otherEdgeLeft - 1, sideEdgeBottom + 1] = '╗';

            tiles[otherEdgeRight + 1, sideEdgeTop - 1] = '╚';
            tiles[otherEdgeRight, sideEdgeTop - 2] = '╚';

            tiles[otherEdgeRight + 1, sideEdgeBottom] = '╔';
            tiles[otherEdgeRight, sideEdgeBottom + 1] = '╔';

            tiles[otherEdgeLeft - 2, sideEdgeTop - 2] = '╔';
            tiles[otherEdgeLeft - 2, sideEdgeBottom + 1] = '╚';
            tiles[otherEdgeRight + 1, sideEdgeTop - 2] = '╗';
            tiles[otherEdgeRight + 1, sideEdgeBottom + 1] = '╝';

            return tiles;
        }
    }
}
