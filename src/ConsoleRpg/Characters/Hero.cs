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
        private int level;
        private int punchDamage = 1;

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
            Console.WriteLine($"{attackName} dealt {punchDamage} damage to {target}.");
            target.GetHit(punchDamage);
        }

        public void LevelUp()
        {
            Console.WriteLine("The hero has levelled up and will do one more damage on PUNCH!");
            punchDamage++;
        }

        public override Side Side => side;
    }
}
