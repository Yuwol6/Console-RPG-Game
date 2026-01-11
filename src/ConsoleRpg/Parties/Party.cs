using ConsoleRpg.Characters;
using ConsoleRpg.Gears;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRpg.Parties
{
    /// <summary>
    /// Represents a party of characters on the same side, managing shared items and gear.
    /// </summary>
    public class Party
    {
        protected List<Character> party = new List<Character>();
        protected List<Gear> gears = new List<Gear>();
        public int PotionCount { get; private set; }

        public Party(int potionCount)
        {
            PotionCount = potionCount;
        }

        /// <summary>
        /// Adds a character to the party.
        /// </summary>
        /// <param name="character">The character to be added.</param>
        public void AddToParty(Character character)
        {
            party.Add(character);
        }

        /// <summary>
        /// Gets the characters in the party.
        /// </summary>
        /// <returns>A copy of the list of characters in the party.</returns>
        public List<Character> GetCharacters()
        {
            return new List<Character>(party);
        }

        /// <summary>
        /// Adds a specified number of potions to the party.
        /// </summary>
        /// <param name="potionsToAdd">The number of potions to add.</param>
        public void CollectPotions(int potionsToAdd)
        {
            PotionCount += potionsToAdd;
            if (potionsToAdd > 0) Console.WriteLine($"The hero party has collected {potionsToAdd} potion/s from the last battle.");
        }

        /// <summary>
        /// Adds specified gear/s to the party.
        /// </summary>
        /// <param name="gears">The gear/s to add.</param>
        public void CollectGears(List<Gear> gears)
        {
            this.gears.AddRange(gears);
            if (gears.Count > 0) Console.WriteLine($"The hero party has collected {gears.Count()} gear/s from the last battle.");
        }

        /// <summary>
        /// Consumes an item from the party's inventory and applies its effect to the specified character.
        /// </summary>
        /// <param name="item">The item to consume.</param>
        /// <param name="character">The character the item is applied to.</param>
        /// <remarks>
        /// If the specified item is not available in the party inventory, no action is taken.
        /// Currently, the only item type available for use is potions.
        /// </remarks>
        public void UseItem(Item item, Character character)
        {
            if (item == Item.Potion)
            {
                if (PotionCount > 0)
                {
                    PotionCount--;
                    character.DrinkPotion();
                    Console.WriteLine($"This party now has {PotionCount} potion/s left.");
                }
                else
                {
                    Console.WriteLine($"This party has no potions left... {character} will do nothing.");
                }
            }
        }

        /// <summary>
        /// Gets the available gear in the party's inventory.
        /// </summary>
        /// <returns>A copy of the list of gears in the party's inventory.</returns>
        public List<Gear> GetGears()
        {
            return new List<Gear>(gears);
        }

        /// <summary>
        /// Gets the first available gear in the party's inventory.
        /// </summary>
        /// <returns>The first available gear in the party's inventory.</returns>
        public Gear GetFirstGear()
        {
            return gears[0];
        }

        /// <summary>
        /// Adds a gear to the party's inventory.
        /// </summary>
        /// <param name="gear">The gear to be added.</param>
        public void AddGear(Gear gear)
        {
            gears.Add(gear);
        }

        /// <summary>
        /// Removes a gear from the party's inventory.
        /// </summary>
        /// <param name="gear">The gear to be removed.</param>
        /// <remarks>The gear specified must be in the party's inventory.</remarks>
        public void RemoveGear(Gear gear)
        {
            gears.Remove(gear);
        }

        /// <summary>
        /// Determines if the party's inventory has any gear.
        /// </summary>
        /// <returns>True if the party has any available gear; otherwise, false.</returns>
        public bool HasGear()
        {
            if (gears.Count > 0) return true;
            return false;
        }
    }
    public enum Item
    {
        Potion
    }
}
