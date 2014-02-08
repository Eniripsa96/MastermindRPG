using System;

namespace MastermindRPG.Items
{
    /// <summary>
    /// Item structure for polymorphism
    /// 
    /// Derived Classes:
    ///     Consumable
    ///     QuestItem (Planned)
    /// </summary>
    public interface Item
    {
        /// <summary>
        /// Adds a quantity to the item
        /// </summary>
        /// <param name="quantity">The quantity to add</param>
        Boolean Add(int quantity);
    }
}
