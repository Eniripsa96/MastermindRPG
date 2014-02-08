using System;

using MastermindRPG.Creatures;

namespace MastermindRPG.Items
{
    /// <summary>
    /// Potions
    /// 
    /// heals the player for various amounts 
    /// depending on whether they are battling or not
    /// 
    /// Derived Classes:
    ///     RecoveryLow
    ///     RecoveryMed
    ///     RecoveryHigh
    /// </summary>
    abstract class Potion : Consumable
    {
        /// <summary>
        /// The amount the potion heals while battling
        /// </summary>
        protected int battleHealAmount;

        /// <summary>
        /// The amount the potion heals while moving in the world
        /// </summary>
        protected int fieldHealAmount;

        /// <summary>
        /// Potion-type item constructor
        /// </summary>
        /// <param name="quantity">initial quantity</param>
        /// <param name="battleHealAmount">restore amount in battles</param>
        /// <param name="fieldHealAmount">restore amount out of battles</param>
        /// <param name="maxQuantity">maximum quantity</param>
        public Potion(int quantity, int battleHealAmount, int fieldHealAmount, int maxQuantity) 
        {
            this.quantity = quantity;
            this.battleHealAmount = battleHealAmount;
            this.fieldHealAmount = fieldHealAmount;
            this.maxQuantity = maxQuantity;
        }

        /// <summary>
        /// Uses the potion to heal the player
        /// </summary>
        /// <returns>true if the potion was consumed</returns>
        public override Boolean Use(Human human)
        {
            // The human must be damaged in order to use a potion
            if (human.IsDamaged && quantity > 0)
            {
                if (human.battling > 0)
                    human.Damage(-battleHealAmount);
                else
                    human.Damage(-fieldHealAmount);
                human.LimitHp();
                quantity--;
                return true;
            }
            else
                return false;
        }
    }
}
