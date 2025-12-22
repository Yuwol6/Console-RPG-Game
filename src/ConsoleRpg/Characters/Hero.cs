using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Characters
{
    public class Hero : Character
    {
        private Side side;

        public Hero(string name) : base(name)
        {
            side = Side.Friendly;
            attackName = "PUNCH";
            maxHP = 25;
            HP = 25;
        }
        public override void Attack(Character target)
        {
            base.Attack(target);
            Console.WriteLine($"{attackName} dealt 1 damage to {target}.");
            target.GetHit(1);
        }

        public override Side Side => side;
    }
}
