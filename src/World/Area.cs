using System;
using System.IO;
using System.Threading;

using MastermindRPG.AI.Pathfinding;
using MastermindRPG.Creatures;
using MastermindRPG.Data;
using MastermindRPG.Data.Structures.List;
using MastermindRPG.GUI;
using MastermindRPG.Items;
using MastermindRPG.World.Templates;

namespace MastermindRPG.World
{
    /// <summary>
    /// Map Type: Area
    /// 
    /// Dungeon maps that have enemy
    /// encounters and bosses at the
    /// end of them. The map size is
    /// determined by the ID of it,
    /// ranging from 1 to 40.
    /// </summary>
    class Area : Map, _2DMap
    {
        # region constants

        /// <summary>
        /// Save file directory (for clearing data upon losing)
        /// </summary>
        private static readonly string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/MastermindRPG/Save";

        # endregion constants

        # region fields

        /// <summary>
        /// Randomizer for the map size and room data
        /// </summary>
        private Random random;

        /// <summary>
        /// Pathfinder for the enemies
        /// </summary>
        private Pathfinder pathing;

        /// <summary>
        /// List of enemies in the map
        /// </summary>
        private SimpleList<Minion> enemyList;

        /// <summary>
        /// Backup of the current room coordinates
        /// </summary>
        int[] backup;

        /// <summary>
        /// The map ID
        /// </summary>
        private int id;

        /// <summary>
        /// The horizontal map size (in rooms)
        /// </summary>
        private int length;

        /// <summary>
        /// The vertical map size (in rooms)
        /// </summary>
        private int width;

        /// <summary>
        /// The current room {horizontal, vertical}
        /// </summary>
        private int[] currentRoom;

        /// <summary>
        /// The seed for the current map
        /// </summary>
        private int seed;

        /// <summary>
        /// The rooms within the area
        /// </summary>
        _2DRoomList rooms;

        /// <summary>
        /// The player reference
        /// </summary>
        Human human;

        # endregion fields

        # region properties

        /// <summary>
        /// Returns the current room location
        /// </summary>
        public int[] RoomCoordinates
        {
            get { return currentRoom; }
        }

        /// <summary>
        /// Returns the current room object
        /// </summary>
        private Room CurrentRoom
        {
            get { return rooms[currentRoom[0], currentRoom[1]]; }
        }

        /// <summary>
        /// Returns the id of the map
        /// </summary>
        /// <returns>the id of the map</returns>
        public int Id
        {
            get { return id; }
        }

        /// <summary>
        /// Returns the horizontal size of the current room
        /// </summary>
        public int HorizontalSize
        {
            get { return CurrentRoom.HorizontalSize; }
        }

        /// <summary>
        /// Returns the vertical size of the current room
        /// </summary>
        public int VerticalSize
        {
            get { return CurrentRoom.VerticalSize; }
        }

        # endregion properties

        # region constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seed">Seed of the area</param>
        /// <param name="human">Human reference</param>
        public Area(int seed, ref Human human)
        {
            this.seed = seed;
            this.human = human;
            currentRoom = new int[] { 0, 0 };
            random = new Random();
            enemyList = new SimpleList<Minion>();
            pathing = new Pathfinder();
            backup = new int[2];
        }

        # endregion constructors

        # region map generation

        /// <summary>
        /// Generates the rooms using the seed
        /// </summary>
        public void Generate()
        {
            this.id = human.area;

            // Set the size (in rooms) for the world
            Console.WriteLine("Choosing sizes...");
            Random generation = new Random(seed);
            length = generation.Next(
                id / Constants.IntValue("mapSizeDivisor") + Constants.IntValue("mapSizeBonusMin"),
                id / Constants.IntValue("mapSizeDivisor") + Constants.IntValue("mapSizeBonusMax"));
            width = generation.Next(
                id / Constants.IntValue("mapSizeDivisor") + Constants.IntValue("mapSizeBonusMin"),
                id / Constants.IntValue("mapSizeDivisor") + Constants.IntValue("mapSizeBonusMax"));
            rooms = new _2DRoomList();
            int finishRoom = generation.Next(0, length);

            int[] roomLengths = new int[length];
            for (int i = 0; i < length; ++i)
                roomLengths[i] = generation.Next(11, 33);

            Console.WriteLine("Generating Rooms...");
            // Create all of the rooms with random templates
            for (int y = 0; y < width; ++y)
            {
                // Randomize the column width
                int roomWidth = generation.Next(7, 13);
                for (int x = 0; x < length; ++x)
                {
                    // Randomize the type
                    int type = generation.Next();

                    // Get the room layout with the new parameters
                    char[,] tiles = Template.CreateRoom(type, roomLengths[x], roomWidth);

                    int halfWidth = roomWidth / 2;
                    int halfLength = roomLengths[x] / 2;
                    // Set the paths between rooms
                    if (x != 0)
                    {
                        tiles[0, halfWidth - 1] = Convert(tiles[0, halfWidth - 1], 4);
                        tiles[0, halfWidth + 1] = Convert(tiles[0, halfWidth + 1], 5);
                        tiles[0, halfWidth] = ' ';
                    }
                    if (y != 0)
                    {
                        tiles[halfLength - 1, 0] = Convert(tiles[halfLength - 1, 0], 0);
                        tiles[halfLength + 1, 0] = Convert(tiles[halfLength + 1, 0], 1);
                        tiles[halfLength, 0] = ' ';
                    }
                    if (x != length - 1)
                    {
                        tiles[roomLengths[x] - 1, halfWidth - 1] = Convert(tiles[roomLengths[x] - 1, halfWidth - 1], 6);
                        tiles[roomLengths[x] - 1, halfWidth + 1] = Convert(tiles[roomLengths[x] - 1, halfWidth + 1], 7);
                        tiles[roomLengths[x] - 1, halfWidth] = ' ';
                    }
                    if (y != width - 1 || x == finishRoom)
                    {
                        tiles[halfLength - 1, roomWidth - 1] = Convert(tiles[halfLength - 1, roomWidth - 1], 2);
                        tiles[halfLength + 1, roomWidth - 1] = Convert(tiles[halfLength + 1, roomWidth - 1], 3);
                        tiles[halfLength, roomWidth - 1] = ' ';
                    }

                    // Add the room to the array
                    rooms.Add(new Room(roomLengths[x], roomWidth, tiles));

                    // Place the human within the first room
                    if (x + y == 0)
                        human.Position = new int[] { 1, roomWidth / 2 };
                }
            }
            pathing.Map = this;
            Console.WriteLine("Arranging rooms...");
            rooms.Arrange(length, width);

            Console.WriteLine("Adding enemies...");
            // Add enemies to the map
            for (int x = 0; x < length; ++x)
                for (int y = 0; y < width; ++y)
                {
                    if (x == 0 && y == 0)
                        continue;
                    currentRoom[0] = x;
                    currentRoom[1] = y;

                    int enemyCount = random.Next(Constants.IntValue("mapEnemyMin"), Constants.IntValue("mapEnemyMax") + 1);
                    for (int i = 0; i < enemyCount; ++i)
                    {
                        Minion enemy = new Minion(id);
                        do
                        {
                            enemy.Position = pathing.GetAvailablePosition(new int[] {1, CurrentRoom.VerticalSize / 2});
                            enemy.Room = new int[] { x, y };
                        }
                        while (enemyList.Contains(enemy));
                        enemyList.Add(enemy);
                    }
                    Console.WriteLine("    Room " + (length * y + x));
                }

            Console.WriteLine("Loading Quest Items...");
            SimpleList<int[]> takenRooms = new SimpleList<int[]>();
            // Place the associated quest items in the map
            foreach (QuestItem item in human.QuestItems)
            {
                if (item.Area == id)
                {
                    Boolean taken;
                    do
                    {
                        taken = false;
                        currentRoom[0] = generation.Next(0, length);
                        currentRoom[1] = generation.Next(0, width);
                        foreach (int[] spot in takenRooms)
                            if (currentRoom[0] == spot[0] && currentRoom[1] == spot[1])
                                taken = true;
                    }
                    while (currentRoom[0] + currentRoom[1] == 0 || taken);
                    takenRooms.Add(new int[] { currentRoom[0], currentRoom[1] });
                    int[] tile = pathing.GetAvailablePosition(new int[] {1, rooms[currentRoom[0], currentRoom[1]].VerticalSize / 2});
                    item.Location = new int[] { currentRoom[0], currentRoom[1], tile[0], tile[1] };
                }
            }

            currentRoom[0] = 0;
            currentRoom[1] = 0;
        }

        /// <summary>
        /// Converts the tiles next to the doors
        /// into connecting pieces for a continuous
        /// wall
        /// </summary>
        /// <param name="c">the adjacent tile</param>
        /// <param name="direction">position of tile</param>
        /// <returns>the converted tile</returns>
        private char Convert(char c, int direction)
        {
            if (direction / 2 == 0)
                if (c == '╔' || c == '╗')
                    return '║';
                else if (c == '═' && direction % 2 == 0)
                    return '╝';
                else if (c == '═' && direction % 2 == 1)
                    return '╚';
            if (direction / 2 == 1)
                if (c == '╝' || c == '╚')
                    return '║';
                else if (c == '═' && direction % 2 == 0)
                    return '╗';
                else if (c == '═' && direction % 2 == 1)
                    return '╔';
            if (direction / 2 == 2)
                if (c == '╔' || c == '╚')
                    return '═';
                else if (c == '║' && direction % 2 == 0)
                    return '╝';
                else if (c == '║' && direction % 2 == 1)
                    return '╗';
            if (direction / 2 == 3)
                if (c == '╗' || c == '╝')
                    return '═';
                else if (c == '║' && direction % 2 == 0)
                    return '╚';
                else if (c == '║' && direction % 2 == 1)
                    return '╔';
            if (c == '╠' || c == '╣')
                if (direction % 2 == 0)
                    return '╩';
                else
                    return '╦';
            return c;
        }

        # endregion mapgeneration

        # region movement

        /// <summary>
        /// Changes between rooms in the area
        /// </summary>
        /// <param name="direction">The direction to move in {horizontal change, vertical change}</param>
        public void ChangeRoom(int[] direction) 
        {
            // Clear the old room
            for (int y = 3; y < 16; ++y)
                ConsoleTools.Draw(9, y, "                                 ");
            ConsoleTools.Draw(25, 9, Constants.CharValue("playerToken"));

            // If the room moving to is outside the bounds of the area, it is a boss room
            if (currentRoom[0] + direction[0] < 0 || currentRoom[0] + direction[0] == length
                || currentRoom[1] + direction[1] < 0 || currentRoom[1] + direction[1] == width)

                // If the human has not beat the boss already, fight it
                if (human.progress < human.area)
                {
                    human.Heal();
                    human.battling = 2;
                    Enemy enemy = new Boss(human.area);
                    int result = Battle.Fight(human, enemy);
                    human.battling = 0;
                    if (result > 0)
                    {
                        human += enemy.Reward;
                        human.progress++;
                        human.MoveToWorld(new int[] { 0, 16, 6 });
                    }
                    else if (result == 0)
                    {
                        try
                        {
                            File.Delete(directory + human.SaveFile);
                        }
                        catch (Exception) { }
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        ConsoleTools.CenterConsole(25, 2);
                        Console.WriteLine("        You Lost!");
                        ConsoleTools.Pause();
                        Environment.Exit(0);
                    }
                }

                // Otherwise move to town
                else
                    human.MoveToWorld(new int[] { 0, 16, 6 });
            
            // If the room is within the area's bounds, move into it and position the player appropriately
            else
            {
                // Change rooms
                currentRoom[0] += direction[0];
                currentRoom[1] += direction[1];

                // Set the new player position
                if (direction[0] == 1)
                    human.Position = new int[] { 0, human.Position[1] };
                if (direction[0] == -1)
                    human.Position = new int[] { CurrentRoom.HorizontalSize - 1, human.Position[1] };
                if (direction[1] == 1)
                    human.Position = new int[] { human.Position[0], 0 };
                if (direction[1] == -1)
                    human.Position = new int[] { human.Position[0], CurrentRoom.VerticalSize - 1 };

                DisplayStats();
            }
        }

        /// <summary>
        /// Checks for an enemy encounter or item pickup between moves
        /// </summary>
        public void MoveAction()
        {
            if (human.dewSteps > 0)
                human.dewSteps--;

            foreach (QuestItem item in human.QuestItems)
            {
                if (item.Location[0] == id
                    && item.Location[1] == currentRoom[0]
                    && item.Location[2] == currentRoom[1]
                    && item.Location[3] == human.Position[0]
                    && item.Location[4] == human.Position[1]
                    && !item.Owned)
                {
                    // Obtain the quest item
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    ConsoleTools.CenterConsole(15 + item.Name.Length, 2);
                    Console.WriteLine(" You found a " + item.Name + "!");
                    ConsoleTools.Pause();
                    Draw();
                    item.Add(1);
                    return;
                }
            }
            foreach (Minion minion in enemyList)
            {
                if (minion.Room[0] != currentRoom[0] || minion.Room[1] != currentRoom[1])
                    continue;

                minion.Moves.Clear();
                if (minion.Position[0] == human.Position[0]
                    && minion.Position[1] == human.Position[1])
                {
                    enemyList.Remove(minion);
                    Encounter(minion);
                    return;
                }
            }
        }

        /// <summary>
        /// Moves the enemy at each tick
        /// </summary>
        public void Tick()
        {
            // Do not move enemies if masking dew is active
            if (human.dewSteps > 0)
                return;

            SimpleList<int[]> occupiedPositions = new SimpleList<int[]>();
            SimpleList<Minion> expired = new SimpleList<Minion>();

            foreach (Minion minion in enemyList)
            {
                // If the enemy is in the same room, move it and check if it reached the player
                if (minion.Room[0] == currentRoom[0] && minion.Room[1] == currentRoom[1])
                {
                    int[] offset = Offset(minion.Room[0], minion.Room[1]);
                    // If the minion doesn't have a list of moves already, give it one
                    if (minion.Moves.Empty())
                        minion.Moves = pathing.GetMoves(new int[] { minion.Position[0] + offset[0], minion.Position[1] + offset[1] }, human.Position);

                    // Get the next move
                    int[] m = minion.Moves.Pop();
                    Boolean occupied = false;

                    // If the spot is taken, backtrack to the original position
                    foreach (int[] spot in occupiedPositions)
                        if (spot[0] == m[0] && spot[1] == m[1])
                        {
                            occupied = true;
                            minion.Moves.Push(m);
                            break;
                        }
                    if (occupied)
                    {
                        occupiedPositions.Add(minion.Position);
                        continue;
                    }

                    // Else, take the position
                    minion.Position = m;
                    occupiedPositions.Add(minion.Position);

                    // If that was the last move, the player has been reached so a battle begins
                    if (minion.Moves.NearEmpty())
                    {
                        expired.Add(minion);
                        Encounter(minion);
                        break;
                    }
                }
                /*
                else
                {
                    if (Math.Abs(minion.Room[0] - currentRoom[0]) > 1 || Math.Abs(minion.Room[1] - currentRoom[1]) > 1)
                        continue;
                    if (minion.Moves.empty())
                    {
                        currentRoom.CopyTo(backup, 0);
                        currentRoom[0] = minion.Room[0];
                        currentRoom[1] = minion.Room[1];
                        int[] target = pathing.GetAvailablePosition(minion.Position);
                        minion.Moves = pathing.GetMoves(minion.Position, target);
                        backup.CopyTo(currentRoom, 0);
                    }
                    minion.Position = minion.Moves.pop();
                }
                  */
            }

            // Remove expired minions (ones that were battled)
            foreach (Minion minion in expired)
                enemyList.Remove(minion);

            // Update the display
            Refresh();
        }

        # endregion movement

        # region graphics

        /// <summary>
        /// Draws the entire GUI including the frame and room
        /// </summary>
        public void Draw() 
        {
            // Draw the Frame
            ConsoleTools.DrawDesign(ConsoleTools.mapGui);

            DisplayStats();
            
            // Draw the room and player
            Refresh();
        }

        /// <summary>
        /// Draws the stats within the GUI
        /// </summary>
        private void DisplayStats()
        {
            ConsoleTools.Draw(2, 3, human.Level);
            ConsoleTools.Draw(2, 7, human.Exp);
            ConsoleTools.Draw(2, 11, human.Health);
            ConsoleTools.Draw(2, 15, human.SkillPoints);
            ConsoleTools.Draw(47, 3, id);
            ConsoleTools.Draw(47, 7, currentRoom[0] + (currentRoom[1] * length) + 1);
            string s = "";
            for (int i = 0; i < 38 * human.Exp / human.RequiredExp; ++i)
                s += '█';
            ConsoleTools.Draw(8, 1, s, Constants.ColorValue("progressBarColor"));
        }

        /// <summary>
        /// Refreshes the map display
        /// </summary>
        public void Refresh() 
        {
            // Draw the map
            string[] buffer = new string[13];
            for (int y = 0; y < 13; ++y)
            {
                int yPos = y - 6 + human.Position[1];
                for (int x = 0; x < 33; ++x)
                    buffer[y] += rooms[currentRoom[0], currentRoom[1], x - 16 + human.Position[0], yPos];
            }
            for (int y = 0; y < 13; ++y)
                ConsoleTools.Draw(9, y + 3, buffer[y]);

            // Draw the enemies
            foreach (Minion enemy in enemyList)
            {
                int[] offset = Offset(enemy.Room[0], enemy.Room[1]);
                int xPos = enemy.Position[0] + 25 - human.Position[0] + offset[0];
                int yPos = enemy.Position[1] + 9 - human.Position[1] + offset[1];
                if (xPos < 9 || xPos > 41 || yPos < 3 || yPos > 15)
                    continue;
                ConsoleTools.Draw(xPos, yPos, Constants.CharValue("enemyToken"), Constants.ColorValue("enemyColor"));
            }

            // Draw the quest items
            foreach (QuestItem item in human.QuestItems)
            {
                if (item.Location[0] == id && !item.Owned)
                {
                    int[] offset = Offset(item.Location[1], item.Location[2]);
                    int xPos = item.Location[3] + 25 - human.Position[0] + offset[0];
                    int yPos = item.Location[4] + 9 - human.Position[1] + offset[1];
                    if (xPos < 9 || xPos > 41 || yPos < 3 || yPos > 15)
                        continue;
                    ConsoleTools.Draw(xPos, yPos, item.Token, ConsoleColor.Blue);
                }
            }

            // Draw the player
            ConsoleTools.Draw(25, 9, Constants.CharValue("playerToken"), Constants.ColorValue("playerColor"));
        }

        # endregion graphics

        # region map accessors

        /// <summary>
        /// Calculates the room offset
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        /// <returns>tile coordinate offset {horizontal, vertical}</returns>
        private int[] Offset(int x, int y)
        {
            int[] offset = new int[2];
            int[] backup = new int[2];
            currentRoom.CopyTo(backup, 0);
            while (x < currentRoom[0])
            {
                currentRoom[0]--;
                offset[0] -= CurrentRoom.HorizontalSize;
            }
            while (x > currentRoom[0])
            {
                offset[0] += CurrentRoom.HorizontalSize;
                currentRoom[0]++;
            }
            while (y < currentRoom[1])
            {
                currentRoom[1]--;
                offset[1] -= CurrentRoom.VerticalSize;
            }
            while (y > currentRoom[1])
            {
                offset[1] += CurrentRoom.VerticalSize;
                currentRoom[1]++;
            }
            backup.CopyTo(currentRoom, 0);
            return offset;
        }

        /// <summary>
        /// Checks if the tile at the given location can be walked on
        /// </summary>
        /// <param name="x">the horizontal coordinate</param>
        /// <param name="y">the vertical coordinate</param>
        /// <returns>true if the tile can be walked on</returns>
        public Boolean? WalkableTile(int x, int y) 
        {
            if (x < 0 || y < 0 || x == CurrentRoom.HorizontalSize || y == CurrentRoom.VerticalSize)
                return null;
            else if (CurrentRoom[x, y] == ' ')
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns if the given tile is walkable
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        /// <returns>true if it is walkable, false otherwise</returns>
        public Boolean IsWalkable(int x, int y)
        {
            if (CurrentRoom[x, y] == ' ')
                return true;
            else
                return false;
        }

        # endregion map accessors

        # region enemy encounter

        /// <summary>
        /// Encounter a minion while exploring
        /// </summary>
        private void Encounter(params Minion[] minion)
        {
            human.battling = 1;
            Enemy enemy;
            if (minion.Length == 0)
                enemy = new Minion(human.area);
            else
                enemy = minion[0];
            int result = Battle.Fight(human, enemy);
            human.battling = 0;
            if (result > 0)
                human += enemy.Reward;
            else if (result == 0)
            {
                try
                {
                    File.Delete(directory + human.SaveFile);
                }
                catch (Exception) { }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                ConsoleTools.CenterConsole(25, 2);
                Console.WriteLine("        You Lost!");
                ConsoleTools.Pause();
                Environment.Exit(0);
            }
            ConsoleTools.DrawDesign(ConsoleTools.mapGui);
            Draw();
            ConsoleTools.Draw(25, 9, Constants.CharValue("playerToken"));
        }

        # endregion enemy encounter
    }
}
