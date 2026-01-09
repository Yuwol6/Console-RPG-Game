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
    public class GameSettings
    {
        private string? playerName;
        public CharacterManager CharacterManager { get; private set; }
        public Party AllyParty { get; private set; }
        public Party ActiveEnemyParty { get; private set; }
        public Party EnemyPartyTwo { get; private set; }
        public Party EnemyPartyThree { get; private set; }
        public PartyController FriendlyController { get; private set; }
        public PartyController EnemyController { get; private set; }
        public GameSettings()
        {
            CreateInitialSettings();
            CreatePartySettings();
        }

        // constructor for testing
        public GameSettings(bool skipConsoleInput)
        {
            if (skipConsoleInput)
            {
                playerName = "Testing Player";
                FriendlyController = DecideController(Side.Friendly, "2");
                EnemyController = DecideController(Side.Enemy, "2");

                CreatePartySettings();
            }
        }

        private void CreateInitialSettings()
        {
            Console.Write("You are the hero destined to hunt down Satan deep inside the dungeon. What is your name? ");
            playerName = Console.ReadLine();
            if (string.IsNullOrEmpty(playerName)) playerName = "Unnamed";

            Console.Write("Would you like the hero party as a human player or a computer? Input 1 for player and 2 for computer. ");
            string? friendlyPartyChoice = Console.ReadLine();
            Console.Write("Would you like the enemy party as a human player or a computer? Input 1 for player and 2 for computer. ");
            string? enemyPartyChoice = Console.ReadLine();
            Console.WriteLine();

            FriendlyController = DecideController(Side.Friendly, friendlyPartyChoice);
            EnemyController = DecideController(Side.Enemy, enemyPartyChoice);
            Console.WriteLine();
        }

        private void CreatePartySettings()
        {
            Hero hero = new Hero(playerName);
            Legolas legolas = new Legolas("Legolas");
            Skeleton skeletonOne = new Skeleton("Tim the skeleton");
            Skeleton skeletonTwo = new Skeleton("Jake the skeleton");
            Skeleton skeletonThree = new Skeleton("Emma the skeleton");
            Satan satan = new Satan("Satan");

            AllyParty = new Party(3);
            AllyParty.AddToParty(legolas);
            AllyParty.AddToParty(hero);
            hero.EquipGear(new Sword("Sword"));
            Party enemyPartyOne = new Party(1);
            enemyPartyOne.AddToParty(skeletonOne);
            skeletonOne.EquipGear(new Dagger("Dagger"));
            EnemyPartyTwo = new Party(1);
            EnemyPartyTwo.AddToParty(skeletonTwo);
            EnemyPartyTwo.AddToParty(skeletonThree);
            EnemyPartyTwo.AddGear(new Dagger("Dagger"));
            EnemyPartyTwo.AddGear(new Dagger("Dagger"));
            EnemyPartyThree = new Party(1);
            EnemyPartyThree.AddToParty(satan);
            satan.EquipGear(new Trident("Hellforged Trident"));
            ActiveEnemyParty = enemyPartyOne;

            CharacterManager = new CharacterManager();
            CharacterManager.AddCharacters(enemyPartyOne.GetCharacters());
            CharacterManager.AddCharacters(AllyParty.GetCharacters());
        }

        private PartyController DecideController(Side side, string input)
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

        public void SetActiveEnemyParty(Party party)
        {
            ActiveEnemyParty = party;
        }
    }
    public enum Side
    {
        Friendly,
        Enemy
    }
}
