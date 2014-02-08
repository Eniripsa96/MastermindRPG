using System;

using MastermindRPG.Creatures;
using MastermindRPG.GUI;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Action for displaying player info
    /// 
    /// Displays the player info then returns
    /// </summary>
    class PlayerInfo
    {
        /// <summary>
        /// Displays player info
        /// </summary>
        public static void Action()
        {
            ConsoleTools.DrawDesign(ConsoleTools.playerInfo);
            ConsoleTools.Draw(12, 3, Adventure.Human.Level);
            ConsoleTools.Draw(12, 4, Adventure.Human.Exp);
            ConsoleTools.Draw(12, 5, Adventure.Human.SkillPoints);
            ConsoleTools.Draw(12, 6, Adventure.Human.Health);
            ConsoleTools.Draw(12, 7, Adventure.Human.ProgressPercent + "%");
            ConsoleTools.Pause();
        }
    }
}
