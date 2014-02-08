using System;

using MastermindRPG.Data;
namespace MastermindRPG.Items
{
    /// <summary>
    /// Item Type: Quest Item
    /// 
    /// These are displayed in the
    /// dungeons and are used for 
    /// completing quests for
    /// NPCs in the town.
    /// </summary>
    public class QuestItem
    {
        /// <summary>
        /// Quest item names
        /// </summary>
        private static readonly string[] itemNames = 
        { 
            Constants.StringValue("nPC0Item"),
            Constants.StringValue("nPC1Item"),
            Constants.StringValue("nPC2Item"),
            Constants.StringValue("nPC3Item"),
            Constants.StringValue("nPC4Item"),
            Constants.StringValue("nPC5Item")
        };

        /// <summary>
        /// The area that the item is located within
        /// </summary>
        private int areaId;

        /// <summary>
        /// The ID of the corresponding quest
        /// </summary>
        private int questId;

        /// <summary>
        /// The room the item is located within
        /// </summary>
        private int[] room;

        /// <summary>
        /// The horizontal position of the item
        /// </summary>
        private int horizontalPosition;

        /// <summary>
        /// The vertical position of the item
        /// </summary>
        private int verticalPosition;

        /// <summary>
        /// Whether or not the item was found
        /// </summary>
        private Boolean obtained;

        /// <summary>
        /// Returns the quest id
        /// </summary>
        public int QuestId
        {
            get { return questId; }
        }

        /// <summary>
        /// Returns the item name
        /// </summary>
        public string Name
        {
            get { return itemNames[questId % itemNames.Length]; }
        }

        /// <summary>
        /// Returns whether or not the item is obtained
        /// </summary>
        public Boolean Owned
        {
            get { return obtained; }
        }

        /// <summary>
        /// Returns the token of the item
        /// </summary>
        public char Token
        {
            get { return Constants.CharValue("questItemToken"); }
        }

        /// <summary>
        /// Returns the monetary reward for the quest
        /// </summary>
        public int Reward
        {
            get { return areaId / Constants.IntValue("questRewardDivisor") + Constants.IntValue("questRewardBonus"); }
        }

        /// <summary>
        /// Returns the area the item is located in
        /// </summary>
        public int Area
        {
            get { return areaId; }
        }

        /// <summary>
        /// Manages the quest item's location
        /// </summary>
        public int[] Location
        {
            get { return new int[] { areaId, room[0], room[1], horizontalPosition, verticalPosition }; }
            set 
            { 
                room[0] = value[0]; 
                room[1] = value[1]; 
                horizontalPosition = value[2]; 
                verticalPosition = value[3]; 
            }
        }

        /// <summary>
        /// Quest Item Constructor
        /// </summary>
        /// <param name="questId">The ID for the quest</param>
        /// <param name="areaId">The ID for the area</param>
        /// <param name="token">token for the item</param>
        /// <param name="name">name of the item</param>
        /// <param name="reward">monetary reward</param>
        public QuestItem(int questId, int areaId)
        {
            this.questId = questId;
            this.areaId = areaId;
            room = new int[2];
        }

        /// <summary>
        /// Increments the quantity owned of the quest item
        /// </summary>
        /// <param name="quantity">amount to add</param>
        /// <returns>true (always can add it)</returns>
        public Boolean Add(int quantity)
        {
            obtained = true;
            return true;
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return questId + ":" + areaId;
        }
    }
}
