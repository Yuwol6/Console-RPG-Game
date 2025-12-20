using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Gears
{
    public class Gear
    {
        public string Name {  get; protected set; }
        public string AttackName { get; protected set; }
        public string Effect { get; protected set; }
        public int Damage { get; protected set; }
        public Gear(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
