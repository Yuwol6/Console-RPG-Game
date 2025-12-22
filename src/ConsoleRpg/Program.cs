using ConsoleRpg.Characters;
using ConsoleRpg.Gears;
using ConsoleRpg.Controllers;
using ConsoleRpg.Parties;

namespace ConsoleRpg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ConsoleRPG";
            // entry settings
            Console.Write("You are the hero destined to hunt down Satan deep inside the dungeon. What is your name? ");
            string? playerName = Console.ReadLine();
            if (string.IsNullOrEmpty(playerName)) playerName = "Unnamed";

            Console.Write("Would you like the hero party as a human player or a computer? Input 1 for player and 2 for computer. ");
            string? friendlyPartyChoice = Console.ReadLine();
            Console.Write("Would you like the enemy party as a human player or a computer? Input 1 for player and 2 for computer. ");
            string? enemyPartyChoice = Console.ReadLine();
            Console.WriteLine();

            PartyController friendlyController = DecideController(Side.Friendly, friendlyPartyChoice);
            PartyController enemyController = DecideController(Side.Enemy, enemyPartyChoice);
            Console.WriteLine();

            PartyController DecideController(Side side, string input)
            {
                switch (input)
                {
                    case "1":
                        Console.WriteLine($"The {side} side will be played by a human player.");
                        return new HumanController(side);
                    case "2":
                        Console.WriteLine($"The {side} side will be played by a computer.");
                        return new ComputerController(side);
                    default:
                        Console.WriteLine($"Invalid Input. The {side} side will be defaulted to computer.");
                        return new ComputerController(side);
                }
            }

            #region Party Settings
            Hero hero = new Hero(playerName);
            Legolas legolas = new Legolas("Legolas");
            Skeleton skeletonOne = new Skeleton("Tim the skeleton");
            Skeleton skeletonTwo = new Skeleton("Jake the skeleton");
            Skeleton skeletonThree = new Skeleton("Emma the skeleton");
            Satan satan = new Satan("Satan");

            Party allyParty = new Party(3);
            allyParty.AddToParty(legolas);
            allyParty.AddToParty(hero);
            hero.EquipGear(new Sword("Sword"));
            Party enemyPartyOne = new Party(1);
            enemyPartyOne.AddToParty(skeletonOne);
            skeletonOne.EquipGear(new Dagger("Dagger"));
            Party enemyPartyTwo = new Party(1);
            enemyPartyTwo.AddToParty(skeletonTwo);
            enemyPartyTwo.AddToParty(skeletonThree);
            enemyPartyTwo.AddGear(new Dagger("Dagger"));
            enemyPartyTwo.AddGear(new Dagger("Dagger"));
            Party enemyPartyThree = new Party(1);
            enemyPartyThree.AddToParty(satan);
            satan.EquipGear(new Trident("Hellforged Trident"));
            Party activeEnemyParty = enemyPartyOne;

            CharacterManager characterManager = new CharacterManager();
            characterManager.AddCharacters(enemyPartyOne.GetCharacters());
            characterManager.AddCharacters(allyParty.GetCharacters());
            #endregion


            while (true)
            {
                List<Character> originalCharacters = characterManager.GetCharacters();
                for (int i = originalCharacters.Count - 1; i >= 0; i--)
                {
                    Console.ResetColor();
                    Character character = originalCharacters[i];
                    if (character.IsDead()) continue;
                    var gameEnded = characterManager.CheckWinningSide();
                    if (gameEnded.ended == true)
                    {
                        if (gameEnded.winningSide == Side.Enemy)
                        {
                            Console.WriteLine("The heroes lost and Satan's forces have prevailed... The world will end now.");
                        }
                        else
                        {
                            if (characterManager.Stage == 0)
                            {
                                Console.WriteLine("The heroes have cleared the first floor. They move on to the next floor.");
                                ProgressStage(characterManager, allyParty, ref activeEnemyParty, enemyPartyTwo);
                                break;
                            }
                            else if (characterManager.Stage >= 1 && characterManager.Stage < 66)
                            {
                                Console.WriteLine("The heroes have cleared the floor. They see two doors; which door are they going through?");
                                Console.WriteLine(@"1 - The yellow door
2 - The black door");
                                string doorInput = Console.ReadLine();
                                bool breakLoop = false;
                                switch (doorInput)
                                {
                                    case "1":
                                        ProgressStage(characterManager, allyParty, ref activeEnemyParty, new RandomParty(1));
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
                                ProgressStage(characterManager, allyParty, ref activeEnemyParty, enemyPartyThree);
                                characterManager.SetToLastStage();
                                break;
                            }
                            else Console.WriteLine("The heroes won and SATAN was defeated! The world remains safe for now.");
                        }

                        Environment.Exit(0); // The game is finished
                    }

                    DisplayStatus(characterManager, character);

                    if (friendlyController.IsSameSide(character))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        ControllerTurn(friendlyController, characterManager, character, allyParty);
                    }
                    if (enemyController.IsSameSide(character))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        ControllerTurn(enemyController, characterManager, character, activeEnemyParty);
                    }

                    foreach (Character deadCharacter in characterManager.GetCharacters())
                    {
                        characterManager.RemoveDeadCharacter(deadCharacter);
                    }

                    Console.WriteLine();
                    Thread.Sleep(500);
                }
            }
        }

        static void ControllerTurn(PartyController partyController, CharacterManager characterManager, Character character, Party activeParty)
        {
            partyController.TakeTurn(characterManager, character, activeParty);
        }

        static void DisplayStatus(CharacterManager characterManager, Character character)
        {
            Console.WriteLine($"=========================================== STAGE {characterManager.Stage} ===========================================");
            characterManager.ShowStatus(Side.Friendly);
            Console.WriteLine("---------------------------------------------- VS ---------------------------------------------");
            characterManager.ShowStatus(Side.Enemy);
            Console.WriteLine("===============================================================================================");

            Console.WriteLine($"It is {character}'s turn...");
        }

        static void ProgressStage(CharacterManager characterManager, Party allyParty, ref Party activeEnemyParty, Party enemyPartyNum)
        {
            characterManager.IncreaseStage();
            allyParty.CollectPotions(activeEnemyParty.PotionCount);
            allyParty.CollectGears(activeEnemyParty.GetGears());
            characterManager.LevelUpHero();
            characterManager.AddCharactersInFront(enemyPartyNum.GetCharacters());
            activeEnemyParty = enemyPartyNum;
        }
    }

    public enum Side
    {
        Friendly,
        Enemy
    }

}

