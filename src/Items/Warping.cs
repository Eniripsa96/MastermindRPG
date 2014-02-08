using System;

using MastermindRPG.Creatures;
using MastermindRPG.Items.Derived;

namespace MastermindRPG.Items
{
    /// <summary>
    /// Warping-type Items
    /// Warps the player to the given destination
    /// 
    /// Derived Classes:
    ///     WarpCrystal
    ///     (More are planned)
    /// </summary>
    abstract class Warping : Consumable
    {
        /// <summary>
        /// Where to port to {area, roomHorizontalLocation, roomVerticalLocation, hoizontalPosition, verticalPosition}
        /// </summary>
        protected int[] destination;

        /// <summary>
        /// Warping-type Item Constructor
        /// </summary>
        /// <param name="quantity">initial quantity</param>
        /// <param name="maxQuantity">maximum quantity</param>
        /// <param name="destination">warping location</param>
        public Warping(int quantity, int maxQuantity, int[] destination) 
        {
            this.quantity = quantity;
            this.maxQuantity = maxQuantity;
            this.destination = destination;
        }

        /// <summary>
        /// Uses the item to warp to another location
        /// </summary>
        /// <returns>true if the potion was consumed</returns>
        public override Boolean Use(Human human)
        {
            // Cannot use a crystal when the player is already in the destination map or when there is no item to use
            if (human.battling == 0 && human.area != destination[0] && quantity > 0)
            {
                human.MoveToWorld(destination);
                if (this is WarpCrystal)
                    quantity--;
                return true;
            }
            else
                return false;
        }
    }
}
