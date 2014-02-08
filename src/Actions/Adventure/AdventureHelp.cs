using System;

using MastermindRPG.GUI;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Action for general help
    /// 
    /// Displays the adventure help screen
    /// </summary>
    class AdventureHelp
    {
        /// <summary>
        /// Displays the help screen
        /// </summary>
        public static void Action()
        {
            ConsoleTools.DrawDesign(ConsoleTools.help);
            ConsoleTools.Pause();
        }
    }
}
