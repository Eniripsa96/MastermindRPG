using System;

using MastermindRPG.Data;
using MastermindRPG.Data.Structures.List;
using MastermindRPG.Data.Structures.Stack;

namespace MastermindRPG.AI.Pathfinding
{
    /// <summary>
    /// A* Pathfinding Algorithm
    /// By: Steven Sucy
    /// </summary>
    class Pathfinder
    {
        # region constants

        /// <summary>
        /// Maximum number of tries while trying to find a contained point
        /// </summary>
        private static readonly short maximumTries = 10000;

        # endregion constants

        # region fields

        /// <summary>
        /// Directional coordinate offsets
        /// </summary>
        private short[,] directions = new short[,] { { 0, -1 }, { 1, -1 }, { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 } };

        /// <summary>
        /// List of open positions
        /// </summary>
        private SimpleList<Position> open;

        /// <summary>
        /// List of closed positions
        /// </summary>
        private SimpleList<Position> closed;

        /// <summary>
        /// The map traveling in
        /// </summary>
        private _2DMap map;

        /// <summary>
        /// Randomizer for avaiable positions
        /// </summary>
        private Random random;

        /// <summary>
        /// Starting position
        /// </summary>
        private Position start;

        /// <summary>
        /// Target position
        /// </summary>
        private Position target;

        /// <summary>
        /// Position currently checking
        /// </summary>
        private Position check;

        # endregion fields

        # region properties

        /// <summary>
        /// Sets the map, given a proper map
        /// </summary>
        public _2DMap Map
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Map cannot be null");
                else if (value.HorizontalSize <= 0 || value.VerticalSize <= 0)
                    throw new ArgumentOutOfRangeException("Map dimensions cannot be outside the set of natural numbers.");
                map = value;
            }
        }

        # endregion properties

        # region constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public Pathfinder()
        {
            open = new SimpleList<Position>();
            closed = new SimpleList<Position>();
            random = new Random();
            start = new Position();
            target = new Position();
            check = new Position();
        }

        /// <summary>
        /// Constructor with a map
        /// </summary>
        /// <param name="map">_2DMap object</param>
        public Pathfinder(_2DMap map)
            : this()
        {
            this.Map = map;
        }

        # endregion constructors

        # region moves

        /// <summary>
        /// Finds a near-optimal path from the starting location to the target location
        /// </summary>
        /// <param name="hStart">horizontal starting coordinate</param>
        /// <param name="vStart">vertical starting coordinate</param>
        /// <param name="hTarget">horizontal target coordinate</param>
        /// <param name="vTarget">vertical target coordinate</param>
        /// <returns>List of moves</returns>
        public Stack<int[]> GetMoves(int[] startLocation, int[] targetLocation)
        {
            // Initialize the variables
            start.Location = startLocation;
            target.Location = targetLocation;
            return GetMoves();
        }

        /// <summary>
        /// Finds a near-optimal path from the starting location to the target location
        /// </summary>
        /// <returns>List of Moves</returns>
        public Stack<int[]> GetMoves()
        {
            open.Clear();
            closed.Clear();

            Position currentPosition;

            // Add the initial position to the open list
            open.Add(start);

            // Find a path to the destination
            do
            {
                // Locate the current best position in the open list
                currentPosition = null;
                int leastF = -1;
                foreach (Position p in open)
                {
                    int f = Math.Abs(p.X - target.X) + Math.Abs(p.Y - target.Y) + p.G;
                    if (currentPosition == null)
                    {
                        currentPosition = p;
                        leastF = f;
                    }
                    else if (leastF > f)
                    {
                        currentPosition = p;
                        leastF = f;
                    }
                }

                // Move the position to the closed list
                open.Remove(currentPosition);
                closed.Add(currentPosition);

                // Check the surrounding positions

                for (short x = 0; x < 8; ++x)
                {
                    if (x % 2 == 1 && !Constants.BooleanValue("enemyMoveDiagonal"))
                        continue;
                    check.X = currentPosition.X + directions[x, 0];
                    check.Y = currentPosition.Y + directions[x, 1];

                    // Ignore it if it is out of bounds, not traversible, or already in the closed list
                    if (check.X < 0
                        || check.Y < 0
                        || check.X >= map.HorizontalSize
                        || check.Y >= map.VerticalSize)
                        continue;
                    else if (!map.IsWalkable(check.X, check.Y)
                        || !map.IsWalkable(check.X, currentPosition.Y)
                        || !map.IsWalkable(currentPosition.X, check.Y)
                        || closed.Contains(check))
                        continue;

                    // If it is in the open list, check if it was a better path than the current one
                    else if (open.Contains(check))
                    {
                        foreach (Position p in open)
                            if (p.Equals(check))
                                if (p.G < currentPosition.G - 1)
                                    currentPosition.Parent = p;
                    }

                    // Otherwise, add it to the open list
                    else
                        open.Add(new Position(check.X, check.Y, currentPosition));
                }
            }
            // Repeat while the end has not been reached and there are positions left to check
            while (!closed.Contains(target) && !open.Empty());

            // If the end was not found, throw an exception
            if (!closed.Contains(target))
                throw new PathNotFoundException("No path exists between the given points");

            // Connect the target location to the path
            target.Parent = currentPosition;

            // Backtrack while adding the moves to the move stack
            Stack<int[]> moves = new Stack<int[]>();
            Position back = target;
            do
            {
                moves.Push(back.Location);
                back = back.Parent;
            }
            while (!back.Equals(start));

            // Return the stack of moves
            return moves;
        }

        # endregion moves

        # region position

        /// <summary>
        /// Gets a random position contained within the map, using the start point as one point inside the map
        /// </summary>
        /// <param name="hStart">horizontal starting position</param>
        /// <param name="vStart">vertical starting position</param>
        /// <returns>{horizontalPosition, verticalPosition}</returns>
        public int[] GetAvailablePosition(int[] startLocation)
        {
            // Make sure the starting location is valid
            if (startLocation[0] < 0 || startLocation[1] < 0)
                throw new ArgumentOutOfRangeException("Starting coordinates cannot be negative!");
            if (startLocation[0] >= map.HorizontalSize || startLocation[1] >= map.VerticalSize)
                throw new ArgumentOutOfRangeException("Starting coordinates must be within the map!");
            if (!map.IsWalkable(startLocation[0], startLocation[1]))
                throw new ArgumentException("Starting coordinates cannot point to a non-traversible tile!");
            if (map.HorizontalSize == 1 && map.VerticalSize == 1)
                return startLocation;

            short tries = 0;
            start.Location = startLocation;

            // Randomize a position and check to make sure if there is a path to it until a valid one is found
            do
            {
                // Get a location that is not the starting location
                do
                {
                    target.Location[0] = random.Next(0, map.HorizontalSize);
                    target.Location[1] = random.Next(0, map.VerticalSize);
                }
                while (target.Equals(start));

                tries++;

                // Try to get a move set between the locations
                try
                {
                    GetMoves();

                    // Use the location if it worked
                    return target.Location;
                }
                catch (PathNotFoundException) { }

                // If too many tries have occurred, return the starting location
                if (tries == maximumTries)
                {
                    return startLocation;
                }
            }
            while (true);
        }

        # endregion position
    }
}