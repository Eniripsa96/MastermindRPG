using System;
using System.IO;

using MastermindRPG.Creatures;
using MastermindRPG.World;

namespace MastermindRPG.Data.IO
{
    /// <summary>
    /// Loads a save file
    /// </summary>
    class Load
    {
        private static readonly string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/MastermindRPG/Save";

        /// <summary>
        /// Loads the file pointed to and stores the data in the supplied human and map
        /// </summary>
        /// <param name="human">human object</param>
        /// <param name="map">map object</param>
        /// <param name="saveFile">save file id</param>
        /// <returns>true if the load was successful</returns>
        public static Boolean LoadGame(out Human human, out Map map, int saveFile)
        {
            StreamReader load = null;

            //try {
                // Opens the file
                load = new StreamReader(directory + saveFile);

                string json = load.ReadLine();
                human = new Human(saveFile);
                human.JsonEncoding = json;

                load.Close();

                // Load the world
                if (human.area == 0)
                    map = new Town(human.Seed, ref human);
                else
                    map = new Area(human.Seed, ref human);
                 
                return true;
        /*    
        }
            catch (Exception e)
            {
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.ReadKey();
                load.Close();
                if (saveFile == -1)
                {
                    // Was no save file
                    int file = FileSelect.Open(true);
                    human = new Human(file);
                    map = new Area(human.Seed, ref human);
                    return false;
                }
                else
                {
                    // Corrupt save file
                    
                    human = new Human(saveFile);
                    map = new Area(human.Seed, ref human);
                    return false;
                }
            }*/
        }
    }
}
