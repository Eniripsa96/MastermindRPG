using System;

namespace MastermindRPG.GUI.Menus.Extras
{
    /// <summary>
    /// Label base class
    /// 
    /// Provides menus with labels that update seperately from the design
    /// that can display user stats or selection information.
    /// 
    /// Derived Classes:
    ///     DynamicLabel
    ///     StatListReference
    ///     StatReference
    /// </summary>
    abstract class Label
    {
        /// <summary>
        /// Coordinates of the label
        /// </summary>
        protected int horizontalPosition;
        protected int verticalPosition;

        /// <summary>
        /// Returns the label text depending on the current choice
        /// </summary>
        /// <param name="x">the current choice</param>
        /// <returns>the label text</returns>
        public virtual string this[int x]
        {
            get { return null; }
        }

        /// <summary>
        /// Returns the horizontal position
        /// </summary>
        public int X
        {
            get { return horizontalPosition; }
        }

        /// <summary>
        /// Returns the vertical position
        /// </summary>
        public int Y
        {
            get { return verticalPosition; }
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetType().ToString();
        }
    }
}
