using System;

using MastermindRPG.Creatures;
using MastermindRPG.World;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Action for a new game
    /// 
    /// Creates a new player and world then starts an adventure
    /// </summary>
    class NewGame
    {
        /// <summary>
        /// Starts a new adventure
        /// </summary>
        public static void Action()
        {
            int file = FileSelect.Open(true);
            if (file < 1)
                return;
            Human human = new Human(file);
            Map map = new Area(human.Seed, ref human);
            Adventure.Begin(human, map);
        }
    }
}
