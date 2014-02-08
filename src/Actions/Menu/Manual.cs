using System;

using MastermindRPG.GUI;
using MastermindRPG.GUI.Menus;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Action for the game manual
    /// 
    /// Displays and handles the game manual
    /// and the pages found within
    /// </summary>
    class Manual
    {
        /// <summary>
        /// Opens the game manual
        /// </summary>
        public static void Action()
        {
            ManualMenu menu = new ManualMenu();
            string result = "";
            do
            {
                result = menu.Show();

                if (result.Equals("General"))
                    ConsoleTools.DrawDesign(ConsoleTools.help);
                else if (result.Equals("Battling"))
                    Battle.Help();
                else if (result.Equals("Stats"))
                    ConsoleTools.DrawDesign(ConsoleTools.statHelp);
                else if (result.Equals("Skills"))
                    ConsoleTools.DrawDesign(ConsoleTools.skillHelp);
                else if (result.Equals("Quests"))
                    ConsoleTools.DrawDesign(ConsoleTools.questHelp);
                else if (result.Equals("Saving"))
                    ConsoleTools.DrawDesign(ConsoleTools.saveHelp);

                if (!result.Equals("Battling") && !result.Equals(""))
                    ConsoleTools.Pause();

                ConsoleTools.DrawDesign(ConsoleTools.manual);
            }
            while (!result.Equals(""));
        }
    }
}
