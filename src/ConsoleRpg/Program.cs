using ConsoleRpg.Characters;
using ConsoleRpg.Gears;
using ConsoleRpg.Controllers;

namespace ConsoleRpg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "The Final Battle";

            Console.Write("The True Programmer, what is your name? "); // entry setting inputs
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

            TrueProgrammer trueProgrammer = new TrueProgrammer(playerName);
            VinFletcher vinFletcher = new VinFletcher("Vin Fletcher");
            Skeleton skeletonOne = new Skeleton("skeleton1");
            Skeleton skeletonTwo = new Skeleton("skeleton2");
            Skeleton skeletonThree = new Skeleton("skeleton3");
            TheUncodedOne theUncodedOne = new TheUncodedOne("The Uncoded One");

            Party allyParty = new Party(3);
            allyParty.AddToParty(trueProgrammer);
            trueProgrammer.EquipGear(new Sword("Sword"));
            allyParty.AddToParty(vinFletcher);
            Party enemyPartyOne = new Party(1);
            enemyPartyOne.AddToParty(skeletonOne);
            skeletonOne.EquipGear(new Dagger("Dagger"));
            Party enemyPartyTwo = new Party(1);
            enemyPartyTwo.AddToParty(skeletonTwo);
            enemyPartyTwo.AddToParty(skeletonThree);
            enemyPartyTwo.AddGear(new Dagger("Dagger"));
            enemyPartyTwo.AddGear(new Dagger("Dagger"));
            Party enemyPartyThree = new Party(1);
            enemyPartyThree.AddToParty(theUncodedOne);
            Party activeEnemyParty = enemyPartyOne;

            CharacterManager characterManager = new CharacterManager();
            characterManager.AddCharacters(allyParty.GetCharacters());
            characterManager.AddCharacters(enemyPartyOne.GetCharacters());


            while (true)
            {
                foreach (Character character in characterManager.GetCharacters())
                {
                    Console.ResetColor();
                    var gameEnded = characterManager.CheckWinningSide();
                    if (gameEnded.ended == true)
                    {
                        if (gameEnded.winningSide == Side.Enemy)
                        {
                            Console.WriteLine("The heroes lost and the Uncoded One's forces have prevailed...");
                        }
                        else
                        {
                            if (characterManager.Stage == 0)
                            {
                                Console.WriteLine("The heroes have finished the first stage. They move on to the next cave.");
                                ProgressStage(characterManager, allyParty, ref activeEnemyParty, enemyPartyTwo);
                                continue;
                            }
                            else if (characterManager.Stage == 1)
                            {
                                Console.WriteLine("The heroes have finished the second stage. The move on to the final cave.");
                                ProgressStage(characterManager, allyParty, ref activeEnemyParty, enemyPartyThree);
                                continue;
                            }
                            else Console.WriteLine("The heroes won and the Uncoded One was defeated!");
                        }

                        Environment.Exit(0); // The game is finished
                    }

                    bool CharacterIsDead = false;
                    foreach (Character deadCharacter in characterManager.GetCharacters())
                    {
                        if (deadCharacter.markedForRemoval == true)
                        {
                            characterManager.RemoveCharacter(deadCharacter);
                            CharacterIsDead = true;
                            break;
                        }
                    }
                    if (CharacterIsDead) continue;
                    if (character.IsDead()) continue;

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

                    Console.WriteLine();
                    Thread.Sleep(500);
                }
            }
        }

        static void ControllerTurn(PartyController partyController, CharacterManager characterManager, Character character, Party activeParty)
        {
            if (partyController is ComputerController)
            {
                (partyController).TakeTurn(characterManager, character, activeParty);
            }
            else if (partyController is HumanController)
            {
                (partyController).TakeTurn(characterManager, character, activeParty);
            }
        }

        static void DisplayStatus(CharacterManager characterManager, Character character)
        {
            Console.WriteLine($"======================= STAGE {characterManager.Stage} =======================");
            characterManager.ShowStatus(Side.Friendly);
            Console.WriteLine("-------------------------- VS -------------------------");
            characterManager.ShowStatus(Side.Enemy);
            Console.WriteLine("=======================================================");

            Console.WriteLine($"It is {character}'s turn...");
        }

        static void ProgressStage(CharacterManager characterManager, Party allyParty, ref Party activeEnemyParty, Party enemyPartyNum)
        {
            characterManager.IncreaseStage();
            allyParty.CollectPotions(activeEnemyParty.PotionCount);
            allyParty.CollectGears(activeEnemyParty.GetGears());
            characterManager.AddCharacters(enemyPartyNum.GetCharacters());
            activeEnemyParty = enemyPartyNum;
        }
    }

    public enum Side
    {
        Friendly,
        Enemy
    }

}

