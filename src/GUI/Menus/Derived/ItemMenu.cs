using System;

using MastermindRPG.Creatures;
using MastermindRPG.Data;
using MastermindRPG.Data.Structures.List;
using MastermindRPG.GUI.Menus.Extras;
using MastermindRPG.Items;

namespace MastermindRPG.GUI.Menus
{
    /// <summary>
    /// Contains the initialization shared between
    /// the inventory menu and the shop menu
    /// (due to both having very similar designs
    /// and using the same items in the same order
    /// with just about all of the same labels)
    /// </summary>
    abstract class ItemMenu : Menu
    {
        /// <summary>
        /// List of item names for checking
        /// </summary>
        private static readonly string[] itemNames = { "RecoveryLow", "RecoveryMed", "RecoveryHigh", "WarpCrystal", "MaskingDew", "Smokebomb" };
        
        /// <summary>
        /// Item effects
        /// </summary>
        private static readonly string[] effectLine1 = { Constants.StringValue("recoveryLowDescription0"), Constants.StringValue("recoveryMedDescription0"), Constants.StringValue("recoveryHighDescription0"), Constants.StringValue("warpCrystalDescription0"), Constants.StringValue("maskingDewDescription0"), Constants.StringValue("smokebombDescription0") };
        private static readonly string[] effectLine2 = { Constants.StringValue("recoveryLowDescription1"), Constants.StringValue("recoveryMedDescription1"), Constants.StringValue("recoveryHighDescription1"), Constants.StringValue("warpCrystalDescription1"), Constants.StringValue("maskingDewDescription1"), Constants.StringValue("smokebombDescription1") };
        private static readonly string[] effectLine3 = { Constants.StringValue("recoveryLowDescription2"), Constants.StringValue("recoveryMedDescription2"), Constants.StringValue("recoveryHighDescription2"), Constants.StringValue("warpCrystalDescription2"), Constants.StringValue("maskingDewDescription2"), Constants.StringValue("smokebombDescription2") };
        private static readonly string[] effectLine4 = { Constants.StringValue("recoveryLowDescription3"), Constants.StringValue("recoveryMedDescription3"), Constants.StringValue("recoveryHighDescription3"), Constants.StringValue("warpCrystalDescription3"), Constants.StringValue("maskingDewDescription3"), Constants.StringValue("smokebombDescription3") };
        private static readonly string[] effectLine5 = { Constants.StringValue("recoveryLowDescription4"), Constants.StringValue("recoveryMedDescription4"), Constants.StringValue("recoveryHighDescription4"), Constants.StringValue("warpCrystalDescription4"), Constants.StringValue("maskingDewDescription4"), Constants.StringValue("smokebombDescription4") };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="designId">Id to pass to the base class constructor</param>
        public ItemMenu(int designId) : base(designId) { }

        /// <summary>
        /// Applies the labels and menu parameters
        /// that are used by both the inventory menu
        /// and the shop menu
        /// </summary>
        protected void Initialize()
        {
            choices = new string[] { "0", "1", "2", "3", "4", "5" };
            indicator = Constants.CharValue("menuItemIndicator");
            horizontalCoordinate = 2;
            verticalOffset = 2;
            verticalScale = 2;
        }

        /// <summary>
        /// Loads the labels shared between the
        /// inventory and shop menus
        /// </summary>
        protected void LoadItemLabels()
        {
            string[] quantities = new string[Adventure.Human.Inventory.Size];
            int index = 0;
            foreach (Consumable c in Adventure.Human.Inventory)
                quantities[index++] = c.Quantity + "";

            labels.Add(new BlankLabel("  ", 31, 1));
            labels.Add(new StatReference(Adventure.Human.Money, 31, 1));
            labels.Add(new BlankLabel("  ", 30, 5));
            labels.Add(new StatListReference(30, 5, quantities));
            labels.Add(new BlankLabel("             ", 23, 9));
            labels.Add(new DynamicLabel<string>(effectLine1, 23, 9));
            labels.Add(new BlankLabel("             ", 23, 10));
            labels.Add(new DynamicLabel<string>(effectLine2, 23, 10));
            labels.Add(new BlankLabel("             ", 23, 11));
            labels.Add(new DynamicLabel<string>(effectLine3, 23, 11));
            labels.Add(new BlankLabel("             ", 23, 12));
            labels.Add(new DynamicLabel<string>(effectLine4, 23, 12));
            labels.Add(new BlankLabel("             ", 23, 13));
            labels.Add(new DynamicLabel<string>(effectLine5, 23, 13));
        }
    }
}
