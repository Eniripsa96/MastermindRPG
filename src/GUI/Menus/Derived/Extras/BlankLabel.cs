using System;

namespace MastermindRPG.GUI.Menus.Extras
{
    class BlankLabel : Label
    {
        /// <summary>
        /// The space string for clearing other labels
        /// </summary>
        private string text;

        /// <summary>
        /// Returns the current information
        /// </summary>
        /// <param name="x">the choice</param>
        /// <returns>the choice's data</returns>
        public override string this[int x]
        {
            get { return text; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">spaces</param>
        /// <param name="hPos">horizontal position</param>
        /// <param name="vPos">vertical position</param>
        public BlankLabel(string text, int hPos, int vPos)
        {
            this.text = text;
            this.horizontalPosition = hPos;
            this.verticalPosition = vPos;
        }
    }
}
