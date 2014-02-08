using System;

using MastermindRPG.Creatures;
using MastermindRPG.Data;
using MastermindRPG.GUI;
using MastermindRPG.World.Templates;

namespace MastermindRPG.World
{
    /// <summary>
    /// Map Type: Town
    /// 
    /// The map for the town that
    /// is visite between dungeons.
    /// The size is always 33x13 for
    /// the one and only room. 
    /// Buildings are placed randomly
    /// in one of 6 spots to include
    /// a shop, inn, and 4 NPC houses.
    /// 
    /// Quests are available at each
    /// of the NPC houses
    /// </summary>
    class Town : Map
    {
        # region fields

        /// <summary>
        /// The building orientation
        /// </summary>
        private int[] buildings;

        /// <summary>
        /// The town layout
        /// </summary>
        private Room room;

        /// <summary>
        /// The town's seed
        /// </summary>
        private int seed;

        /// <summary>
        /// The human reference
        /// </summary>
        private Human human;

        # endregion fields

        # region properties

        /// <summary>
        /// Returns the map ID
        /// </summary>
        /// <returns>0 (Town ID)</returns>
        public int Id
        {
            get { return 0; }
        }

        # endregion properties

        # region constructors

        /// <summary>
        /// Town constructor
        /// </summary>
        /// <param name="seed">the seed</param>
        /// <param name="human">the human reference</param>
        public Town(int seed, ref Human human)
        {
            this.seed = seed;
            this.human = human;
        }

        # endregion constructors

        # region map generation

        /// <summary>
        /// Create the town with the seed
        /// </summary>
        public void Generate() 
        {
            // Set the human in the middle of town
            human.Position = new int[] { 16, 6 };

            // Randomize the builings
            Random generation = new Random(seed);
            buildings = new int[6];
            for (int x = 0; x < 6; ++x)
                buildings[x] = 0;

            // Place the shop in the town
            int shop = generation.Next(0, 6);
            buildings[shop] = 2;

            // Place the inn in the town
            // Cannot place it on the shop
            int inn;
            do
            {
                inn = generation.Next(0, 6);
            }
            while (inn == shop);
            buildings[inn] = 1;

            // Get the town layout
            char[,] tiles = TownTemplate.Create(buildings);
            room = new Room(33, 13, tiles);
        }

        # endregion map generation

        # region movement

        /// <summary>
        /// Exits the town and opens the world portal
        /// </summary>
        /// <param name="direction">The direction in which the town was exited (not used)</param>
        public void ChangeRoom(int[] direction) 
        {
            // Open the portal
            int world = Portal.Open(human.progress);

            // If the user didn't press escape to exit the portal,
            // move to the chosen world.
            if (world != 0)
            {
                if (human.Seeds.Size == human.progress + 1)
                    human.GenerateSeed();
                human.MoveToWorld(new int[] { world, 0, 0 });
            }
            else
                Draw();
        }

        /// <summary>
        /// Checks if the player entered a building
        /// </summary>
        public void MoveAction()
        {
            if (human.Position[0] % 10 == 6 && human.Position[1] % 4 == 0 && human.Position[1] % 12 != 0)
            {
                int building = (human.Position[0] / 10) + 3 * (human.Position[1] / 8);

                // Inn
                if (buildings[building] == 2)
                {
                    human.Heal();
                    ConsoleTools.DrawDesign(ConsoleTools.inn);
                    Console.ReadKey();
                }

                // Shop
                else if (buildings[building] == 1)
                    Shop.Open(ref human);

                // NPC house
                else
                {
                    NPC.Talk(building, ref human);
                }

                // Refresh the town display
                Draw();
                Refresh();
            }
        }

        /// <summary>
        /// No need to do anything each tick
        /// </summary>
        public void Tick() { }

        # endregion movement

        # region graphics

        // Draw the town and GUI
        public void Draw() 
        {
            // Draw the GUI Frame and the data inside
            ConsoleTools.DrawDesign(ConsoleTools.mapGui);
            ConsoleTools.Draw(2, 3, human.Level);
            ConsoleTools.Draw(2, 7, human.Exp);
            ConsoleTools.Draw(2, 11, human.Health);
            ConsoleTools.Draw(2, 15, human.SkillPoints);
            ConsoleTools.Draw(45, 3, "Town");
            ConsoleTools.Draw(47, 7, "--");
            for (int x = 0; x < 33; ++x)
                for (int y = 0; y < 13; ++y)
                    ConsoleTools.Draw(x + 9, y + 3, room[x, y]);
            ConsoleTools.Draw(9 + human.Position[0], 3 + human.Position[1], Constants.CharValue("playerToken"), Constants.ColorValue("playerColor"));
            string s = "";
            for (int i = 0; i < 38 * human.Exp / human.RequiredExp; ++i)
                s += '█';
            ConsoleTools.Draw(8, 1, s, Constants.ColorValue("progressBarColor"));
        }
        
        /// <summary>
        /// Refreshes the map directly around the player
        /// </summary>
        public void Refresh()
        { 
            for (int x = human.Position[0] - 1; x <= human.Position[0] + 1; ++x)
                for (int y = human.Position[1] - 1; y <= human.Position[1] + 1; ++y)
                    if (x >= 0 && x < 33 && y >= 0 && y < 13)
                        ConsoleTools.Draw(x + 9, y + 3, room[x, y]);
            ConsoleTools.Draw(9 + human.Position[0], 3 + human.Position[1], Constants.CharValue("playerToken"), ConsoleColor.Green);
        }

        # endregion graphics

        # region map accessors

        /// <summary>
        /// Returns whether or not the target tile is walkable
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        /// <returns>true if the tile can be walked on</returns>
        public Boolean? WalkableTile(int x, int y) 
        {
            try
            {
                if (room[x, y] == ' ' || room[x, y] == ';')
                    return true;
                else
                    return false;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        # endregion map accessors
    }
}
