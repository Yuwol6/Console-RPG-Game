using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Characters
{
    /// <summary>
    /// Represents an allied sidekick character.
    /// </summary>
    public class Legolas : Character
    {
        private Side side;
        private Random random = new Random();

        public Legolas(string name) : base(name)
        {
            side = Side.Friendly;
            attackName = "PUNCH";
            maxHP = 15;
            HP = 15;
        }
        public override void Attack(Character target)
        {
            base.Attack(target);
            Console.WriteLine($"{attackName} dealt 1 damage to {target}.");
            target.GetHit(1);
        }

        /// <summary>
        /// Performs a character-specific attack that has a chance to miss and deals 3 damage on a hit.
        /// </summary>
        /// <param name="target">The character being attacked.</param>
        public void QuickShot(Character target)
        {
            int chance = random.Next(11);
            bool hit = false;
            if (chance > 4) hit = true;
            Console.WriteLine($"{name} used QUICKSHOT on {target}.");
            if (hit)
            {
                Console.WriteLine($"QUICKSHOT dealt 3 damage to {target}.");
                target.GetHit(3);
            }
            else Console.WriteLine($"QUICKSHOT missed!");
        }

        public override Side Side => side;
    }
}
