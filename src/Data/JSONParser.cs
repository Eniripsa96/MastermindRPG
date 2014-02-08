using System;
using System.Linq;
using System.Reflection;

namespace MastermindRPG.Data
{
    /// <summary>
    /// Parses strings resembling JSON data
    /// </summary>
    class JSONParser
    {
        /// <summary>
        /// char list for splitting elements
        /// </summary>
        private static char[] splitter = { ':' };

        /// <summary>
        /// JSON data
        /// </summary>
        private string data;

        /// <summary>
        /// The number of sets of data that have been read
        /// </summary>
        private int set;

        /// <summary>
        /// Property for the set
        /// </summary>
        public int Set
        {
            get { return set; }
        }

        /// <summary>
        /// Checks if the reading is done
        /// </summary>
        public Boolean Done
        {
            get { return !data.Contains('}'); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">JSON data</param>
        public JSONParser(string data)
        {
            this.data = data;
            set = 0;
        }

        /// <summary>
        /// Gets the next element in the data
        /// </summary>
        /// <returns></returns>
        public string[] GetNextElement()
        {
            string s = "";
            while ((!s.Contains(",") && !s.Contains("}")) || (s.Contains("[") && !s.Contains("],") && !s.Contains("]}")))
            {
                s += data[0];
                data = data.Substring(1);
            }
            if (s.Contains("}"))
                set++;
            s = s.Substring(0, s.Length - 1).Replace("\n", "").Replace("\r", "").Replace("{", "");

            return s.Split(splitter, 2);
        }

        /// <summary>
        /// Parses the string resembling a list into a list of strings
        /// </summary>
        /// <param name="s">string resembling a list</param>
        /// <returns>list elements</returns>
        public static string[] ParseList(string s)
        {
            return s.Replace("[", "").Replace("]", "").Split(',');
        }

        /// <summary>
        /// Serializes the object into JSON given its field information
        /// </summary>
        /// <param name="parent">object to serialize</param>
        /// <param name="fields">object field data</param>
        /// <returns>object JSON serialization</returns>
        public static string Serialize(object parent, params FieldInfo[] fields)
        {
            string s = "{";
            foreach (FieldInfo f in fields)
                s += f.Name + ":" + f.GetValue(parent) + ",";
            return s.Substring(0, s.Length - 1) + "}";
        }
    }
}
