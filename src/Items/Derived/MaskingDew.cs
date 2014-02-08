using System;

using MastermindRPG.Creatures;
using MastermindRPG.Data;

namespace MastermindRPG.Items.Derived
{
    /// <summary>
    /// Masking Dew Item
    /// 
    /// reduces encounter rate
    /// </summary>
    class MaskingDew : Consumable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="quantity">initial quantity</param>
        public MaskingDew(int quantity)
        {
            this.quantity = quantity;
            maxQuantity = Constants.IntValue("maskingDewCapacity");
        }

        /// <summary>
        /// Use the masking dew
        /// </summary>
        /// <param name="human">Player using the item</param>
        /// <returns>true if the item was successfully used</returns>
        public override Boolean Use(Human human)
        {
            // Cannot use if its effect is already active or there is none to use
            if (human.dewSteps > 0 || quantity == 0 || human.battling > 0)
                return false;

            human.dewSteps = Constants.IntValue("maskingDewDuration");
            quantity--;
            return true;
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>ID:Quantity</returns>
        public override string ToString()
        {
            return "4:" + quantity;
        }
    }
}
