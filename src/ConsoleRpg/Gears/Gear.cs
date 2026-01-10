using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Gears
{
    /// <summary>
    /// Base class for all gear in the game.
    /// </summary>
    public class Gear
    {
        public string Name {  get; protected set; }
        public string AttackName { get; protected set; }
        public string Effect { get; protected set; }
        public int Damage { get; protected set; }
        protected Random random = new Random();
        public Rank GearRank { get; protected set; }
        public Gear(string name)
        {
            Name = name;
            GearRank = GetRandomRank();
            Damage = DamageFromRank(GearRank);
        }

        public override string ToString()
        {
            return $"{Name} ({GearRank})";
        }

        /// <summary>
        /// Returns a random rank to be assigned to the gear.
        /// </summary>
        /// <returns>The rank randomly generated.</returns>
        protected Rank GetRandomRank()
        {
            int rankNumber = random.Next(10);
            switch (rankNumber)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return Rank.Normal;
                case 4:
                case 5:
                case 6:
                    return Rank.Rare;
                case 7:
                case 8:
                    return Rank.Unique;
                case 9:
                    return Rank.Legendary;
                default:
                    return Rank.Normal;
            }
        }

        /// <summary>
        /// Calculates the bonus damage added to a weapon based on its rank.
        /// </summary>
        /// <param name="rank">The rank used to determine bonus damage.</param>
        /// <returns>The bonus damage associated with the specified rank.</returns>
        protected int DamageFromRank(Rank rank)
        {
            switch (rank)
            {
                case Rank.Normal:
                    return 0;
                case Rank.Rare:
                    return 1;
                case Rank.Unique:
                    return 3;
                case Rank.Legendary:
                    return 7;
                default:
                    return 0;
            }
        }
    }

    public enum Rank
    {
        Normal,
        Rare,
        Unique,
        Legendary
    }
}
