using System;

using MastermindRPG.Creatures;
using MastermindRPG.Data.IO;
using MastermindRPG.GUI;
using MastermindRPG.World;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Action for continuing a game
    /// 
    /// Loads a save file and then begins the adventure
    /// </summary>
    class Continue
    {
        /// <summary>
        /// Action method (run from the Action class)
        /// </summary>
        public static void Action()
        {
            int file = FileSelect.Open(false);
            if (file < 0)
            {
                if (file == -1)
                    ConsoleTools.DrawDesign(ConsoleTools.noSaveFile);
                return;
            }
            Human human;
            Map map;
            Load.LoadGame(out human, out map, file);
            Adventure.Begin(human, map);
        }
    }
}
