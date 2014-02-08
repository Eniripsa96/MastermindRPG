using System;
using System.Threading;

using MastermindRPG.Data;
using MastermindRPG.GUI;
using MastermindRPG.GUI.Menus;
using MastermindRPG.Threads;

namespace MastermindRPG
{
    /// <summary>
    /// The start of the game
    /// 
    /// Displays the logo and
    /// handles the main menu
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">(unused)</param>
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Mastermind RPG - By Steven Sucy";
            
            // Display the logo upon startup
            Constants.Load();
            ConsoleTools.DrawDesign(ConsoleTools.logo);
            Console.ReadKey();
            
            string choice = "";

            Thread thread = new Thread(() => KeyInput.ThreadMethod());
            thread.Start();

            do
            {
                // If there was an input already that didn't exit the
                // loop, execute it
                if (choice.Length != 0)
                    MastermindRPG.Actions.Action.Execute(choice);

                // Display the main menu
                MainMenu mainMenu = new MainMenu();
                choice = mainMenu.Show();
            }
            while (!choice.Equals("Quit") && !choice.Equals(""));

            Console.Clear();

            thread.Abort();
        }
    }
}
