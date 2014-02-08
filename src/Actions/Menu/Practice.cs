using System;

using MastermindRPG.Creatures;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Action for a practice battle
    /// 
    /// Initializes a battle with practice characters
    /// </summary>
    class Practice
    {
        /// <summary>
        /// Begins a practice battle
        /// </summary>
        public static void Action()
        {
            Human practiceHuman = new Human(999, 999);
            Enemy practiceEnemy = new Minion(-1);
            Battle.Fight(practiceHuman, practiceEnemy);
        }
    }
}
