using System;

namespace MastermindRPG.GUI.Menus.Extras
{
    /// <summary>
    /// Label Type: Stat Reference
    /// 
    /// Handles display a player's stat at 
    /// the given location
    /// </summary>
    class StatReference : Label
    {
        /// <summary>
        /// The player's stat
        /// </summary>
        private int stat;
        
        /// <summary>
        /// Returns the player's stat
        /// </summary>
        /// <param name="x">the choice</param>
        /// <returns>player's stat</returns>
        public override string this[int x]
        {
            get { return stat + ""; }
        }

        /// <summary>
        /// StatReference Constructor
        /// </summary>
        /// <param name="stat">the player's stat</param>
        /// <param name="hPos">horizontal position</param>
        /// <param name="vPos">vertical position</param>
        public StatReference(int stat, int hPos, int vPos)
        {
            this.stat = stat;
            this.horizontalPosition = hPos;
            this.verticalPosition = vPos;
        }
    }
}
