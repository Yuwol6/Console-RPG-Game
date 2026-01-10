using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Gears
{
    /// <summary>
    /// Represents the default gear for Satan, with 7 base damage and a permanent rank of Legendary.
    /// </summary>
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
