using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Characters
{
    /// <summary>
    /// Represents the boss character of the enemy party. The game ends if this character is defeated.
    /// </summary>
    public class Satan : Character
    {
        private Side side;
        private Random random = new Random();
        public Satan(string name) : base(name)
        {
            side = Side.Enemy;
            attackName = "UNRAVELING ATTACK";
            maxHP = 36;
            HP = 36;
        }

        public override void Attack(Character target)
        {
            int damage = random.Next(3);
            base.Attack(target);
            Console.WriteLine($"{attackName} dealt {damage} damage to {target}.");
            target.GetHit(damage);
        }

        public override Side Side => side;
    }
}
