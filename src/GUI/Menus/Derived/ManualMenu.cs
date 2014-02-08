using System;

using MastermindRPG.Data;
using MastermindRPG.Data.Structures.List;
using MastermindRPG.GUI.Menus.Extras;

namespace MastermindRPG.GUI.Menus
{
    /// <summary>
    /// Menu Type: Manual
    /// 
    /// Initializes a menu with the
    /// parameters for the manual
    /// </summary>
    class ManualMenu : Menu
    {
        /// <summary>
        /// Initializes the menu parameters for
        /// the manual
        /// </summary>
        public ManualMenu() : base(ConsoleTools.manual)
        {
            choices = new string[] { "General", "Battling", "Stats", "Skills", "Quests", "Saving" };
            indicator = Constants.CharValue("menuManualIndicator");
            horizontalCoordinate = 4;
            verticalOffset = 4;
            verticalScale = 2;
        }
    }
}
