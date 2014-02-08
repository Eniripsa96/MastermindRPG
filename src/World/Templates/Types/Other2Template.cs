using System;

namespace MastermindRPG.World.Templates.Types
{
    /// <summary>
    /// Template Type: Other2
    /// 
    /// Creates a room layout with the general shape:
    ///    _
    /// |_| |_|
    /// | |_| |
    /// </summary>
    class Other2Template : TemplateType
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

            // Add the side paths
            tiles[0, 0] = '╔';
            tiles[length - 1, 0] = '╗';
            tiles[0, width - 1] = '╚';
            tiles[length - 1, width - 1] = '╝';
            tiles[1, 0] = '═';
            tiles[1, width - 1] = '═';
            tiles[length - 2, 0] = '═';
            tiles[length - 2, width - 1] = '═';
            tiles[2, 0] = '╗';
            tiles[2, width - 1] = '╝';
            tiles[length - 3, 0] = '╔';
            tiles[length - 3, width - 1] = '╚';
            for (int y = 1; y < width - 1; ++y)
            {
                if (y < width / 2 - 1 || y > width / 2 + 1)
                {
                    tiles[2, y] = '║';
                    tiles[length - 3, y] = '║';
                }
                tiles[0, y] = '║';
                tiles[length - 1, y] = '║';
            }

            // Add the connecting paths
            tiles[2, width / 2 - 1] = '╚';
            tiles[2, width / 2 + 1] = '╔';
            tiles[length - 3, width / 2 - 1] = '╝';
            tiles[length - 3, width / 2 + 1] = '╗';
            for (int x = 3; x < length / 2 - 2; ++x)
            {
                tiles[x, width / 2 - 1] = '═';
                tiles[x, width / 2 + 1] = '═';
                tiles[length - x - 1, width / 2 - 1] = '═';
                tiles[length - x - 1, width / 2 + 1] = '═';
            }

            // Make the central section
            for (int y = 2; y < width - 2; ++y)
                tiles[length / 2, y] = '║';
            tiles[length / 2 - 2, width / 2 - 1] = '╝';
            tiles[length / 2 - 2, width / 2 + 1] = '╗';
            tiles[length / 2 + 2, width / 2 - 1] = '╚';
            tiles[length / 2 + 2, width / 2 + 1] = '╔';
            tiles[length / 2 - 2, 0] = '╔';
            tiles[length / 2 - 2, width - 1] = '╚';
            tiles[length / 2 + 2, 0] = '╗';
            tiles[length / 2 + 2, width - 1] = '╝';
            for (int y = 1; y < width - 1; ++y)
            {
                if (y >= width / 2 - 1 && y <= width / 2 + 1)
                    continue;
                tiles[length / 2 - 2, y] = '║';
                tiles[length / 2 + 2, y] = '║';
            }
            tiles[length / 2 - 1, 0] = '═';
            tiles[length / 2, 0] = '═';
            tiles[length / 2 + 1, 0] = '═';
            tiles[length / 2 - 1, width - 1] = '═';
            tiles[length / 2, width - 1] = '═';
            tiles[length / 2 + 1, width - 1] = '═';

            return tiles;
        }
    }
}
