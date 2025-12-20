using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Characters
{
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
