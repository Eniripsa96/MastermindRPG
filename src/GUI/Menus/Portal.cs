using System;

using MastermindRPG.Data;
using MastermindRPG.GUI;
using MastermindRPG.Threads;

namespace MastermindRPG
{
    /// <summary>
    /// Handles the interaction with the world portal
    /// </summary>
    class Portal
    {
        /// <summary>
        /// The world limit
        /// </summary>
        private static readonly int worldLimit = Constants.IntValue("worldLimit");

        /// <summary>
        /// The selected world
        /// </summary>
        private static int choice;

        /// <summary>
        /// The digit being modified (0 = ones digit, 1 = tens digit)
        /// </summary>
        private static int digit;

        /// <summary>
        /// Opens the portal
        /// </summary>
        /// <param name="progress">current progress through the game</param>
        /// <returns>the selected world (0 if escape was pressed)</returns>
        public static int Open(int progress)
        {
            choice = 1;
            ConsoleKey key;

            // Draws the portal
            ConsoleTools.DrawDesign(ConsoleTools.portal);
            Refresh();

            do
            {
                System.Threading.Thread.Sleep(50);
                key = KeyInput.Key;

                if (key == ConsoleKey.UpArrow)
                    choice += (int)Math.Pow(10, digit);
                else if (key == ConsoleKey.DownArrow)
                    choice -= (int)Math.Pow(10, digit);
                else if (key == ConsoleKey.LeftArrow)
                    digit = 1;
                else if (key == ConsoleKey.RightArrow)
                    digit = 0;
                else if (key == ConsoleKey.Spacebar)
                    return choice;
                else
                    continue;

                // Limit the area ID to available worlds and to the world limit
                 if (choice > progress + 1)
                    choice = progress + 1;
                if (choice < 1)
                    choice = 1;
                if (choice > worldLimit)
                    choice = worldLimit;

                Refresh();
            }
            while (key != ConsoleKey.Escape);

            return 0;
        }

        # region numbers

        /// <summary>
        /// Layout for numbers displayed in the world portal
        /// </summary>
        private static readonly String[,] numberTemplates =
        {
            { Constants.StringValue("n0Line1"), Constants.StringValue("n0Line2"), Constants.StringValue("n0Line3"), Constants.StringValue("n0Line4"), Constants.StringValue("n0Line5") },
            { Constants.StringValue("n1Line1"), Constants.StringValue("n1Line2"), Constants.StringValue("n1Line3"), Constants.StringValue("n1Line4"), Constants.StringValue("n1Line5") },
            { Constants.StringValue("n2Line1"), Constants.StringValue("n2Line2"), Constants.StringValue("n2Line3"), Constants.StringValue("n2Line4"), Constants.StringValue("n2Line5") },
            { Constants.StringValue("n3Line1"), Constants.StringValue("n3Line2"), Constants.StringValue("n3Line3"), Constants.StringValue("n3Line4"), Constants.StringValue("n3Line5") },
            { Constants.StringValue("n4Line1"), Constants.StringValue("n4Line2"), Constants.StringValue("n4Line3"), Constants.StringValue("n4Line4"), Constants.StringValue("n4Line5") },
            { Constants.StringValue("n5Line1"), Constants.StringValue("n5Line2"), Constants.StringValue("n5Line3"), Constants.StringValue("n5Line4"), Constants.StringValue("n5Line5") },
            { Constants.StringValue("n6Line1"), Constants.StringValue("n6Line2"), Constants.StringValue("n6Line3"), Constants.StringValue("n6Line4"), Constants.StringValue("n6Line5") },
            { Constants.StringValue("n7Line1"), Constants.StringValue("n7Line2"), Constants.StringValue("n7Line3"), Constants.StringValue("n7Line4"), Constants.StringValue("n7Line5") },
            { Constants.StringValue("n8Line1"), Constants.StringValue("n8Line2"), Constants.StringValue("n8Line3"), Constants.StringValue("n8Line4"), Constants.StringValue("n8Line5") },
            { Constants.StringValue("n9Line1"), Constants.StringValue("n9Line2"), Constants.StringValue("n9Line3"), Constants.StringValue("n9Line4"), Constants.StringValue("n9Line5") },
        };

        # endregion numbers

        /// <summary>
        /// Refreshes the portal display
        /// </summary>
        private static void Refresh()
        {
            // Draw the numbers
            for (int x = 0; x < 5; ++x)
            {
                ConsoleTools.Draw(3, x + 5, "    ");
                ConsoleTools.Draw(13, x + 5, "    ");
                ConsoleTools.Draw(3, x + 5, numberTemplates[choice / 10, x]);
                ConsoleTools.Draw(13, x + 5, numberTemplates[choice % 10, x]);
            }

            // Draw the digit indicators
            ConsoleTools.Draw(14 - 10 * digit, 3, Constants.CharValue("menuPortalIndicatorTop"));
            ConsoleTools.Draw(14 - 10 * digit, 11, Constants.CharValue("menuPortalIndicatorBottom"));
            ConsoleTools.Draw(4 + 10 * digit, 3, ' ');
            ConsoleTools.Draw(4 + 10 * digit, 11, ' ');
        }
    }
}
