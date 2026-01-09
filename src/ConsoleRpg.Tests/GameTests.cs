using ConsoleRpg.Characters;
using ConsoleRpg.Gears;
using ConsoleRpg.Controllers;
using ConsoleRpg.Parties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Tests
{
    public class GameTests
    {
        Game testGame;
        public GameTests()
        {
            testGame = new Game(new GameSettings(true));
        }

        [Fact]
        public void ProgressStageSetActivePartyTest()
        {
            List<Character> activeEnemyParty;
            string partyStatus = "";

            testGame.ProgressStageSetActiveEnemyParty(true, 2);
            activeEnemyParty = testGame.GetActiveEnemyPartyCharacters();
            foreach (Character enemy in activeEnemyParty)
            {
                partyStatus += enemy.GetStatus();
            }
            Assert.Equal("JAKE THE SKELETON  5/5EMMA THE SKELETON  5/5", partyStatus);


            testGame.ProgressStageSetActiveEnemyParty(true, 3);
            activeEnemyParty = testGame.GetActiveEnemyPartyCharacters();
            Assert.Equal("SATAN  36/36  Hellforged Trident (Legendary)", activeEnemyParty[0].GetStatus());
        }

        [Fact]
        public void ProgressStageCollectPotionsTest()
        {
            Assert.Equal(3, testGame.GetAllyPotionCount());
            testGame.ProgressStageSetActiveEnemyParty(true, 2);
            Assert.Equal(4, testGame.GetAllyPotionCount());

            testGame.ProgressStageSetActiveEnemyParty(true, 3);
            Assert.Equal(5, testGame.GetAllyPotionCount());
        }

        [Fact]
        public void ProgressStageCollectGearsTest()
        {
            Assert.Equal(0, testGame.GetAllyGearsCount());
            testGame.ProgressStageSetActiveEnemyParty(true, 2);
            Assert.Equal(0, testGame.GetAllyGearsCount());

            testGame.ProgressStageSetActiveEnemyParty(true, 3);
            Assert.Equal(2, testGame.GetAllyGearsCount());
        }
    }
}
