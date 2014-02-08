using System;
using System.Threading;

using MastermindRPG.AI;
using MastermindRPG.Creatures;
using MastermindRPG.Data;
using MastermindRPG.Data.IO;
using MastermindRPG.GUI;
using MastermindRPG.Threads;
using MastermindRPG.World;

namespace MastermindRPG
{
    /// <summary>
    /// Handles the player moving around in the world
    /// </summary>
    class Adventure
    {
        # region constants

        /// <summary>
        /// Direction constants
        /// </summary>
        private static readonly int[] up = { 0, -1 };
        private static readonly int[] down = { 0, 1 };
        private static readonly int[] left = { -1, 0 };
        private static readonly int[] right = { 1, 0 };

        # endregion constants

        # region fields

        /// <summary>
        /// Reference to the human object for
        /// action classes
        /// </summary>
        private static Human humanRef;

        # endregion fields

        # region properties

        /// <summary>
        /// Returns the human reference as
        /// a readonly object
        /// </summary>
        public static Human Human
        {
            get { return humanRef; }
        }

        # endregion properties

        # region methods

        /// <summary>
        /// Begins the adventure
        /// </summary>
        /// <param name="human">human pbject</param>
        /// <param name="map">map object</param>
        public static void Begin(Human human, Map map)
        {
            humanRef = human;

            ConsoleKey key;
            int[] direction;

            // Initializes the map and then draws it
            Console.Clear();
            Console.WriteLine("Generating map...");
            map.Generate();
            map.Draw();

            int cycles = 0;

            do
            {
                Thread.Sleep(25);
                cycles = (cycles + 1) % 15;
                key = KeyInput.Key;

                if (key == ConsoleKey.UpArrow)
                    direction = up;
                else if (key == ConsoleKey.RightArrow)
                    direction = right;
                else if (key == ConsoleKey.LeftArrow)
                    direction = left;
                else if (key == ConsoleKey.DownArrow)
                    direction = down;
                else if (key == ConsoleKey.E && KeyInput.Modifier == ConsoleModifiers.Control)
                {
                    Constants.Edit();
                    map.Draw();
                    continue;
                }
                else
                {
                    if (key == ConsoleKey.M)
                    {
                        if (MastermindRPG.Actions.Action.Execute("Menu").ToString().Equals("Save"))
                            break;
                    }
                    else if (key == ConsoleKey.H)
                        MastermindRPG.Actions.Action.Execute("AdventureHelp");
                    else if (key == ConsoleKey.I)
                    {
                        MastermindRPG.Actions.Action.Execute("Inventory");
                        if (map.Id != human.area)
                        {
                            map = WorldTransition.CreateMap(human.area, human);
                            map.Generate();
                        }
                    }
                    else
                    {
                        if (cycles == 0)
                            map.Tick();
                        continue;
                    }
                    map.Draw();
                    continue;
                }

                if (cycles == 0)
                    map.Tick();

                // Move the player
                try
                {
                    bool? check = map.WalkableTile(human.Position[0] + direction[0], human.Position[1] + direction[1]);
                    if (check == null)
                        map.ChangeRoom(direction);
                    else if (check == true)
                    {
                        human.Move(direction);
                        map.MoveAction();
                    }
                }

                // If the player went out of the bounds of the room,
                // change to the next room
                catch (Exception e)
                {
                    Console.SetBufferSize(Console.LargestWindowWidth, 300);
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Console.ReadKey();
                    map.ChangeRoom(direction);
                }
                
                // If the map was ever changed, load the new map
                if (map.Id != human.area)
                {
                    map = WorldTransition.CreateMap(human.area, human);
                    map.Generate();
                    map.Draw();
                }

                // Refresh the display
                map.Refresh();
            }
            while (key != ConsoleKey.Escape);

            // Save the game upon exiting (Quicksave)
            Save.SaveGame(human, human.SaveFile);
        }

        # endregion methods
    }
}
