using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MastermindRPG.GUI.Menus.Extras
{
    /// <summary>
    /// Label Type: DynamicLabel
    /// 
    /// Provides a label that points to the
    /// current selection's details
    /// </summary>
    class DynamicLabel<T> : Label
    {
        /// <summary>
        /// The information list
        /// </summary>
        private T[] text;

        /// <summary>
        /// Returns the current information
        /// </summary>
        /// <param name="x">the choice</param>
        /// <returns>the choice's data</returns>
        public override string this[int x]
        {
            get { return text[x].ToString(); }
        }

        /// <summary>
        /// DynamicLabel Constructor
        /// </summary>
        /// <param name="text">label's text data</param>
        /// <param name="hPos">horizontal position</param>
        /// <param name="vPos">vertical position</param>
        public DynamicLabel(T[] text, int hPos, int vPos)
        {
            this.text = text;
            this.horizontalPosition = hPos;
            this.verticalPosition = vPos;
        }
    }
}
