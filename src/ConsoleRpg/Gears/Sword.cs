using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Gears
{
    /// <summary>
    /// Represents the default gear for the Hero, with 2 base damage.
    /// </summary>
    public class Sword : Gear
    {
        public Sword(string name) : base(name)
        {
            AttackName = "SLASH";
            Effect = "2 damage";
            Damage += 2;
        }
    }
}
