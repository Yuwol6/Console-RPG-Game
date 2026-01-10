using ConsoleRpg.Characters;
using ConsoleRpg.Gears;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Parties
{
    /// <summary>
    /// Represents a party of skeletons on the enemy side, managing shared items such as gear and potions.
    /// </summary>
    /// <remarks>Every time this party is created, it will hold one more skeleton than the last.</remarks>
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
