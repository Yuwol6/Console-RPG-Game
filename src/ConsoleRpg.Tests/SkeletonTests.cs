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
    public class SkeletonTests
    {
        private Skeleton skeletonOne;
        private Skeleton skeletonTwo;
        private Party testingParty;
        private CharacterManager characterManager;
        public SkeletonTests()
        {
            skeletonOne = new Skeleton("Testing Skeleton One");
            skeletonTwo = new Skeleton("Testing Skeleton Two");
            testingParty = new Party(1);
            testingParty.AddToParty(skeletonOne);
            testingParty.AddToParty(skeletonTwo);
            characterManager = new CharacterManager();
            characterManager.AddCharacters(testingParty.GetCharacters());
        }

        [Fact]
        public void SkeletonAttackTest()
        {
            skeletonOne.Attack(skeletonTwo);
            Assert.True(skeletonTwo.GetStatus() == "TESTING SKELETON TWO  5/5" || skeletonTwo.GetStatus() == "TESTING SKELETON TWO  4/5");

            skeletonOne.Attack(skeletonTwo);
            Assert.True(skeletonTwo.GetStatus() == "TESTING SKELETON TWO  5/5" || skeletonTwo.GetStatus() == "TESTING SKELETON TWO  4/5" || 
                skeletonTwo.GetStatus() == "TESTING SKELETON TWO  3/5");

            skeletonOne.Attack(skeletonTwo);
            Assert.True(skeletonTwo.GetStatus() == "TESTING SKELETON TWO  5/5" || skeletonTwo.GetStatus() == "TESTING SKELETON TWO  4/5" || 
                skeletonTwo.GetStatus() == "TESTING SKELETON TWO  3/5" || skeletonTwo.GetStatus() == "TESTING SKELETON TWO  2/5");
        }

        [Fact]
        public void SkeletonGearAttackTest()
        {
            skeletonOne.EquipGear(new Dagger("Dagger"));
            Rank daggerRank = skeletonOne.GetGear().GearRank;
            switch (daggerRank)
            {
                case Rank.Legendary:
                    break;
                case Rank.Unique:
                    skeletonOne.GearAttack(skeletonTwo);
                    break;
                case Rank.Rare:
                    skeletonOne.GearAttack(skeletonTwo);
                    skeletonOne.GearAttack(skeletonTwo);
                    break;
                case Rank.Normal:
                default:
                    skeletonOne.GearAttack(skeletonTwo);
                    skeletonOne.GearAttack(skeletonTwo);
                    skeletonOne.GearAttack(skeletonTwo);
                    skeletonOne.GearAttack(skeletonTwo);
                    break;
            }
            Assert.False(skeletonTwo.IsDead());

            skeletonOne.GearAttack(skeletonTwo);
            Assert.True(skeletonTwo.IsDead());
        }

        [Fact]
        public void SkeletonGearStatusTest()
        {
            Assert.Equal("TESTING SKELETON ONE  5/5", skeletonOne.GetStatus());

            skeletonOne.EquipGear(new Dagger("Dagger"));
            Rank daggerRank = skeletonOne.GetGear().GearRank;

            Assert.Equal($"TESTING SKELETON ONE  5/5  Dagger ({daggerRank})", skeletonOne.GetStatus());
        }

        [Fact]
        public void SkeletonHPTest()
        {
            skeletonOne.DrinkPotion();
            Assert.Equal("TESTING SKELETON ONE  5/5", skeletonOne.GetStatus());
            Assert.False(skeletonOne.IsUnderHalfHP());

            skeletonOne.GetHit(100);
            Assert.Equal("TESTING SKELETON ONE  0/5", skeletonOne.GetStatus());
            Assert.True(skeletonOne.IsDead());
        }
    }
}
