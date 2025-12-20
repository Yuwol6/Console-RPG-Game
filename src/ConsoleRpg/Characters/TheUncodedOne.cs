using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Characters
{
    public class TheUncodedOne : Character
    {
        private Side side;
        private Random random = new Random();
        public TheUncodedOne(string name) : base(name)
        {
            side = Side.Enemy;
            attackName = "UNRAVELING ATTACK";
            maxHP = 15;
            HP = 15;
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
