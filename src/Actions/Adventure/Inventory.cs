using System;

using MastermindRPG.GUI;
using MastermindRPG.GUI.Menus;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Action: Inventory
    /// 
    /// Opens the inventory while
    /// exploring the world to use
    /// or view the currently owned
    /// items
    /// </summary>
    class Inventory
    {
        /// <summary>
        /// Opens the inventory
        /// </summary>
        public static void Action()
        {
            string result = "";
            InventoryMenu menu = new InventoryMenu();

            do
            {
                // If an input did not exit the loop,
                // execute it.
                if (result.Length != 0)
                {
                    int id = int.Parse(result);
                    if (Adventure.Human.UseItem(id))
                        return;
                    else
                    {
                        ConsoleTools.DrawDesign(ConsoleTools.cannotUseItem);
                        ConsoleTools.DrawDesign(ConsoleTools.inventory); 
                    }                  
                }

                result = menu.Show();
            }
            while (!result.Equals(""));
        }
    }
}
