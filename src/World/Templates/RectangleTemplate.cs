using System;

namespace MastermindRPG.World.Templates
{
    /// <summary>
    /// Template Type: Rectangle
    /// 
    /// Creates a rectangle with the given dimensions
    /// 
    /// Contains the shared drawing portions between
    /// RegRectangle and HollowRectangle
    /// </summary>
    class RectangleTemplate : TemplateType
    {
        protected static void Draw(int l, int w)
        {
            int offset = length - l;
            // Set the corners
            tiles[offset, offset] = '╔';
            tiles[l - 1, offset] = '╗';
            tiles[offset, w - 1] = '╚';
            tiles[l - 1, w - 1] = '╝';
             
            // Fill in the edges
            int max = l;
            if (w > l)
                max = w;
            for (int y = 0; y < 2; ++y)
            {
                if (y == 1)
                    offset = 0;
                for (int x = 1 + length - l ; x < max; ++x)
                {
                    if (x < l - 1) tiles[x, y * (w - 1) + offset] = '═';
                    if (x < w - 1) tiles[y * (l - 1) + offset, x] = '║';
                }
            }
        }
    }
}
