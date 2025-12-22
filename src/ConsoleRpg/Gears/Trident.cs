using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Gears
{
    public class Trident : Gear
    {
        public Trident(string name) : base(name)
        {
            AttackName = "HELLFIRE";
            Effect = "7 damage";
            GearRank = Rank.Legendary;
            Damage = 7;
        }
    }
}
