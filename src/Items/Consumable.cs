using System;
using System.Linq;
using System.Reflection;

using MastermindRPG.Creatures;
using MastermindRPG.Data.Structures.List;

namespace MastermindRPG.Items
{
    /// <summary>
    /// Consumable-type item (not quest items) 
    /// These items can be used
    /// 
    /// Derived Classes:
    ///     Potion
    ///         RecoveryLow
    ///         RecoveryMed
    ///         RecoveryHigh
    ///     Warping
    ///         Warp Crystal
    ///         (Other item(s) planned)
    ///     Smokebomb
    ///     Masking Dew
    /// </summary>
    abstract class Consumable : Item
    {
        /// <summary>
        /// Item Type constants
        /// </summary>
        private static readonly string itemTypePrefix = "MastermindRPG.Items.Derived";
        private static readonly string[] types = { "RecoveryLow", "RecoveryMed", "RecoveryHigh", "WarpCrystal", "MaskingDew", "Smokebomb" };

        /// <summary>
        /// The quantity owned of the item
        /// </summary>
        protected int quantity;

        /// <summary>
        /// The max amount of the item a player can hold
        /// </summary>
        protected int maxQuantity;

        /// <summary>
        /// Returns the quantity
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
        }

        /// <summary>
        /// Returns an empty inventory
        /// </summary>
        public static SimpleList<Consumable> EmptyInventory
        {
            get 
            {
                // Create an array of the items
                SimpleList<Consumable> items = new SimpleList<Consumable>();
                foreach (string s in types)
                {
                    Type type = Type.GetType(itemTypePrefix + "." + s);
                    items.Add((Consumable)Activator.CreateInstance(type, 0));
                }

                return items;
            }
        }

        /// <summary>
        /// Adds a quantity to the item
        /// </summary>
        /// <param name="quantity">The quantity being added</param>
        /// <returns>true if the quantity could be added</returns>
        public Boolean Add(int quantity)
        {
            if (quantity + this.quantity > maxQuantity)
                return false;
            this.quantity += quantity;
            return true;
        }

        /// <summary>
        /// Uses the item
        /// </summary>
        /// <param name="world">The world using it from</param>
        /// <param name="battling">Whether or not it is being used in battle</param>
        /// <returns>true if the item was consumed</returns>
        public virtual Boolean Use(Human human) { return false; }
    }
}
