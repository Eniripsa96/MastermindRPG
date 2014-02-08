using System;

namespace MastermindRPG.World.Templates.Types
{
    /// <summary>
    /// Template Type: Other 1
    /// 
    /// Creates a room layout with the general shape:
    ///  ___
    /// | | |
    /// __|__
    /// </summary>
    class Other1Template : TemplateType
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

            // Add the corners
            tiles[0, 0] = '╔';
            tiles[length - 1, 0] = '╗';
            tiles[0, width - 1] = '╚';
            tiles[length - 1, width - 1] = '╝';

            // Add the top and bottom edges
            for (int x = 1; x < length - 1; ++x)
            {
                tiles[x, 0] = '═';
                tiles[x, width - 1] = '═';
            }

            // Add the inside edges
            int leftSide = (length - 3) / 2;
            int rightSide = length / 2 + 1;
            tiles[leftSide, 2] = '╗';
            tiles[rightSide, 2] = '╔';
            tiles[leftSide, width - 3] = '╝';
            tiles[rightSide, width - 3] = '╚';
            for (int y = 3; y < width - 3; ++y)
            {
                tiles[leftSide, y] = '║';
                tiles[rightSide, y] = '║';
            }

            // Add the left and right edges
            for (int y = 1; y < width / 2 + 1; ++y)
            {
                tiles[0, y] = '║';
                tiles[length - 1, y] = '║';
                if (y > 2)
                {
                    tiles[2, y] = '║';
                    tiles[length - 3, y] = '║';
                }
            }

            // Cap the left and right sides
            tiles[0, width / 2 + 1] = '╚';
            tiles[1, width / 2 + 1] = '═';
            tiles[2, width / 2 + 1] = '╝';
            tiles[length - 1, width / 2 + 1] = '╝';
            tiles[length - 2, width / 2 + 1] = '═';
            tiles[length - 3, width / 2 + 1] = '╚';

            // Finish the top
            tiles[2, 2] = '╔';
            tiles[length - 3, 2] = '╗';
            for (int x = 3; x < leftSide; ++x)
            {
                tiles[x, 2] = '═';
                tiles[x + rightSide - 2, 2] = '═';
            }

            // Finish the bottom corners
            tiles[0, width - 2] = '║';
            tiles[length - 1, width - 2] = '║';
            tiles[0, width - 3] = '╔';
            tiles[length - 1, width - 3] = '╗';

            // Finish the bottom edges
            for (int x = 1; x < leftSide; ++x)
            {
                tiles[x, width - 3] = '═';
                tiles[x + rightSide, width - 3] = '═';
            }

            // Fix the merged paths for small rooms
            if (width <= 8)
            {
                tiles[0, (width + 2) / 2] = '╠';
                tiles[length - 1, (width + 2) / 2] = '╣';
                tiles[2, (width + 2) / 2] = '╩';
                tiles[length - 3, (width + 2) / 2] = '╩';
            }

            // Reflect it if indicated
            if (dim[2] == 1)
            {
                char[,] reflection = new char[length, width];
                for (int x = 0; x < length; ++x)
                {
                    for (int y = 0; y < width; ++y)
                    {
                        int Y = width - y - 1;
                        if (tiles[x, y] == '╗')
                            reflection[x, Y] = '╝';
                        else if (tiles[x, y] == '╝')
                            reflection[x, Y] = '╗';
                        else if (tiles[x, y] == '╔')
                            reflection[x, Y] = '╚';
                        else if (tiles[x, y] == '╚')
                            reflection[x, Y] = '╔';
                        else if (tiles[x, y] == '╩')
                            reflection[x, Y] = '╦';
                        else
                            reflection[x, Y] = tiles[x, y];
                    }
                }
                for (int x = 0; x < length; ++x)
                    for (int y = 0; y < width; ++y)
                        tiles[x, y] = reflection[x, y];
            }

            return tiles;
        }
    }
}
