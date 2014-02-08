using System;

using MastermindRPG.Creatures;
using MastermindRPG.Data;
using MastermindRPG.Data.Structures.List;
using MastermindRPG.GUI.Menus.Extras;

namespace MastermindRPG.GUI.Menus
{
    /// <summary>
    /// Menu Type: Skill Menu
    /// 
    /// Initializes a menu with the
    /// parameters for the skill
    /// menu.
    /// </summary>
    class SkillMenu : Menu
    {
        # region skill info

        /// <summary>
        /// Choices for this menu
        /// </summary>
        private readonly string[] choiceStrings = { "0", "1", "2", "3", "4", "5", "6", "7", "8" };

        /// <summary>
        /// Skill detail label texts
        /// </summary>
        private readonly int[] tiers =   
        {
            Constants.IntValue("skillBetrayTier"),
            Constants.IntValue("skillSacrificeTier"),
            Constants.IntValue("skillDoubleStrikeTier"),
            Constants.IntValue("skillNegotiateTier"),
            Constants.IntValue("skillHealTier"),
            Constants.IntValue("skillRewindTier"),
            Constants.IntValue("skillVoidTier"),
            Constants.IntValue("skillOfferingTier"),
            Constants.IntValue("skillDominateTier")
        };
        private readonly int[] costs =   
        {
            Constants.IntValue("skillBetraySPCost"),
            Constants.IntValue("skillSacrificeSPCost"),
            Constants.IntValue("skillDoubleStrikeSPCost"),
            Constants.IntValue("skillNegotiateSPCost"),
            Constants.IntValue("skillHealSPCost"),
            Constants.IntValue("skillRewindSPCost"),
            Constants.IntValue("skillVoidSPCost"),
            Constants.IntValue("skillOfferingSPCost"),
            Constants.IntValue("skillDominateSPCost")
        };
        private readonly int[] apCosts = 
        {
            Constants.IntValue("skillBetrayAPCost"),
            Constants.IntValue("skillSacrificeAPCost"),
            Constants.IntValue("skillDoubleStrikeAPCost"),
            Constants.IntValue("skillNegotiateAPCost"),
            Constants.IntValue("skillHealAPCost"),
            Constants.IntValue("skillRewindAPCost"),
            Constants.IntValue("skillVoidAPCost"),
            Constants.IntValue("skillOfferingAPCost"),
            Constants.IntValue("skillDominateAPCost")
        };

        # region effects
        private readonly string[] effectLine1 = { Constants.StringValue("skillBetrayEffect0"), Constants.StringValue("skillSacrificeEffect0"), Constants.StringValue("skillDoubleStrikeEffect0"), Constants.StringValue("skillNegotiateEffect0"), Constants.StringValue("skillHealEffect0"), Constants.StringValue("skillRewindEffect0"), Constants.StringValue("skillVoidEffect0"), Constants.StringValue("skillOfferingEffect0"), Constants.StringValue("skillDominateEffect0") };
        private readonly string[] effectLine2 = { Constants.StringValue("skillBetrayEffect1"), Constants.StringValue("skillSacrificeEffect1"), Constants.StringValue("skillDoubleStrikeEffect1"), Constants.StringValue("skillNegotiateEffect1"), Constants.StringValue("skillHealEffect1"), Constants.StringValue("skillRewindEffect1"), Constants.StringValue("skillVoidEffect1"), Constants.StringValue("skillOfferingEffect1"), Constants.StringValue("skillDominateEffect1") };
        private readonly string[] effectLine3 = { Constants.StringValue("skillBetrayEffect2"), Constants.StringValue("skillSacrificeEffect2"), Constants.StringValue("skillDoubleStrikeEffect2"), Constants.StringValue("skillNegotiateEffect2"), Constants.StringValue("skillHealEffect2"), Constants.StringValue("skillRewindEffect2"), Constants.StringValue("skillVoidEffect2"), Constants.StringValue("skillOfferingEffect2"), Constants.StringValue("skillDominateEffect2") };
        private readonly string[] effectLine4 = { Constants.StringValue("skillBetrayEffect3"), Constants.StringValue("skillSacrificeEffect3"), Constants.StringValue("skillDoubleStrikeEffect3"), Constants.StringValue("skillNegotiateEffect3"), Constants.StringValue("skillHealEffect3"), Constants.StringValue("skillRewindEffect3"), Constants.StringValue("skillVoidEffect3"), Constants.StringValue("skillOfferingEffect3"), Constants.StringValue("skillDominateEffect3") };
        private readonly string[] effectLine5 = { Constants.StringValue("skillBetrayEffect4"), Constants.StringValue("skillSacrificeEffect4"), Constants.StringValue("skillDoubleStrikeEffect4"), Constants.StringValue("skillNegotiateEffect4"), Constants.StringValue("skillHealEffect4"), Constants.StringValue("skillRewindEffect4"), Constants.StringValue("skillVoidEffect4"), Constants.StringValue("skillOfferingEffect4"), Constants.StringValue("skillDominateEffect4") };
        private readonly string[] effectLine6 = { Constants.StringValue("skillBetrayEffect5"), Constants.StringValue("skillSacrificeEffect5"), Constants.StringValue("skillDoubleStrikeEffect5"), Constants.StringValue("skillNegotiateEffect5"), Constants.StringValue("skillHealEffect5"), Constants.StringValue("skillRewindEffect5"), Constants.StringValue("skillVoidEffect5"), Constants.StringValue("skillOfferingEffect5"), Constants.StringValue("skillDominateEffect5") };
        private readonly string[] effectLine7 = { Constants.StringValue("skillBetrayEffect6"), Constants.StringValue("skillSacrificeEffect6"), Constants.StringValue("skillDoubleStrikeEffect6"), Constants.StringValue("skillNegotiateEffect6"), Constants.StringValue("skillHealEffect6"), Constants.StringValue("skillRewindEffect6"), Constants.StringValue("skillVoidEffect6"), Constants.StringValue("skillOfferingEffect6"), Constants.StringValue("skillDominateEffect6") };
        private readonly string[] effectLine8 = { Constants.StringValue("skillBetrayEffect7"), Constants.StringValue("skillSacrificeEffect7"), Constants.StringValue("skillDoubleStrikeEffect7"), Constants.StringValue("skillNegotiateEffect7"), Constants.StringValue("skillHealEffect7"), Constants.StringValue("skillRewindEffect7"), Constants.StringValue("skillVoidEffect7"), Constants.StringValue("skillOfferingEffect7"), Constants.StringValue("skillDominateEffect7") };
        private readonly string[] effectLine9 = { Constants.StringValue("skillBetrayEffect8"), Constants.StringValue("skillSacrificeEffect8"), Constants.StringValue("skillDoubleStrikeEffect8"), Constants.StringValue("skillNegotiateEffect8"), Constants.StringValue("skillHealEffect8"), Constants.StringValue("skillRewindEffect8"), Constants.StringValue("skillVoidEffect8"), Constants.StringValue("skillOfferingEffect8"), Constants.StringValue("skillDominateEffect8") };
        # endregion effects

        # endregion skill info

        /// <summary>
        /// Initializes a menu with the parameters
        /// for the skill menu
        /// </summary>
        public SkillMenu() : base(ConsoleTools.skillMenu)
        {
            choices = choiceStrings;
            indicator = Constants.CharValue("menuSkillIndicator");
            horizontalCoordinate = 2;
            verticalOffset = 2;
            verticalScale = 2;
        }

        /// <summary>
        /// Laods the labels for the skill menu
        /// </summary>
        protected override void LoadLabels()
        {
            labels.Clear();

            SimpleList<Boolean> skills = Adventure.Human.Skills;
            string s = "             ";

            labels.Add(new DynamicLabel<int>(tiers, 29, 1));
            labels.Add(new BlankLabel("  ", 29, 3));
            labels.Add(new DynamicLabel<int>(costs, 29, 3));
            labels.Add(new BlankLabel("  ", 27, 5));
            labels.Add(new DynamicLabel<int>(apCosts, 27, 5));
            labels.Add(new BlankLabel(s, 23, 11));
            labels.Add(new DynamicLabel<string>(effectLine1, 23, 11));
            labels.Add(new BlankLabel(s, 23, 12));
            labels.Add(new DynamicLabel<string>(effectLine2, 23, 12));
            labels.Add(new BlankLabel(s, 23, 13));
            labels.Add(new DynamicLabel<string>(effectLine3, 23, 13));
            labels.Add(new BlankLabel(s, 23, 14));
            labels.Add(new DynamicLabel<string>(effectLine4, 23, 14));
            labels.Add(new BlankLabel(s, 23, 15));
            labels.Add(new DynamicLabel<string>(effectLine5, 23, 15)); 
            labels.Add(new BlankLabel(s, 23, 16));
            labels.Add(new DynamicLabel<string>(effectLine6, 23, 16));
            labels.Add(new BlankLabel(s, 23, 17));
            labels.Add(new DynamicLabel<string>(effectLine7, 23, 17));
            labels.Add(new BlankLabel(s, 23, 18));
            labels.Add(new DynamicLabel<string>(effectLine8, 23, 18));
            labels.Add(new BlankLabel(s, 23, 19));
            labels.Add(new DynamicLabel<string>(effectLine9, 23, 19));
            labels.Add(new StatListReference(31, 7, skills[0], skills[1], skills[2], skills[3], skills[4], skills[5], skills[6], skills[7], skills[8]));
            labels.Add(new BlankLabel("  ", 29, 21));
            labels.Add(new StatReference(Adventure.Human.SkillPoints, 29, 21));
        }
    }
}