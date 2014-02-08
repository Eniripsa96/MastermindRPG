using System;

using MastermindRPG.Data;
using MastermindRPG.Data.Structures.List;
using MastermindRPG.GUI.Menus.Extras;

namespace MastermindRPG.GUI.Menus.Derived
{
    /// <summary>
    /// Menu Type: Game Menu
    /// 
    /// Initializes a menu with the
    /// parameters for the game menu
    /// </summary>
    class GameMenu : Menu
    {
        /// <summary>
        /// Initializes the menu parameters for
        /// the game menu
        /// </summary>
        public GameMenu() : base(ConsoleTools.menu)
        {
            choices = new string[] { "PlayerInfo", "Skills", "Quests", "Manual", "Save", "Practice" };
            indicator = Constants.CharValue("menuGameIndicator");
            horizontalCoordinate = 4;
            verticalOffset = 4;
            verticalScale = 2;
            labels = new SimpleList<Label>();
        }
    }
}
