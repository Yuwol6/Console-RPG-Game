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
    public class LegolasTests
    {
        private Skeleton skeleton;
        private Legolas legolas;
        private Party testingParty;
        private CharacterManager characterManager;

        public LegolasTests()
        {
            skeleton = new Skeleton("Testing Skeleton");
            legolas = new Legolas("Testing Legolas");
            testingParty = new Party(1);
            testingParty.AddToParty(skeleton);
            testingParty.AddToParty(legolas);
            characterManager = new CharacterManager();
            characterManager.AddCharacters(testingParty.GetCharacters());
        }

        [Fact]
        public void LegolasAttackTest()
        {
            legolas.Attack(skeleton);
            legolas.Attack(skeleton);
            legolas.Attack(skeleton);
            legolas.Attack(skeleton);
            Assert.Equal("TESTING SKELETON  1/5", skeleton.GetStatus());

            legolas.Attack(skeleton);
            Assert.True(skeleton.IsDead());
        }

        [Fact]
        public void QuickShotTest()
        {
            legolas.QuickShot(skeleton);
            Assert.True(skeleton.GetStatus() == "TESTING SKELETON  5/5" || skeleton.GetStatus() == "TESTING SKELETON  2/5");

            legolas.QuickShot(skeleton);
            Assert.True(skeleton.GetStatus() == "TESTING SKELETON  5/5" || skeleton.GetStatus() == "TESTING SKELETON  2/5" || 
                skeleton.GetStatus() == "TESTING SKELETON  0/5");
        }

        [Fact]
        public void LegolasGearAttackTest()
        {
            legolas.EquipGear(new Dagger("Dagger"));
            Rank daggerRank = legolas.GetGear().GearRank;
            switch (daggerRank)
            {
                case Rank.Legendary:
                    break;
                case Rank.Unique:
                    legolas.GearAttack(skeleton);
                    break;
                case Rank.Rare:
                    legolas.GearAttack(skeleton);
                    legolas.GearAttack(skeleton);
                    break;
                case Rank.Normal:
                default:
                    legolas.GearAttack(skeleton);
                    legolas.GearAttack(skeleton);
                    legolas.GearAttack(skeleton);
                    legolas.GearAttack(skeleton);
                    break;
            }
            Assert.False(skeleton.IsDead());

            legolas.GearAttack(skeleton);
            Assert.True(skeleton.IsDead());
        }

        [Fact]
        public void LegolasHPTest()
        {
            legolas.DrinkPotion();
            Assert.Equal("TESTING LEGOLAS  15/15", legolas.GetStatus());
            Assert.False(legolas.IsUnderHalfHP());

            legolas.GetHit(100);
            Assert.Equal("TESTING LEGOLAS  0/15", legolas.GetStatus());
            Assert.True(legolas.IsDead());
        }
    }
}
