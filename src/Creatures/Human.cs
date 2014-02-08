using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using MastermindRPG.Data;
using MastermindRPG.Data.Structures.List;
using MastermindRPG.GUI;
using MastermindRPG.Items;

namespace MastermindRPG.Creatures
{
    /// <summary>
    /// Actor Class: Human
    /// 
    /// Contains associated methods
    /// 
    /// Fields and Properties are in HumanData.cs
    /// </summary>
    sealed partial class Human : Actor
    {
        # region fields

        /// <summary>
        /// The player's inventory
        /// </summary>
        private SimpleList<Consumable> inventory;

        /// <summary>
        /// The stat points of the player
        /// </summary>
        private int sp;

        /// <summary>
        /// The money the player has
        /// </summary>
        private int money;

        /// <summary>
        /// The remaining effect duration of Masking Dew
        /// </summary>
        public int dewSteps;

        /// <summary>
        /// Battle state of the player (0 = not battling; 1 = normal battle; 2 = boss battle)
        /// </summary>
        public int battling;

        /// <summary>
        /// The ID of the world the player is currently in
        /// </summary>
        public int area;

        /// <summary>
        /// The seeds for the areas that have been explored
        /// </summary>
        private SimpleList<int> seeds;

        /// <summary>
        /// The list of active quest items
        /// </summary>
        private SimpleList<QuestItem> questItems;

        /// <summary>
        /// The progress through the game
        /// </summary>
        public int progress;

        /// <summary>
        /// The file associated with this character
        /// </summary>
        private int saveFile;

        # endregion fields

        # region properties

        /// <summary>
        /// Returns the associated save file number
        /// </summary>
        public int SaveFile
        {
            get { return saveFile; }
        }

        /// <summary>
        /// Returns the experience required to level up
        /// </summary>
        public int RequiredExp
        {
            get { return Constants.IntValue("playerExpRequiredPerLevel") * level + Constants.IntValue("playerExpRequiredBonus"); }
        }

        /// <summary>
        /// Returns the maximum amount of HP the player can have according to level
        /// </summary>
        private int MaxHP
        {
            get { return Constants.IntValue("playerHealthPerLevel") * level + Constants.IntValue("playerHealthBonus"); }
        }

        /// <summary>
        /// Returns the player's inventory
        /// </summary>
        public SimpleList<Consumable> Inventory
        {
            get { return inventory; }
        }

        /// <summary>
        /// Returns the player's experience
        /// </summary>
        public int Exp
        {
            get { return exp; }
        }

        /// <summary>
        /// Returns the player's sp
        /// </summary>
        public int SkillPoints
        {
            get { return sp; }
        }

        /// <summary>
        /// Returns the list of seeds
        /// </summary>
        public SimpleList<int> Seeds
        {
            get { return seeds; }
        }

        /// <summary>
        /// Returns the current seed
        /// </summary>
        public int Seed
        {
            get { return seeds[area]; }
        }

        /// <summary>
        /// Returns the progress as a percentage
        /// </summary>
        public int ProgressPercent
        {
            get { return 100 * progress / 40; }
        }

        /// <summary>
        /// Returns the list of active quest items
        /// </summary>
        public SimpleList<QuestItem> QuestItems
        {
            get { return questItems; }
        }

        /// <summary>
        /// Gives money to the player
        /// </summary>
        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        # endregion properties

        # region constructors

        public Human() { }

        /// <summary>
        /// Constructor for a new player
        /// </summary>
        public Human(int file)
            : this (false, 1, Constants.IntValue("playerHealthBonus") + Constants.IntValue("playerHealthPerLevel"))
        {
            money = Constants.IntValue("playerStartMoney");
            inventory = Consumable.EmptyInventory;
            seeds = new SimpleList<int>();
            Random random = new Random();
            seeds.Add(random.Next());
            seeds.Add(random.Next());
            area = 1;
            this.saveFile = file;
        }

        /// <summary>
        /// Practice human constructor
        /// </summary>
        /// <param name="level"></param>
        /// <param name="hp"></param>
        public Human(int level, int hp)
            : this (true, level, hp)
        { }

        /// <summary>
        /// Shared Constructor
        /// </summary>
        /// <param name="s">default skill state</param>
        /// <param name="l">level</param>
        /// <param name="h">health</param>
        public Human(Boolean s, int l, int h)
        {
            level = l;
            health = h;
            maxHP = h;
            skills = new SimpleList<Boolean>();
            for (int x = 0; x < 9; ++x)
                skills.Add(s);
            questItems = new SimpleList<QuestItem>();
        }

        # endregion constructors

        # region skills

        /// <summary>
        /// Unlock a skill, consuming the corresponding number of stat points
        /// </summary>
        /// <param name="skill">the skill ID</param>
        /// <param name="cost">the stat point cost</param>
        public void UnlockSkill(int skill, int cost)
        {
            if (skills[skill])
            {
                ConsoleTools.DrawDesign(ConsoleTools.alreadyUnlocked);
                ConsoleTools.DrawDesign(ConsoleTools.skillMenu);
                return;
            }
            if (sp < cost)
            {
                ConsoleTools.DrawDesign(ConsoleTools.notEnoughPoints);
                ConsoleTools.DrawDesign(ConsoleTools.skillMenu);
                return;
            }
            sp -= cost;
            skills[skill] = true;
        }

        # endregion skills

        # region items

        /// <summary>
        /// Purchases the designated item
        /// </summary>
        /// <param name="item">The item id being purchased</param>
        /// <param name="cost">The cost of the item</param>
        public void PurchaseItem(int item, int cost)
        {
            if (inventory[item].Add(1))
                money -= cost;
            else
            {
                ConsoleTools.DrawDesign(ConsoleTools.tooManyItems);
                ConsoleTools.DrawDesign(ConsoleTools.shop);
            }
            return;
        }

        /// <summary>
        /// Uses the given item
        /// </summary>
        /// <param name="id">Uses the item at the designated inventory slot</param>
        /// <returns>true if the item was successfully used</returns>
        public Boolean UseItem(int id)
        {
            return ((Consumable)inventory[id]).Use(this);
        }

        # endregion items

        # region world interaction

        /// <summary>
        /// Moves to the designated world with the given coordinates
        /// </summary>
        /// <param name="location">The destination {world, horizontlPosition, verticalPosition}</param>
        public void MoveToWorld(int[] location)
        {
            // Set the ID
            this.area = location[0];

            // Set the player inside the world inside the beginning room
            horizontalPosition = location[1];
            verticalPosition = location[2];
        }

        /// <summary>
        /// Adds a seed to the list for a new world
        /// </summary>
        public void GenerateSeed()
        {
            seeds.Add(new Random(seeds[seeds.Size - 1]).Next());
        }

        # endregion world interaction

        # region experience

        /// <summary>
        /// Rewards a human with experience via operator
        /// </summary>
        /// <param name="human">The human receiving exp</param>
        /// <param name="exp">The amount of exp received</param>
        /// <returns>The result</returns>
        public static Human operator +(Human human, int exp)
        {
            human.exp += exp;

            // Check for a level up
            while (human.exp >= human.RequiredExp && human.level < Constants.IntValue("playerLevelMax"))
            {
                human.exp -= human.RequiredExp;
                human.level++;
                human.maxHP = human.MaxHP;
                human.health = human.MaxHP;
                human.sp++;

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                ConsoleTools.CenterConsole(25, 3);
                ConsoleTools.Draw(5, 0,"You leveled up!");
                ConsoleTools.Draw(1, 1, "You are now level " + human.level + "!");
                ConsoleTools.Pause();
            }
            if (human.level == Constants.IntValue("playerLevelMax") && human.exp > 0)
                human.exp = 0;

            return human;
        }

        # endregion experience

        # region quests

        /// <summary>
        /// Adds the provided quest to the quest list
        /// </summary>
        /// <param name="quest">gained quest</param>
        public void GetQuest(QuestItem quest)
        {
            questItems.Add(quest);
        }

        /// <summary>
        /// Rewards the player for the given quest
        /// </summary>
        /// <param name="quest">completed quest</param>
        public void Reward(QuestItem quest)
        {
            money += quest.Reward;
            questItems.Remove(quest);
        }

        # endregion quests

        # region JSON

        private static FieldInfo[] fields = GetAllFields();

        private static FieldInfo[] GetAllFields()
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            FieldInfo[] humanInfo = typeof(Human).GetFields(flags);
            FieldInfo[] actorInfo = typeof(Actor).GetFields(flags);
            FieldInfo[] allInfo = new FieldInfo[humanInfo.Length + actorInfo.Length];
            humanInfo.CopyTo(allInfo, 0);
            actorInfo.CopyTo(allInfo, humanInfo.Length);
            return allInfo;
        }

        public string JsonEncoding
        {
            get
            {
                return JSONParser.Serialize(this, fields);
            }
            set 
            {
                this.seeds.Clear();
                JSONParser parser = new JSONParser(value);
                while (!parser.Done)
                {
                    string[] data = parser.GetNextElement();
                    foreach (FieldInfo f in fields)
                    {
                        if (data[0].Equals(f.Name))
                        {
                            if (f.GetValue(this) is int)
                                f.SetValue(this, int.Parse(data[1]));
                            else if (f.GetValue(this) is char)
                                f.SetValue(this, data[1][0]);
                            else if (f.Name.Equals("seeds"))
                            {
                                string[] seeds = JSONParser.ParseList(data[1]);
                                foreach (string s in seeds)
                                    this.seeds.Add(int.Parse(s));
                            }
                            else if (f.Name.Equals("inventory"))
                            {
                                string[] contents = JSONParser.ParseList(data[1]);
                                foreach (string s in contents)
                                {
                                    string[] details = s.Split(':');
                                    inventory[int.Parse(details[0])].Add(int.Parse(details[1]));
                                }
                            }
                            else if (f.Name.Equals("questItems"))
                            {
                                string[] contents = JSONParser.ParseList(data[1]);
                                foreach (string s in contents)
                                {
                                    string[] details = s.Split(':');
                                    try
                                    {
                                        questItems.Add(new QuestItem(int.Parse(details[0]), int.Parse(details[1])));
                                    }
                                    catch (Exception) { } // No Quest Items
                                }
                            }
                        }
                    }
                }
            }
        }

        # endregion JSON
    }
}
