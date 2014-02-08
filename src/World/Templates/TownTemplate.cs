using System;

namespace MastermindRPG.World.Templates
{
    /// <summary>
    /// Template Type: Town
    /// 
    /// Generates a town with the provided building
    /// orientation.
    /// </summary>
    class TownTemplate : TemplateType
    {
        /// <summary>
        /// Creates the town
        /// </summary>
        /// <param name="buildings">the building orientation {top left, top center, top right, bottom left, bottom center, bottom right}</param>
        /// <returns>the town layout</returns>
        public new static char[,] Create(params int[] parameters)
        {
            // Set town to the default size
            length = 33;
            width = 13;

            Initialize();

            // Add the roads
            for (int x = 0; x < 33; ++x)
                for (int y = 5; y < 8; ++y)
                    tiles[x, y] = ';';
            for (int x = 10; x < 13; ++x)
                for (int y = 0; y < 13; ++y)
                {
                    tiles[x, y] = ';';
                    tiles[x + 10, y] = ';';
                }

            // Add the buildings
            for (int x = 0; x < 3; ++x)
                for (int y = 0; y < 2; ++y)
                {
                    int xPos = 10 * x + 4;
                    int yPos = 7 * y + 1;
                    int xChange = 4;
                    if (parameters[y * 3 + x] == 1)
                    {
                        xPos--;
                        xChange++;
                    }

                    // Add the side walls
                    tiles[xPos, yPos] = '╔';
                    tiles[xPos, yPos + 1] = '║';
                    tiles[xPos, yPos + 2] = '║';
                    tiles[xPos, yPos + 3] = '╚';
                    tiles[xPos + xChange, yPos] = '╗';
                    tiles[xPos + xChange, yPos + 1] = '║';
                    tiles[xPos + xChange, yPos + 2] = '║';
                    tiles[xPos + xChange, yPos + 3] = '╝';

                    //Add the top and bottom walls
                    for (int a = xPos + 1; a < xPos + xChange; ++a)
                        tiles[a, y * 3 + yPos] = '═';
                    xPos += xChange;
                    yPos = (1 - y) * 3 + yPos;
                    char[] lastWall;
                    int yChange;
                    if (y == 0)
                    {
                        lastWall = new char[] { '╚', '╗', '═', '╔', '╝' };
                        yChange = -1;
                    }
                    else
                    {
                        lastWall = new char[] { '╔', '╝', '═', '╚', '╗' };
                        yChange = 1;
                    }
                    tiles[xPos - 1, yPos] = lastWall[0];
                    tiles[xPos - 1, yPos + yChange] = lastWall[1];
                    tiles[xPos - 2, yPos + yChange] = lastWall[2];
                    tiles[xPos - 3, yPos + yChange] = lastWall[3];
                    tiles[xPos - 3, yPos] = lastWall[4];
                    if (parameters[y * 3 + x] == 1)
                        tiles[xPos - 4, yPos] = '═';

                    // Add the signs for the Inn and Shop
                    yPos = 2 * yChange + yPos;
                    if (parameters[y * 3 + x] == 1)
                    {
                        tiles[xPos - 4, yPos] = 'S';
                        tiles[xPos - 3, yPos] = 'h';
                        tiles[xPos - 2, yPos] = 'o';
                        tiles[xPos - 1, yPos] = 'p';
                    }
                    else if (parameters[y * 3 + x] == 2)
                    {
                        tiles[xPos - 3, yPos] = 'I';
                        tiles[xPos - 2, yPos] = 'n';
                        tiles[xPos - 1, yPos] = 'n';
                    }
                }

            return tiles;
        }
    }
}
