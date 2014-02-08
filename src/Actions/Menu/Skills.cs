using System;

using MastermindRPG.GUI.Menus;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Action for the skill menu
    /// 
    /// Displays and handles the skill menu
    /// </summary>
    class Skills
    {
        /// <summary>
        /// The costs (in stat points) of each skill
        /// </summary>
        private static readonly int[] costs = { 2, 3, 3, 5, 6, 7, 15, 18, 20 };
        
        /// <summary>
        /// Opens the skill menu
        /// </summary>
        public static void Action()
        {
            SkillMenu menu = new SkillMenu();
            string result = "";

            do
            {
                // If an input did not exit the loop,
                // execute it.
                if (result.Length != 0)
                {
                    int id = int.Parse(result);
                    Adventure.Human.UnlockSkill(id, costs[id]);
                }

                result = menu.Show();
            }
            while (!result.Equals(""));
        }
    }
}
