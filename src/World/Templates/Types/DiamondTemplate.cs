using System;

namespace MastermindRPG.World.Templates.Types
{
    /// <summary>
    /// Template Type: Diamond
    /// 
    /// Creates a room layout in the shape of a diamond
    /// </summary>
    class DiamondTemplate : TemplateType
    {
        /// <summary>
        /// Creates a diamond room layout
        /// </summary>
        /// <param name="dim">dimensions of the template</param>
        /// <returns>the room layout</returns>
        public new static char[,]  Create(params int[] dim)
        {
            length = dim[0];
            width = dim[1];

            Initialize();

            int edgeClose;
            int lEdgeFar;
            int wEdgeFar;

            // Obtain the outside positions
            if (width >= length)
            {
                edgeClose = (length - 1) / 2;
                lEdgeFar = length - edgeClose;
                wEdgeFar = width - edgeClose;
            }
            else
            {
                edgeClose = (width - 1) / 2;
                lEdgeFar = length - edgeClose;
                wEdgeFar = width - edgeClose;
            }

            // Print the outside edges
            for (int y = edgeClose; y < wEdgeFar; ++y)
            {
                for (int x = 0; x < length; x += length - 1)
                {
                    tiles[x, y] = '║';
                }
            }
            for (int x = edgeClose; x < lEdgeFar; ++x)
            {
                for (int y = 0; y < width; y += width - 1)
                {
                    tiles[x, y] = '═';
                }
            }

            // print the outside corners
            tiles[0, edgeClose - 1] = '╔';
            tiles[0, wEdgeFar] = '╚';
            tiles[length - 1, edgeClose - 1] = '╗';
            tiles[length - 1, wEdgeFar] = '╝';

            // print the jagged edges
            for (int x = 1; x < edgeClose; ++x)
            {
                tiles[x, edgeClose - x] = '╝';
                tiles[x, edgeClose - 1 - x] = '╔';
                tiles[length - 1 - x, edgeClose - x] = '╚';
                tiles[length - 1 - x, edgeClose - 1 - x] = '╗';
                tiles[x, width - edgeClose + x] = '╚';
                tiles[x, width - edgeClose - 1 + x] = '╗';
                tiles[length - 1 - x, width - edgeClose + x] = '╝';
                tiles[length - 1 - x, width - edgeClose - 1 + x] = '╔';
            }

            return tiles;
        }
    }
}
