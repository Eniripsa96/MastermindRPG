using System;
using System.IO;
using System.Reflection;
using System.Threading;

using MastermindRPG.Data.Structures.List;
using MastermindRPG.Properties;
using MastermindRPG.Threads;

namespace MastermindRPG.Data
{
    static class Constants
    {
        # region fields

        /// <summary>
        /// List of Boolean constants
        /// </summary>
        private static PairList<string, Boolean> booleanList;

        /// <summary>
        /// List of char constants
        /// </summary>
        private static PairList<string, char> charList;

        /// <summary>
        /// List of ConsoleColor constants
        /// </summary>
        private static PairList<string, ConsoleColor> colorList;

        /// <summary>
        /// List of int constants
        /// </summary>
        private static PairList<string, int> intList;

        /// <summary>
        /// List of string constants
        /// </summary>
        private static PairList<string, string> stringList;

        /// <summary>
        /// Index of Boolean constants
        /// </summary>
        private static int booleanIndex;

        /// <summary>
        /// Index of char constants
        /// </summary>
        private static int charIndex;

        /// <summary>
        /// Index of ConsoleColor constants
        /// </summary>
        private static int colorIndex;

        /// <summary>
        /// Index of int constants
        /// </summary>
        private static int intIndex;

        # endregion fields

        # region IO

        /// <summary>
        /// Load the constants
        /// Initially loads the constants but writes over
        /// them with values found in an external file
        /// </summary>
        public static void Load()
        {   
            // Initialize variables
            booleanList = new PairList<string, bool>();
            charList = new PairList<string, char>();
            colorList = new PairList<string, ConsoleColor>();
            intList = new PairList<string, int>();
            stringList = new PairList<string, string>();
            LoadDefaults();
            LoadExternalFile();
            booleanIndex = booleanList.Size;
            charIndex = booleanIndex + charList.Size;
            colorIndex = charIndex + colorList.Size;
            intIndex = colorIndex + intList.Size;
        }

        /// <summary>
        /// Loads the external constants if available
        /// </summary>
        private static void LoadExternalFile()
        {
            StreamReader read = null;
            try
            {
                read = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/MastermindRPG/constants.txt");
                JSONParser parser = new JSONParser(read.ReadToEnd());
                string type = "";
                while (!parser.Done)
                {
                    string[] details = parser.GetNextElement();
                    if (details[0].Equals("ConstantType"))
                        type = details[1];
                    else if (type.Equals("Boolean"))
                        booleanList[details[0]] = Boolean.Parse(details[1]);
                    else if (type.Equals("Character"))
                        charList[details[0]] = details[1][0];
                    else if (type.Equals("Color"))
                        colorList[details[0]] = ParseColor(details[1], colorList[details[0]]);
                    else if (type.Equals("Integer"))
                        intList[details[0]] = int.Parse(details[1]);
                    else if (type.Equals("String"))
                        stringList[details[0]] = details[1];
                }
            }
            catch (Exception) { }
            if (read != null)
                read.Close();
        }

        /// <summary>
        /// Loads the default constants
        /// </summary>
        private static void LoadDefaults()
        {
            string type = "";
            JSONParser parser = new JSONParser(Resources.ConstantDefaults);
            while (!parser.Done)
            {
                string[] details = parser.GetNextElement();
                if (details[0].Equals("ConstantType"))
                    type = details[1];
                else if (type.Equals("Boolean"))
                    booleanList.Add(details[0], Boolean.Parse(details[1]));
                else if (type.Equals("Character"))
                    charList.Add(details[0], details[1][0]);
                else if (type.Equals("Color"))
                    colorList.Add(details[0], ParseColor(details[1], ConsoleColor.White));
                else if (type.Equals("Integer"))
                    intList.Add(details[0], int.Parse(details[1]));
                else if (type.Equals("String"))
                    stringList.Add(details[0], details[1]);
            }
        }

        /// <summary>
        /// Save the modified constant values
        /// </summary>
        private static void Save()
        {
            try
            {
                StreamWriter save = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/MastermindRPG/constants.txt");
                save.WriteLine("{ConstantType:Boolean}");
                WriteList<string, Boolean>(booleanList, save);
                save.WriteLine("{ConstantType:Character}");
                WriteList<string, char>(charList, save);
                save.WriteLine("{ConstantType:Color}");
                WriteList<string, ConsoleColor>(colorList, save);
                save.WriteLine("{ConstantType:Integer}");
                WriteList<string, int>(intList, save);
                save.WriteLine("{ConstantType:String}");
                WriteList<string, string>(stringList, save);
                save.Close();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Writes the list of constants to the given output stream
        /// </summary>
        /// <typeparam name="T">Key type</typeparam>
        /// <typeparam name="O">Value type</typeparam>
        /// <param name="list">list of pairs</param>
        /// <param name="stream">output stream</param>
        private static void WriteList<T, O>(PairList<T, O> list, StreamWriter stream)
        {
            stream.WriteLine("{");
            Boolean first = true;
            foreach (PairListNode<T, O> node in list)
            {
                if (!first)
                    stream.WriteLine(",");
                first = false;
                stream.Write(node.Key + ":" + node.Value);
            }
            stream.WriteLine();
            stream.WriteLine("}");
            stream.WriteLine();
        }

        # endregion IO

        /// <summary>
        /// Allows the editing of constant values
        /// </summary>
        public static void Edit()
        {
            int choice = 0;
            ConsoleKey key;
            Draw();
            do
            {
                Console.SetCursorPosition(1, choice);
                Console.Write('>');
                Thread.Sleep(10);
                key = KeyInput.Key;
                Console.SetCursorPosition(1, choice);
                Console.Write(' ');

                if (key == ConsoleKey.UpArrow && choice > 0)
                    choice--;
                else if (key == ConsoleKey.DownArrow && choice < Console.BufferHeight - 1)
                    choice++;

                // Edit a value
                else if (key == ConsoleKey.Spacebar)
                {
                    // Get input
                    Console.Clear();
                    Console.WriteLine("Enter new value: ");
                    string s = Console.ReadLine();
                    // Assign the value according to the field type
                    try
                    {
                        if (choice < booleanIndex)
                            booleanList[choice] = Boolean.Parse(s);
                        else if (choice < charIndex)
                            charList[choice - booleanIndex] = s[0];
                        else if (choice < colorIndex)
                            colorList[choice - charIndex] = ParseColor(s, colorList[choice - charIndex]);
                        else if (choice < intIndex)
                            intList[choice - colorIndex] = int.Parse(s);
                        else
                            stringList[choice - intIndex] = s;
                    }
                    // Do nothing with improper input
                    catch (Exception) { }

                    Draw();
                    continue;
                }
                else if (key == ConsoleKey.D)
                {
                    booleanList.Clear();
                    charList.Clear();
                    colorList.Clear();
                    intList.Clear();
                    stringList.Clear();
                    LoadDefaults();
                    Draw();
                }

                else
                    continue;
            }
            while (key != ConsoleKey.Escape);

            // Save the values after editing
            Save();
        }

        /// <summary>
        /// Draws the list of fields for editing
        /// </summary>
        private static void Draw()
        {
            Console.SetBufferSize(Console.LargestWindowWidth, booleanList.Size + charList.Size + colorList.Size + intList.Size + stringList.Size + 1);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Space = Edit, Esc = Back");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (PairListNode<string, Boolean> pair in booleanList)
                Console.WriteLine("   " + pair.Key + ": " + pair.Value);
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (PairListNode<string, char> pair in charList)
                Console.WriteLine("   " + pair.Key + ": " + pair.Value);
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (PairListNode<string, ConsoleColor> pair in colorList)
                Console.WriteLine("   " + pair.Key + ": " + pair.Value);
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (PairListNode<string, int> pair in intList)
                Console.WriteLine("   " + pair.Key + ": " + pair.Value);
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (PairListNode<string, string> pair in stringList)
                Console.WriteLine("   " + pair.Key + ": " + pair.Value);
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// List of colors available
        /// </summary>
        private static readonly ConsoleColor[] colors = { ConsoleColor.Black, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkBlue, ConsoleColor.DarkCyan, ConsoleColor.DarkGray, ConsoleColor.DarkGreen, ConsoleColor.DarkMagenta, ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.Gray, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Yellow };

        /// <summary>
        /// Parses the string into a ConsoleColor
        /// </summary>
        /// <param name="s">string resembling a color</param>
        /// <returns>ConsoleColor</returns>
        private static ConsoleColor ParseColor(string s, object original)
        {
            foreach (ConsoleColor color in colors)
                if (color.ToString().Equals(s))
                    return color;
            return ParseColor(original.ToString(), ConsoleColor.White);
        }

        # region accessors

        /// <summary>
        /// Returns the Boolean constant with the given key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        public static Boolean BooleanValue(string key)
        {
            return booleanList[key];
        }

        /// <summary>
        /// Returns the char constant with the given key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        public static char CharValue(string key)
        {
            return charList[key];
        }

        /// <summary>
        /// Returns the ConsoleColor constant with the given key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        public static ConsoleColor ColorValue(string key)
        {
            return colorList[key];
        }

        /// <summary>
        /// Returns the int constant with the given key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        public static int IntValue(string key)
        {
            return intList[key];
        }

        /// <summary>
        /// Returns the string constant with the given key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        public static string StringValue(string key)
        {
            return stringList[key];
        }

        # endregion accessors
    }
}