using System;

using MastermindRPG.GUI;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Action: Credits
    /// 
    /// Displays the credits for the game
    /// </summary>
    class Credits
    {
        /// <summary>
        /// Displays the credits
        /// </summary>
        public static void Action()
        {
            ConsoleTools.DrawDesign(ConsoleTools.credits);
            ConsoleTools.Pause();
        }
    }
}
