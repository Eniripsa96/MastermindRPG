using System;

using MastermindRPG.Data;
using MastermindRPG.Data.Structures.List;
using MastermindRPG.GUI.Menus.Extras;

namespace MastermindRPG.GUI.Menus
{
    /// <summary>
    /// Menu Class: Main Menu
    /// 
    /// Initializes the menu fields to fit the main menu
    /// </summary>
    class MainMenu : Menu
    {
        /// <summary>
        /// Main Menu Constructor
        /// </summary>
        public MainMenu() : base(ConsoleTools.mainMenu)
        {
            choices = new string[] {"NewGame","Continue","Credits","Quit"};
            indicator = Constants.CharValue("menuMainIndicator");
            horizontalCoordinate = 2;
            verticalOffset = 2;
            verticalScale = 4;
        }
    }
}
