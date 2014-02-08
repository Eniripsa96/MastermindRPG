using System;

using MastermindRPG.Creatures;
using MastermindRPG.Data;

namespace MastermindRPG.Items.Derived
{
    /// <summary>
    /// Smokebomb item
    /// 
    /// Escapes from a battle
    /// </summary>
    class Smokebomb : Consumable
    {
        /// <summary>
        /// Smokebomb constructor
        /// </summary>
        /// <param name="quantity">initial quantity</param>
        public Smokebomb(int quantity)
        {
            this.quantity = quantity;
            maxQuantity = Constants.IntValue("smokebombCapacity");
        }

        /// <summary>
        /// Smokebomb effect
        /// </summary>
        /// <param name="human">player using the item</param>
        /// <returns></returns>
        public override Boolean Use(Human human)
        {
            // The human must be in a battle to use it
            if (human.battling != 1 || quantity == 0)
                return false;

            human.battling = 0;
            quantity--;
            return true;
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>ID:Quantity</returns>
        public override string ToString()
        {
            return "5:" + quantity;
        }
    }
}
