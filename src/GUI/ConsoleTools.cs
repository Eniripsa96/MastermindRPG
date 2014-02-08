using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using MastermindRPG.Data;
using MastermindRPG.Properties;
using MastermindRPG.Threads;

namespace MastermindRPG.GUI
{
    /// <summary>
    /// Contains methods for abstracted interaction
    /// with the console
    /// </summary>
    static class ConsoleTools
    {
        # region resource strings

        /// <summary>
        /// Resources in the project
        /// </summary>
        private static string[] resources = new string[]
        {
            // Multi-line designs
            Resources.Adventure,
            Resources.AdventureHelp,
            Resources.Battlefield,
            Resources.Battlefield,
            Resources.BattleHelp1,
            Resources.BattleHelp2,
            Resources.BattleHelp3,
            Resources.Credits,
            Resources.FileSelection,
            Resources.GameManual,
            Resources.GameMenu,
            Resources.Inn,
            Resources.Inventory,
            Resources.Logo,
            Resources.MainMenu,
            Resources.NPC,
            Resources.PlayerInfo,
            Resources.Quests,
            Resources.QuestHelp,
            Resources.SaveHelp,
            Resources.Shop,
            Resources.SkillHelp,
            Resources.SkillMenu,
            Resources.StatHelp,
            Resources.WorldPortal,

            // Single-line notifications
            Resources.AlreadyUnlocked,
            Resources.CannotUseItem,
            Resources.ErrorLoadingFile,
            Resources.NoSaveFileFound,
            Resources.NotEnoughMoney,
            Resources.NotEnoughPoints,
            Resources.TooManyItems
        };

        # endregion resource strings

        # region resource indexes

        /// <summary>
        /// Indexes for various designs in the resource array
        /// </summary>
        public static readonly int
            mapGui = 0,
            help = 1,
            battlefield = 2,
            battlefieldPractice = 3,
            battleHelp1 = 4,
            battleHelp2 = 5,
            battleHelp3 = 6,
            credits = 7,
            fileSelection = 8,
            manual = 9,
            menu = 10,
            inn = 11,
            inventory = 12,
            logo = 13,
            mainMenu = 14,
            npc = 15,
            playerInfo = 16,
            quests = 17,
            questHelp = 18,
            saveHelp = 19,
            shop = 20,
            skillHelp = 21,
            skillMenu = 22,
            statHelp = 23,
            portal = 24,
            alreadyUnlocked = 25,
            cannotUseItem = 26,
            errorLoadingFile = 27,
            noSaveFile = 28,
            notEnoughMoney = 29,
            notEnoughPoints = 30,
            tooManyItems = 31;

        # endregion resource indexes

        # region resource sizes

        /// <summary>
        /// The sizes of each of the designs
        /// </summary>
        private static readonly int[,] designSizes = new int[,]
        {
            {51, 19}, // Map GUI
            {75, 26}, // General Help
            {65, 24}, // Battlefield GUI
            {65, 26}, // Battlefield Practice GUI
            {54, 25}, // Battle Help Page 1
            {91, 28}, // Battle Help Page 2
            {58, 41}, // Battle Help Page 3
            {25, 5},  // Credits
            {20, 16}, // File Selection GUI
            {23, 19}, // Manual
            {23, 19}, // Game Menu
            {30, 3},  // Inn Notification
            {38, 17}, // Inventory GUI
            {116, 40},// Logo
            {67, 18}, // Main Menu GUI
            {43, 17}, // NPC GUI
            {23, 9},  // Player Info GUI
            {43, 15}, // Quests GUI
            {44, 26}, // Quests Help
            {61, 15}, // Save Help
            {38, 17}, // Shop GUI
            {60, 15}, // Skill Help
            {38, 25}, // Skill Menu GUI
            {70, 26}, // Stat Help
            {20, 16}, // Portal GUI
            {32, 2},  // Already unlocked notification
            {36, 2},  // Cannot use item notification
            {53, 2},  // Error Reading File notification
            {25, 2},  // No Save Files Found notification
            {29, 2},  // Not enough money notification
            {34, 2},  // Not enough stat points noticiation
            {34, 2}   // Too many items notification
        };

        # endregion resource sizes

        /// <summary>
        /// The starting index of notifications
        /// </summary>
        private static readonly int notifications = 25;

        /// <summary>
        /// Draws the object on the console at
        /// the given coordinates
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        /// <param name="o">the ouput object</param>
        public static void Draw(int x, int y, object o, params ConsoleColor[] args)
        {
            if (args.Length == 0)
                Console.ForegroundColor = Constants.ColorValue("defaultForegroundColor");
            else
                Console.ForegroundColor = args[0];
            if (args.Length == 2)
                Console.BackgroundColor = args[1];
            else
                Console.BackgroundColor = Constants.ColorValue("defaultBackgroundColor");
            Console.SetCursorPosition(x, y);
            Console.Write(o.ToString(), args);
        }

        /// <summary>
        /// Draws the design with the given id to the console
        /// </summary>
        /// <param name="resourceId">the design id</param>
        public static void DrawDesign(int resourceId)
        {
            Console.BackgroundColor = Constants.ColorValue("designBackgroundColor");
            Console.ForegroundColor = Constants.ColorValue("designForegroundColor");
            CenterConsole(designSizes[resourceId, 0], designSizes[resourceId, 1]);
            Console.Write(resources[resourceId]);

            if (resourceId >= notifications)
                Pause();
        }

        /// <summary>
        /// Pauses the game so the user can read a message
        /// </summary>
        public static void Pause()
        {
            ConsoleKey key;
            do
            {
                System.Threading.Thread.Sleep(50);
                key = KeyInput.Key;
            }
            while (key == ConsoleKey.NoName);
        }

        /// <summary>
        /// Centers the console window
        /// </summary>
        public static void CenterConsole(int w, int h)
        {
            // Clear the window
            Console.Clear();

            int maxW = w;
            int maxH = h;

            // Limit the screen size to the max supported by the computer
            if (w > Console.LargestWindowWidth)
                maxW = Console.LargestWindowWidth;
            if (h > Console.LargestWindowHeight)
                maxH = Console.LargestWindowHeight;

            // Set the window size and buffer
            Console.SetWindowSize(maxW, maxH);
            Console.SetBufferSize(w, h);

            // Wait for it to update
            Thread.Sleep(20);

            // http://stackoverflow.com/questions/2888824/console-setwindowposition-centered-each-and-every-time
            # region borrowed code

            // Get the console and window details
            IntPtr consoleWindow = GetConsoleWindow();
            Rectangle rectangle;
            GetWindowRect(consoleWindow, out rectangle);
            Screen screen = Screen.FromPoint(new Point(rectangle.left, rectangle.top));

            // Center the console
            int x = screen.WorkingArea.Left + (screen.WorkingArea.Width - (rectangle.right - rectangle.left)) / 2;
            int y = screen.WorkingArea.Top + (screen.WorkingArea.Height - (rectangle.bottom - rectangle.top)) / 2;
            MoveWindow(consoleWindow, x, y, rectangle.right - rectangle.left, rectangle.bottom - rectangle.top, false);

            # endregion borrowed code
        }

        # region borroed code

        // Framework for centering the console
        // http://stackoverflow.com/questions/2888824/console-setwindowposition-centered-each-and-every-time
        private struct Rectangle { public int left, top, right, bottom; }
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rectangle rc);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);

        # endregion borrowed code
    }
}