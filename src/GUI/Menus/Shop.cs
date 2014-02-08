using System;

using MastermindRPG.Creatures;
using MastermindRPG.Data;
using MastermindRPG.GUI;
using MastermindRPG.GUI.Menus;

namespace MastermindRPG
{
    /// <summary>
    /// Handles the shop menu
    /// </summary>
    class Shop
    {
        /// <summary>
        /// Costs for items in the shop
        /// </summary>
        private static readonly int[] shopCosts = 
        { 
            Constants.IntValue("recoveryLowCost"), 
            Constants.IntValue("recoveryMedCost"), 
            Constants.IntValue("recoveryHighCost"), 
            Constants.IntValue("warpCrystalCost"), 
            Constants.IntValue("maskingDewCost"), 
            Constants.IntValue("smokebombCost")
        };
        
        /// <summary>
        /// Opens the shop
        /// </summary>
        /// <param name="player">The player data</param>
        public static void Open(ref Human human)
        {
            string input;
            ShopMenu menu = new ShopMenu();
            
            do
            {
                // Show the shop and get a choice
                input = menu.Show();

                // Get the item ID that was selected and
                // if the human has enough money to buy it,
                // try to buy the item.
                try
                {
                    int itemId = int.Parse(input);
                    if (human.Money >= shopCosts[itemId])
                        human.PurchaseItem(itemId, shopCosts[itemId]);
                    else
                    {
                        ConsoleTools.DrawDesign(ConsoleTools.notEnoughMoney);
                        ConsoleTools.DrawDesign(ConsoleTools.shop);
                    }
                }
                catch (Exception)
                {
                    // Pressed escape to exit
                }
            }
            while (!input.Equals(""));
        }
    }
}
