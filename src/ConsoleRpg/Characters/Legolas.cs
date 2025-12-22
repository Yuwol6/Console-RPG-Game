using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Characters
{
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
