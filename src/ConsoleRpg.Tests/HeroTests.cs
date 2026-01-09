using ConsoleRpg.Characters;
using ConsoleRpg.Parties;
using ConsoleRpg.Gears;
using System.IO;

namespace ConsoleRpg.Tests
{
    public class HeroTests
    {
        private Hero hero;
        private Skeleton skeletonOne;
        private Skeleton skeletonTwo;
        private Party testingParty;
        private CharacterManager characterManager;

        public HeroTests()
        {
            hero = new Hero("Testing Hero");
            skeletonOne = new Skeleton("Testing Skeleton One");
            skeletonTwo = new Skeleton("Testing Skeleton Two");
            testingParty = new Party(1);
            testingParty.AddToParty(hero);
            testingParty.AddToParty(skeletonOne);
            testingParty.AddToParty(skeletonTwo);
            characterManager = new CharacterManager();
            characterManager.AddCharacters(testingParty.GetCharacters());
        }

        [Fact]
        public void Hero_LevelsUp_AndAttacksStronger()
        {
            for (int i = 0; i < 4; i++)
            {
                hero.Attack(skeletonOne);
            }
            Assert.False(skeletonOne.IsDead());
            hero.Attack(skeletonOne);
            Assert.True(skeletonOne.IsDead());

            characterManager.LevelUpHero();
            for (int i = 0; i < 3; i++)
            {
                hero.Attack(skeletonTwo);
            }
            Assert.True(skeletonTwo.IsDead());
        }

        [Fact]
        public void Hero_EquipsGear_AndAttacksStronger()
        {
            hero.EquipGear(new Sword("Sword"));
            Rank swordRank = hero.GetGear().GearRank;
            switch (swordRank)
            {
                case Rank.Legendary:
                case Rank.Unique:
                    break;
                case Rank.Rare:
                    hero.GearAttack(skeletonOne);
                    break;
                case Rank.Normal:
                default:
                    hero.GearAttack(skeletonOne);
                    hero.GearAttack(skeletonOne);
                    break;
            }
            Assert.False(skeletonOne.IsDead());

            hero.GearAttack(skeletonOne);
            Assert.True(skeletonOne.IsDead());
        }

        [Fact]
        public void Hero_EquipsGear_Successfully()
        {
            Assert.Equal("TESTING HERO  25/25", hero.GetStatus());

            hero.EquipGear(new Sword("Sword"));
            Rank swordRank = hero.GetGear().GearRank;

            Assert.Equal($"TESTING HERO  25/25  Sword ({swordRank})", hero.GetStatus());
        }

        [Fact]
        public void Hero_HP_StaysInValidRange()
        {
            hero.DrinkPotion();
            Assert.Equal("TESTING HERO  25/25", hero.GetStatus());
            Assert.False(hero.IsUnderHalfHP());

            hero.GetHit(100);
            Assert.Equal("TESTING HERO  0/25", hero.GetStatus());
            Assert.True(hero.IsDead());
        }
    }
}