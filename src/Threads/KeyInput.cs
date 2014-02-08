using System;

namespace MastermindRPG.Threads
{
    /// <summary>
    /// Handles key input via threading
    /// </summary>
    class KeyInput
    {
        /// <summary>
        /// The pressed key
        /// </summary>
        private static ConsoleKey key;

        /// <summary>
        /// Modifier of the key
        /// </summary>
        private static ConsoleKeyInfo info;

        /// <summary>
        /// Key property
        /// </summary>
        public static ConsoleKey Key
        {
            get { ConsoleKey k = key; key = ConsoleKey.NoName; return k; }
        }

        /// <summary>
        /// Modifier property
        /// </summary>
        public static ConsoleModifiers Modifier
        {
            get { return info.Modifiers; }
        }

        /// <summary>
        /// Thread method that gets input constantly
        /// </summary>
        public static void ThreadMethod()
        {
            key = ConsoleKey.NoName;
            while (true)
            {
                info = Console.ReadKey(true);
                key = info.Key;
            }
        }
    }
}
