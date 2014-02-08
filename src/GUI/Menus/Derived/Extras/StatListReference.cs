using System;

namespace MastermindRPG.GUI.Menus.Extras
{
    /// <summary>
    /// Label Type: Stat List Reference
    /// 
    /// Handles pointing to a list of player
    /// stats that lines up with the choices
    /// in the menu
    /// </summary>
    class StatListReference : Label
    {
        /// <summary>
        /// The list of player stats
        /// </summary>
        private object[] stat;

        /// <summary>
        /// Returns the corresponding stat value
        /// </summary>
        /// <param name="x">the choice</param>
        /// <returns>corresponding stat value</returns>
        public override string this[int x]
        {
            get
            {
                if (stat[x] is Boolean)
                    return (Boolean)stat[x] ? "Yes" : "No ";
                else
                    return stat[x].ToString();
            }
        }

        /// <summary>
        /// StatListReference Constructor
        /// </summary>
        /// <param name="hPos">horizontal coordinate</param>
        /// <param name="vPos">vertical coordinate</param>
        /// <param name="stats">player's stat list</param>
        public StatListReference(int hPos, int vPos, params object[] stats)
        {
            this.stat = stats;
            this.horizontalPosition = hPos;
            this.verticalPosition = vPos;
        }
    }
}
