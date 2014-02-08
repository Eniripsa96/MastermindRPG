using System;
using System.IO;

using MastermindRPG.Creatures;
using MastermindRPG.World;

namespace MastermindRPG.Data.IO
{
    /// <summary>
    /// Saves the game data
    /// </summary>
    static class Save
    {
        private static readonly string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/MastermindRPG/Save";

        /// <summary>
        /// Saves the data to the designated file
        /// </summary>
        /// <param name="human">human data</param>
        /// <param name="map">map data</param>
        /// <param name="saveFile">save file id</param>
        /// <returns>true if the save was successful</returns>
        public static Boolean SaveGame(Human human, int saveFile)
        {
            try
            {
                Directory.CreateDirectory(directory.Replace("/Save", ""));
                // Open the file
                StreamWriter save = new StreamWriter(directory + saveFile);
                save.WriteLine(human.JsonEncoding);

                save.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
