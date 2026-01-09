using System;
using ConsoleRpg.Gears;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Characters
{
    /// <summary>
    /// Base class for all characters in the game.
    /// </summary>
    public abstract class Character
    {
        protected string name;
        protected int maxHP;
        protected int HP;
        protected string attackName;
        protected Gear activeGear = null;
        public abstract Side Side { get; }
        public bool markedForRemoval { get; protected set; } = false;

        public Character(string name)
        {
            this.name = name.ToUpper();
        }

        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Performs no actions for this character's turn.
        /// </summary>
        public void DoNothing()
        {
            Console.WriteLine($"{name} did NOTHING.");
        }

        /// <summary>
        /// Performs the basic attack for the character on the target.
        /// </summary>
        /// <param name="target">The character being attacked.</param>
        public virtual void Attack(Character target)
        {
            Console.WriteLine($"{name} used {attackName} on {target}.");
        }

        /// <summary>
        /// Performs a gear attack for the character, using the equipped gear to attack the target.
        /// </summary>
        /// <param name="target">The character being attacked.</param>
        /// <remarks>The character must have a gear equipped.</remarks>
        public virtual void GearAttack(Character target)
        {
            {
                if (!HasGearOn()) Console.WriteLine($"{name} has no equipped gear. {name} will do nothing.");
                else
                {
                    Console.WriteLine($"{name} used {activeGear.AttackName} on {target}.");
                    Console.WriteLine($"{activeGear.AttackName} dealt {activeGear.Damage} damage to {target}");
                    target.GetHit(activeGear.Damage);
                }
            }
        }

        /// <summary>
        /// Inflicts damage on the character's HP.
        /// </summary>
        /// <param name="damage">The number of damage inflicted on the character. Must be non-negative</param>
        /// <remarks>
        /// If the character's HP reduces to 0, the character is defeated and marked for removal.
        /// HP will not drop below 0.
        /// </remarks>
        public void GetHit(int damage)
        {
            if (HP - damage <= 0)
            {
                HP = 0;
                Console.WriteLine($"{name}'s HP is at 0. {name} has been defeated!");
                markedForRemoval = true;
            }
            else
            {
                HP -= damage;
                Console.WriteLine($"{name} is now at {HP}/{maxHP} HP.");
            }
        }

        /// <summary>
        /// The character drinks a potion, increasing its HP by 10.
        /// </summary>
        /// <remarks>HP will not exceed character's maximum HP.</remarks>
        public void DrinkPotion()
        {
            HP += 10;
            if (HP > maxHP) HP = maxHP;
            Console.WriteLine($"After drinking the potion, {name} now has {HP}/{maxHP} HP.");
        }

        /// <summary>
        /// Returns the character's current status, including its name, HP, maximum HP and equipped Gear (if any).
        /// </summary>
        /// <returns>A formatted string representing the character's current status.</returns>
        public string GetStatus()
        {
            if (HasGearOn())
                return $"{name}  {HP}/{maxHP}  {activeGear}";
            return $"{name}  {HP}/{maxHP}";
        }

        /// <summary>
        /// Determines whether the character's current HP is below half of its maximum HP.
        /// </summary>
        /// <returns>True if the character's current HP is below half of its maximum HP; otherwise, false.</returns>
        public bool IsUnderHalfHP()
        {
            return HP < maxHP/2;
        }

        /// <summary>
        /// Gets the name of the character's basic attack.
        /// </summary>
        /// <returns>The name of the attack.</returns>
        public string GetAttackName()
        {
            return attackName;
        }

        /// <summary>
        /// Determines whether the character has gear equipped.
        /// </summary>
        /// <returns>True if the character has gear equipped; otherwise, false.</returns>
        public bool HasGearOn()
        {
            if (activeGear != null) return true;
            return false;
        }

        /// <summary>
        /// Gets the character's equipped gear.
        /// </summary>
        /// <returns>The equipped gear.</returns>
        public Gear GetGear()
        {
            return activeGear;
        }

        /// <summary>
        /// Equips the specified gear on the character.
        /// </summary>
        /// <param name="gear">The gear to equip.</param>
        public void EquipGear(Gear gear)
        {
            activeGear = gear;
        }

        /// <summary>
        /// Determines whether the character is dead.
        /// </summary>
        /// <returns>True if the character is dead; otherwise, false.</returns>
        public bool IsDead()
        {
            if (HP == 0) return true;
            return false;
        }
    }
}
