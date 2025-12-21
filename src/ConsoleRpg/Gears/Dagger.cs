using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Gears
{
    public class Dagger : Gear
    {
        public Dagger(string name) : base(name)
        {
            AttackName = "STAB";
            Effect = "1 damage";
            Damage += 1;
        }
    }
}
