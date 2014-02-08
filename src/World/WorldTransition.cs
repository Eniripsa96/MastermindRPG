using System;

using MastermindRPG.Creatures;

namespace MastermindRPG.World
{
    /// <summary>
    /// Transitions the player between maps
    /// </summary>
    static class WorldTransition
    {
        /// <summary>
        /// Type constants
        /// </summary>
        private static string mapPrefix = "MastermindRPG.World.";
        private static string[] mapNames = new string[] { "Town", "Area" };

        /// <summary>
        /// Moves to the map with the given ID
        /// </summary>
        /// <param name="id">map ID</param>
        /// <param name="human">human reference</param>
        /// <returns>the new map</returns>
        public static Map CreateMap(int id, Human human)
        {
            if (id > 0)
                id = 1;

            // Create the map and return it
            Type type = Type.GetType(mapPrefix + mapNames[id]);
            return (Map)Activator.CreateInstance(type, new object[] { human.Seed, human });
        }
    }
}
