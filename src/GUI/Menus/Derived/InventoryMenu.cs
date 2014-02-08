using System;

using MastermindRPG.GUI.Menus.Extras;

namespace MastermindRPG.GUI.Menus
{
    /// <summary>
    /// Menu Type: Inventory
    /// 
    /// Initializes a menu with the
    /// parameters for the inventory
    /// </summary>
    class InventoryMenu : ItemMenu
    {
        /// <summary>
        /// Shop Menu constructor
        /// </summary>
        public InventoryMenu() : base(ConsoleTools.inventory)
        {
            base.Initialize();
        }

        /// <summary>
        /// Reloads the labels for the inventory
        /// </summary>
        protected override void LoadLabels()
        {
            labels.Clear();
            base.LoadItemLabels();
        }
    }
}
