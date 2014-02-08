using System;

using MastermindRPG.Data;

namespace MastermindRPG.Creatures
{
    /// <summary>
    /// Enemy Class: Boss
    /// 
    /// Creates an enemy with boss stats
    /// </summary>
    class Boss : Enemy
    {
        /// <summary>
        /// Boss Constructor
        /// </summary>
        /// <param name="id">area id</param>
        public Boss(int id)
            : base( 
                id == Constants.IntValue("worldLimit") 
                    ? Constants.IntValue("bossFinalHealth")     
                    : id * Constants.IntValue("bossHealthMultiplier") + Constants.IntValue("bossHealthBonus"),         // Health
                
                id == Constants.IntValue("worldLimit") 
                    ? Constants.IntValue("bossFinalLevel")      
                    : id * Constants.IntValue("bossLevelMultiplier") + Constants.IntValue("bossLevelBonus"),           // Level

                id == Constants.IntValue("worldLimit") 
                    ? Constants.IntValue("bossFinalExperience") 
                    : id * Constants.IntValue("bossExperienceMultiplier") + Constants.IntValue("bossExperienceBonus")) // Experience
        { }
    }
}
