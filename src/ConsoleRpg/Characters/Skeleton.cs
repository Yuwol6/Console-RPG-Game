using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Characters
{
    /// <summary>
    /// Represents a character of the enemy party.
    /// </summary>
    public class Skeleton : Character
    {
        private Side side;
        private Random random = new Random();

        public Skeleton(string name) : base(name)
        {
            side = Side.Enemy;
            attackName = "BONE CRUNCH";
            maxHP = 5;
            HP = 5;
        }

        /// <summary>
        /// Performs the basic attack for the skeleton that has a chance to miss and deals 1 damage on a hit.
        /// </summary>
        /// <param name="target">The character being attacked.</param>
        public override void Attack(Character target)
        {
            int damage = random.Next(2);
            base.Attack(target);
            Console.WriteLine($"{attackName} dealt {damage} damage to {target}.");
            target.GetHit(damage);
        }
        public override Side Side => side;
    }
}
