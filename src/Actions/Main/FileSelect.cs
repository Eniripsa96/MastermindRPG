using System;
using System.IO;

using MastermindRPG.Data;
using MastermindRPG.GUI;
using MastermindRPG.Threads;

namespace MastermindRPG
{
    /// <summary>
    /// Handles the interaction with the world portal
    /// </summary>
    class FileSelect
    {
        private static readonly string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/MastermindRPG/Save";

        /// <summary>
        /// The world limit
        /// </summary>
        private static readonly int fileLimit = 99;

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
        public static int Open(Boolean newFiles)
        {
            choice = 1;
            if (!LimitChoices(newFiles, 1))
                return -1;
            ConsoleKey key;

            // Draws the portal
            ConsoleTools.DrawDesign(ConsoleTools.fileSelection);
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

                LimitChoices(newFiles, (key == ConsoleKey.UpArrow ? 1 : -1));

                Refresh();
            }
            while (key != ConsoleKey.Escape);

            return -2;
        }

        /// <summary>
        /// Limits the choices according to the mode
        /// </summary>
        /// <param name="newFiles">whether or not it is for a new file</param>
        private static Boolean LimitChoices(Boolean newFiles, int change)
        {
            int cycles = 0;
            if (choice < 1)
                choice = fileLimit;
            if (choice > fileLimit)
                choice = 1;
            while (File.Exists(directory + choice) == newFiles)
            {
                cycles++;
                choice += change;
                if (choice > fileLimit)
                    choice -= fileLimit;
                if (choice < 1)
                    choice = 99;
                if (cycles == 100)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Layout for numbers displayed in the world portal
        /// </summary>
        private static readonly String[,] numberTemplates =
        {
            {
                " ##",
                "#  #",
                "#  #",
                "#  #",
                " ##"
            },
            {
                "  # ",
                " ## ",
                "  # ",
                "  # ",
                "  # ",
            },
            {
                " ## ",
                "#  #",
                "  # ",
                " #  ",
                "####"
            },
            {
                "### ",
                "   #",
                " ## ",
                "   #",
                "### "
            },
            {
                "#  #",
                "#  #",
                "####",
                "   #",
                "   #"
            },
            {
                "####",
                "#   ",
                "### ",
                "   #",
                "### "
            },
            {
                " ## ",
                "#   ",
                "### ",
                "#  #",
                " ## "
            },
            {
                "####",
                "   #",
                "  # ",
                " #  ",
                "#   "
            },
            {
                " ## ",
                "#  #",
                " ## ",
                "#  #",
                " ## "
            },
            {
                " ## ",
                "#  #",
                " ###",
                "   #",
                "  # "
            }
        };

        /// <summary>
        /// Refreshes the portal display
        /// </summary>
        private static void Refresh()
        {
            // Draw the numbers
            for (int x = 0; x < 5; ++x)
            {
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
