using ConsoleRpg.Characters;
using ConsoleRpg.Gears;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Parties
{
    public class RandomParty : Party
    {
        static int enemyCount = 1;
        public RandomParty(int potionCount) : base(potionCount)
        {
            enemyCount++;
            for (int i = 0; i < enemyCount; i++)
            {
                AddToParty(new Skeleton("Yellow Skeleton"));
                AddGear(new Dagger("Dagger"));
                AddGear(new Dagger("Dagger"));
            }
        }
    }
}
