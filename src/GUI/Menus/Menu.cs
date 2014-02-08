using System;
using System.Threading;

using MastermindRPG.Data.Structures.List;
using MastermindRPG.GUI.Menus.Extras;
using MastermindRPG.Threads;

namespace MastermindRPG.GUI.Menus
{
    /// <summary>
    /// Menu Class
    /// 
    /// Handles the menu controls for basic menus
    /// 
    /// Derived Classes:
    ///     MainMenu
    ///     ShopMenu
    /// </summary>
    abstract class Menu
    {
        /// <summary>
        /// The list of extra displays outside the design
        /// that need to be drawn to the console
        /// </summary>
        protected SimpleList<Label> labels;

        /// <summary>
        /// The list of choices in the menu (will be returned)
        /// </summary>
        protected string[] choices;

        /// <summary>
        /// The indicator character
        /// </summary>
        protected char indicator;

        /// <summary>
        /// The horizontal position of the cursor (fixed)
        /// </summary>
        protected int horizontalCoordinate;

        /// <summary>
        /// The vertical offset of the cursor (position for choice == 0)
        /// </summary>
        protected int verticalOffset;

        /// <summary>
        /// The vertical position change per choice increment
        /// </summary>
        protected int verticalScale;

        /// <summary>
        /// The currently selected choice
        /// </summary>
        private int choice;

        /// <summary>
        /// The actual vertical position for drawing
        /// </summary>
        private int VerticalCoordinate
        {
            get { return choice * verticalScale + verticalOffset; }
        }

        /// <summary>
        /// Draws the menu initially
        /// </summary>
        public Menu(int designId)
        {
            ConsoleTools.DrawDesign(designId);
            choice = 0;
            labels = new SimpleList<Label>();
        }

        /// <summary>
        /// Opens the menu
        /// </summary>
        /// <returns>The chosen option</returns>
        public string Show()
        {
            LoadLabels();

            ConsoleKey key;

            // Draw the menu
            ConsoleTools.Draw(horizontalCoordinate, VerticalCoordinate, indicator);
            DrawLabels();

            // Get user input, moving the cursor as needed
            do
            {
                Thread.Sleep(100);
                key = KeyInput.Key;

                if (key != ConsoleKey.UpArrow && key != ConsoleKey.DownArrow)
                    continue;

                ConsoleTools.Draw(horizontalCoordinate, VerticalCoordinate, " ");

                if (key == ConsoleKey.UpArrow)
                    choice = (choice + choices.Length - 1) % choices.Length;
                else if (key == ConsoleKey.DownArrow)
                    choice = (choice + 1) % choices.Length;

                ConsoleTools.Draw(horizontalCoordinate, VerticalCoordinate, indicator);
                DrawLabels();
            }
            while (key != ConsoleKey.Escape && key != ConsoleKey.Spacebar);

            // Exit if escape was pressed
            if (key == ConsoleKey.Escape)
                return "";

            // Else return the current choice
            return choices[choice];
        }

        /// <summary>
        /// Draws all of the labels to the console
        /// </summary>
        private void DrawLabels()
        {
            foreach (Label l in labels)
            {
                ConsoleTools.Draw(l.X, l.Y, l[choice]);
            }
        }

        /// <summary>
        /// For use in children
        /// 
        /// Loads/updates all of the labels for the
        /// associated menu
        /// </summary>
        protected virtual void LoadLabels() { }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetType().ToString();
        }
    }
}
