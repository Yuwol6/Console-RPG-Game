using System;
using ConsoleRpg.Gears;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Characters
{
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

        public void DoNothing()
        {
            Console.WriteLine($"{name} did NOTHING.");
        }

        public virtual void Attack(Character target)
        {
            Console.WriteLine($"{name} used {attackName} on {target}.");
        }

        public virtual void GearAttack(Character target)
        {
            {
                if (!hasGearOn()) Console.WriteLine($"{name} has no equipped gear. {name} will do nothing.");
                else
                {
                    Console.WriteLine($"{name} used {activeGear.AttackName} on {target}.");
                    Console.WriteLine($"{activeGear.AttackName} dealt {activeGear.Damage} damage to {target}");
                    target.GetHit(activeGear.Damage);
                }
            }
        }

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
        public void DrinkPotion()
        {
            HP += 10;
            if (HP > maxHP) HP = maxHP;
            Console.WriteLine($"After drinking the potion, {name} now has {HP}/{maxHP} HP.");
        }

        public string GetStatus()
        {
            if (hasGearOn())
                return $"{name}  {HP}/{maxHP}  {activeGear}";
            return $"{name}  {HP}/{maxHP}";
        }

        public bool IsUnderHalfHP()
        {
            return HP < maxHP/2;
        }

        public string GetAttackName()
        {
            return attackName;
        }

        public bool hasGearOn()
        {
            if (activeGear != null) return true;
            return false;
        }

        public Gear GetGear()
        {
            return activeGear;
        }

        public void EquipGear(Gear gear)
        {
            activeGear = gear;
        }

        public bool IsDead()
        {
            if (HP == 0) return true;
            return false;
        }
    }
}
