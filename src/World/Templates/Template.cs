using System;
using System.Linq;
using System.Reflection;

using MastermindRPG.Data.Structures.List;
using MastermindRPG.World.Templates.Types;

namespace MastermindRPG.World.Templates
{
    /// <summary>
    /// Template initialization
    /// 
    /// Uses the given parameters to select and use a template
    /// to generate a room with the given parameters
    /// </summary>
    class Template
    {
        private static readonly string templateTypes = "MastermindRPG.World.Templates.Types";

        /// <summary>
        /// Uses a template according to the given parameters to generate a room
        /// </summary>
        /// <param name="typeInt">A randomed integer that determines the type</param>
        /// <param name="l">The room length</param>
        /// <param name="w">The room width</param>
        /// <returns>The room layout</returns>
        public static char[,] CreateRoom(int typeInt, int l, int w)
        {
            // Get the list of templates
            SimpleList<string> types = new SimpleList<string>();
            var q = from t in Assembly.GetExecutingAssembly().GetTypes() 
                    where t.IsClass && t.Namespace == templateTypes
                    select t; 
            q.ToList().ForEach(t => types.Add(t.Name));

            // Choose the template according to the type given
            int moduloType = typeInt % types.Size;
            
            // Apply the corresponding template
            Type type = Type.GetType(templateTypes + "." + types[moduloType]);
            return (char[,])type.InvokeMember("Create", BindingFlags.InvokeMethod, null, null, new object[] {l, w, typeInt % 2});
        }
    }
}