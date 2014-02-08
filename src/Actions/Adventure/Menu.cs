using System;

using MastermindRPG.GUI;
using MastermindRPG.GUI.Menus.Derived;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Action for the game menu
    /// 
    /// Handles the game menu
    /// </summary>
    class Menu
    {
        /// <summary>
        /// Opens the game menu
        /// </summary>
        /// <returns>the result from the menu</returns>
        public static string Action()
        {
            GameMenu menu = new GameMenu();
            string result = "";
            do
            {
                // If an input did not exit the loop,
                // execute it.
                if (result.Length != 0)
                {
                    MastermindRPG.Actions.Action.Execute(result);
                    ConsoleTools.DrawDesign(ConsoleTools.menu);
                }
                result = menu.Show();
            }
            while (!result.Equals("") && !result.Equals("Save"));
            return result;
        }
    }
}
