using System;
using System.Reflection;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Executes an action from a menu dynamically
    /// </summary>
    static class Action
    {
        private static readonly string prefix = "MastermindRPG.Actions.";

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="action">the type the action points to</param>
        public static object Execute(string action)
        {
            Type type = Type.GetType(prefix + action);
            return type.InvokeMember("Action", BindingFlags.InvokeMethod, null, null, null);
        }
    }
}
