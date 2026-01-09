using ConsoleRpg.Characters;
using ConsoleRpg.Parties;
using ConsoleRpg.Gears;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Tests
{
    public class SatanTests
    {
        private Satan satan;
        private Hero hero;
        private Party testingParty;
        private CharacterManager characterManager;

        public SatanTests()
        {
            satan = new Satan("Testing Satan");
            hero = new Hero("Testing Hero");
            testingParty = new Party(1);
            testingParty.AddToParty(satan);
            testingParty.AddToParty(hero);
            characterManager = new CharacterManager();
            characterManager.AddCharacters(testingParty.GetCharacters());
        }

        [Fact]
        public void Satan_Attacks_Successfully()
        {
            satan.Attack(hero);
            Assert.True(hero.GetStatus() == "TESTING HERO  25/25" || hero.GetStatus() == "TESTING HERO  24/25" || 
                hero.GetStatus() == "TESTING HERO  23/25");

            satan.Attack(hero);
            Assert.True(hero.GetStatus() == "TESTING HERO  25/25" || hero.GetStatus() == "TESTING HERO  24/25" ||
                hero.GetStatus() == "TESTING HERO  23/25" || hero.GetStatus() == "TESTING HERO  22/25" ||
                hero.GetStatus() == "TESTING HERO  21/25");
        }

        [Fact]
        public void Satan_EquipsGear_AndAttacksStronger()
        {
            satan.EquipGear(new Trident("Hellforged Trident"));
            Assert.True(satan.GetGear().GearRank == Rank.Legendary);

            satan.GearAttack(hero);
            Assert.Equal("TESTING HERO  18/25", hero.GetStatus());

            satan.GearAttack(hero);
            satan.GearAttack(hero);
            Assert.Equal("TESTING HERO  4/25", hero.GetStatus());

            satan.GearAttack(hero);
            Assert.Equal("TESTING HERO  0/25", hero.GetStatus());
            Assert.True(hero.IsDead());
        }

        [Fact]
        public void Satan_HP_StaysInValidRange()
        {
            satan.DrinkPotion();
            Assert.Equal("TESTING SATAN  36/36", satan.GetStatus());
            Assert.False(satan.IsUnderHalfHP());

            satan.GetHit(100);
            Assert.Equal("TESTING SATAN  0/36", satan.GetStatus());
            Assert.True(satan.IsDead());
        }
    }
}
