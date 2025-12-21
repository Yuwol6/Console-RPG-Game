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
    public class Party
    {
        protected List<Character> party = new List<Character>();
        protected List<Gear> gears = new List<Gear>();
        public int PotionCount { get; private set; }

        public Party(int potionCount)
        {
            PotionCount = potionCount;
        }

        public void AddToParty(Character character)
        {
            party.Add(character);
        }

        public List<Character> GetCharacters()
        {
            return new List<Character>(party);
        }

        public void CollectPotions(int potionsToAdd)
        {
            PotionCount += potionsToAdd;
            if (potionsToAdd > 0) Console.WriteLine($"The hero party has collected {potionsToAdd} potion/s from the battle.");
        }

        public void CollectGears(List<Gear> gears)
        {
            this.gears.AddRange(gears);
            if (gears.Count > 0) Console.WriteLine($"The hero party has collected {gears.Count()} gear/s from the battle.");
        }

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

        public List<Gear> GetGears()
        {
            return new List<Gear>(gears);
        }

        public Gear GetFirstGear()
        {
            return gears[0];
        }

        public void AddGear(Gear gear)
        {
            gears.Add(gear);
        }

        public void RemoveGear(Gear gear)
        {
            gears.Remove(gear);
        }

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
