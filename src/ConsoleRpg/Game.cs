using ConsoleRpg.Characters;
using ConsoleRpg.Gears;
using ConsoleRpg.Controllers;
using ConsoleRpg.Parties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg
{
    public class Game
    {
        private GameSettings gameSettings;
        public Game(GameSettings gameSettings)
        {
            this.gameSettings = gameSettings;
        }

        public void RunGame()
        {
            while (true)
            {
                List<Character> originalCharacters = gameSettings.CharacterManager.GetCharacters();
                for (int i = originalCharacters.Count - 1; i >= 0; i--)
                {
                    Console.ResetColor();
                    Character character = originalCharacters[i];
                    if (character.IsDead()) continue;
                    var gameEnded = gameSettings.CharacterManager.CheckWinningSide();
                    if (gameEnded.ended == true)
                    {
                        if (gameEnded.winningSide == Side.Enemy)
                        {
                            Console.WriteLine("The heroes lost and Satan's forces have prevailed... The world will end now.");
                        }
                        else
                        {
                            if (gameSettings.CharacterManager.Stage == 0)
                            {
                                Console.WriteLine("The heroes have cleared the first floor. They move on to the next floor.");
                                ProgressStage(gameSettings.CharacterManager, gameSettings.AllyParty, gameSettings.EnemyPartyTwo);
                                break;
                            }
                            else if (gameSettings.CharacterManager.Stage >= 1 && gameSettings.CharacterManager.Stage < 66)
                            {
                                Console.WriteLine("The heroes have cleared the floor. They see two doors; which door are they going through?");
                                Console.WriteLine(@"1 - The yellow door
2 - The black door");
                                string doorInput = Console.ReadLine();
                                bool breakLoop = false;
                                switch (doorInput)
                                {
                                    case "1":
                                        ProgressStage(gameSettings.CharacterManager, gameSettings.AllyParty, new RandomParty(1));
                                        Console.WriteLine("The heroes move on through the yellow door. They see some more enemies.");
                                        breakLoop = true;
                                        break;
                                    case "2":
                                        break;
                                    default:
                                        Console.Write("Invalid Input. ");
                                        break;
                                }
                                if (breakLoop) break;
                                Console.WriteLine("The heroes move on through the black door. They are met with SATAN, crouching on top of a mountain of skulls.");
                                ProgressStage(gameSettings.CharacterManager, gameSettings.AllyParty, gameSettings.EnemyPartyThree);
                                gameSettings.CharacterManager.SetToLastStage();
                                break;
                            }
                            else Console.WriteLine("The heroes won and SATAN was defeated! The world remains safe for now.");
                        }

                        Environment.Exit(0); // The game is finished
                    }

                    DisplayStatus(gameSettings.CharacterManager, character);

                    if (gameSettings.FriendlyController.IsSameSide(character))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        ControllerTurn(gameSettings.FriendlyController, gameSettings.CharacterManager, character, gameSettings.AllyParty);
                    }
                    if (gameSettings.EnemyController.IsSameSide(character))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Thread.Sleep(1000);
                        ControllerTurn(gameSettings.EnemyController, gameSettings.CharacterManager, character, gameSettings.ActiveEnemyParty);
                    }

                    foreach (Character deadCharacter in gameSettings.CharacterManager.GetCharacters())
                    {
                        gameSettings.CharacterManager.RemoveDeadCharacter(deadCharacter);
                    }

                    Console.WriteLine();
                    Thread.Sleep(500);
                }
            }
        }

        private void ControllerTurn(PartyController partyController, CharacterManager characterManager, Character character, Party activeParty)
        {
            partyController.TakeTurn(characterManager, character, activeParty);
        }

        private void DisplayStatus(CharacterManager characterManager, Character character)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"=========================================== STAGE {characterManager.Stage} ===========================================");
            characterManager.ShowStatus(Side.Friendly);
            Console.WriteLine("---------------------------------------------- VS ---------------------------------------------");
            characterManager.ShowStatus(Side.Enemy);
            Console.WriteLine("===============================================================================================");

            Console.WriteLine($"It is {character}'s turn...");
        }

        private void ProgressStage(CharacterManager characterManager, Party allyParty, Party enemyPartyNum)
        {
            characterManager.IncreaseStage();
            allyParty.CollectPotions(gameSettings.ActiveEnemyParty.PotionCount);
            allyParty.CollectGears(gameSettings.ActiveEnemyParty.GetGears());
            characterManager.LevelUpHero();
            characterManager.AddCharactersInFront(enemyPartyNum.GetCharacters());
            gameSettings.SetActiveEnemyParty(enemyPartyNum);
        }
    }
}