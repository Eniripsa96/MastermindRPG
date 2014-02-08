using System;
using System.Reflection;

using MastermindRPG.Data.Structures.List;
using MastermindRPG.World;

namespace MastermindRPG.Creatures
{
    /// <summary>
    /// Actor base class
    /// 
    /// Derived Classes:
    ///     Human
    ///     Enemy
    ///         Minion
    ///         Boss
    /// </summary>
    abstract class Actor
    {
        /// <summary>
        /// Dynamic invokation data
        /// </summary>
        private static readonly string[] skillNames = 
            { "PlaceToken", "Betray", "Sacrifice", "DoubleStrike", "Negotiate", "Heal", "Rewind", "Void", "Offering", "Dominate" };
        private static readonly string skillPrefix = "MastermindRPG.Creatures.Skills.";

        /// <summary>
        /// The actor's health
        /// </summary>
        protected int health;

        /// <summary>
        /// The actor's level
        /// </summary>
        protected int level;

        /// <summary>
        /// The actor's experience
        /// </summary>
        protected int exp;

        /// <summary>
        /// The actor's max HP
        /// </summary>
        protected int maxHP;

        /// <summary>
        /// The actor's horizontal position in a map
        /// </summary>
        protected int horizontalPosition;

        /// <summary>
        /// The actor's vertical position in a map
        /// </summary>
        protected int verticalPosition;

        /// <summary>
        /// The list of the actor's skills
        /// </summary>
        protected SimpleList<Boolean> skills;

        /// <summary>
        /// Skills property
        /// </summary>
        public SimpleList<Boolean> Skills
        {
            get { return skills; }
        }

        /// <summary>
        /// Returns enemy health
        /// </summary>
        public int Health
        {
            get { return health; }
        }

        /// <summary>
        /// Returns enemy level
        /// </summary>
        public int Level
        {
            get { return level; }
        }

        /// <summary>
        /// Returns and sets the vertical and horizontal positions
        /// </summary>
        public int[] Position
        {
            get { return new int[] { horizontalPosition, verticalPosition }; }
            set { horizontalPosition = value[0]; verticalPosition = value[1]; }
        }

        /// <summary>
        /// Returns true if the player is damaged
        /// </summary>
        public Boolean IsDamaged
        {
            get { return health < maxHP; }
        }

        /// <summary>
        /// Make sure the player doesn't exceed maximum hp
        /// </summary>
        public void LimitHp()
        {
            if (health > maxHP)
                health = maxHP;
        }

        /// <summary>
        /// Damages the actor
        /// </summary>
        /// <param name="damage">Damage being dealt</param>
        public void Damage(int damage)
        {
            this.health -= damage;
        }

        /// <summary>
        /// Heal the player to full
        /// </summary>
        public void Heal()
        {
            health = maxHP;
        }

        /// <summary>
        /// Moves the actor in the map
        /// </summary>
        /// <param name="direction">direction in which to be moved</param>
        public void Move(int[] direction)
        {
            horizontalPosition += direction[0];
            verticalPosition += direction[1];
        }

        /// <summary>
        /// Uses the skill with the given id
        /// </summary>
        /// <param name="id">the skill id</param>
        /// <returns>true if the cast was successful</returns>
        public Boolean UseSkill(int id)
        {
            Type type = Type.GetType(skillPrefix + skillNames[id]);
            return (Boolean)type.InvokeMember("Perform", BindingFlags.InvokeMethod, null, null, null);
        }
    }
}
