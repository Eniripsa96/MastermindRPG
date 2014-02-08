using System;

using MastermindRPG.Data;

namespace MastermindRPG.Creatures
{
    /// <summary>
    /// Actor Class: Enemy
    /// 
    /// Derived Classes:
    ///     Minion
    ///     Boss
    /// </summary>
    abstract class Enemy : Actor
    {
        /// <summary>
        /// Returns the experience reward for beating the enemy
        /// </summary>
        public int Reward
        {
            get { return exp; }
        }

        /// <summary>
        /// Gives the number of skills the enemy has
        /// </summary>
        public int SkillLimit
        {
            get
            {
                if (level < Constants.IntValue("enemySkillLevel1"))
                    return 1;
                if (level < Constants.IntValue("enemySkillLevel2"))
                    return 2;
                if (level < Constants.IntValue("enemySkillLevel3"))
                    return 3;
                if (level < Constants.IntValue("enemySkillLevel4"))
                    return 4;
                if (level < Constants.IntValue("enemySkillLevel5"))
                    return 5;
                if (level < Constants.IntValue("enemySkillLevel6"))
                    return 6;
                if (level < Constants.IntValue("enemySkillLevel7"))
                    return 7;
                if (level < Constants.IntValue("enemySkillLevel8"))
                    return 8;
                if (level < Constants.IntValue("enemySkillLevel9"))
                    return 9;
                return 10;
            }
        }

        /// <summary>
        /// Enemy Constructor
        /// </summary>
        /// <param name="hp">enemy health</param>
        /// <param name="lv">enemy level</param>
        /// <param name="ex">enemy experience reward</param>
        public Enemy(int hp, int lv, int ex)
        {
            health = hp;
            level = lv;
            exp = ex;
        }
    }
}
