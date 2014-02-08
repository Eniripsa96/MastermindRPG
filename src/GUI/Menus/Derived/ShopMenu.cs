using System;

using MastermindRPG.Data;
using MastermindRPG.GUI.Menus.Extras;

namespace MastermindRPG.GUI.Menus
{
    /// <summary>
    /// Menu Class: ShopMenu
    /// 
    /// Initializes the menu fields for the Shop Menu
    /// </summary>
    class ShopMenu : ItemMenu
    {
        /// <summary>
        /// Item costs
        /// </summary>
        protected readonly int[] shopCosts = 
        {
            Constants.IntValue("recoveryLowCost"),
            Constants.IntValue("recoveryMedCost"),
            Constants.IntValue("recoveryHighCost"),
            Constants.IntValue("warpCrystalCost"),
            Constants.IntValue("maskingDewCost"),
            Constants.IntValue("smokebombCost")
        };

        /// <summary>
        /// Shop Menu constructor
        /// </summary>
        public ShopMenu() : base(ConsoleTools.shop)
        {
            base.Initialize();
        }

        /// <summary>
        /// Reloads the labels for the shop
        /// </summary>
        protected override void LoadLabels()
        {
            labels.Clear();
            base.LoadItemLabels();
            labels.Add(new BlankLabel("  ", 30, 3));
            labels.Add(new DynamicLabel<int>(shopCosts, 30, 3));
        }
    }
}
