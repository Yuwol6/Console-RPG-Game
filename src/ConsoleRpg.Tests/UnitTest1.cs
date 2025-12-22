using ConsoleRpg.Characters;
using ConsoleRpg.Parties;
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
        public void HeroLevelUpTest()
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
    }
}