using ConsoleRpg.Characters;
using ConsoleRpg.Gears;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Controllers
{
    public class HumanController : PartyController
    {
        public HumanController(Side side) : base(side)
        {

        }

        public override void TakeTurn(CharacterManager characterManager, Character character, Party activeParty)
        {
            Console.WriteLine(@$"1 - Standard attack ({character.GetAttackName()})
2 - Do nothing
3 - Use item
4 - Equip gear
5 - Gear attack");
            if (character is VinFletcher)
            {
                Console.WriteLine($"6 - Quick Shot");
            }
            Console.WriteLine("What do you want to do? ");
            string response = Console.ReadLine();

            switch (response)
            {
                case "1":
                    character.Attack(characterManager.GetOppositeSideCharacter(character));
                    break;
                case "2":
                    character.DoNothing();
                    break;
                case "3":
                    Console.WriteLine("What item would you like to use?");
                    Console.WriteLine("1 - Potion (+10HP)");
                    string itemResponse = Console.ReadLine();
                    if (itemResponse == "1")
                    {
                        activeParty.UseItem(Item.Potion, character);
                    }
                    else Console.WriteLine($"Invalid Option. {character} will do nothing.");
                    break;
                case "4":
                    EquipGear(characterManager, character, activeParty);
                    break;
                case "5":
                    character.GearAttack(characterManager.GetOppositeSideCharacter(character));
                    break;
                case "6":
                    try
                    {
                        ((VinFletcher)character).QuickShot(characterManager.GetOppositeSideCharacter(character));
                    } catch (Exception)
                    {
                        Console.WriteLine($"Invalid Option. {character} will do nothing.");
                    }
                    break;
                default:
                    Console.WriteLine($"Invalid Option. {character} will do nothing.");
                    character.DoNothing();
                    break;
            }
        }

        public void EquipGear(CharacterManager characterManager, Character character, Party activeParty)
        {
            if (!activeParty.HasGear())
            {
                Console.WriteLine($"This party has no gear. {character} will do nothing.");
                return;
            }
            Console.WriteLine("What gear would you like to equip?");
            List<Gear> gearList = activeParty.GetGears();
            for (int i = 0; i < gearList.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {gearList[i]}");
            }
            string gearChoice = Console.ReadLine();
            Gear chosenGear;
            if (int.TryParse(gearChoice, out int gearChoiceNum))
            {
                try
                {
                    chosenGear = gearList[gearChoiceNum-1];
                }
                catch (Exception)
                {
                    Console.WriteLine($"Invalid Option. {character} will do nothing.");
                    return;
                }
            }
            else
            {
                Console.WriteLine($"Invalid Option. {character} will do nothing.");
                return;
            }
            // at this point, the player has chosen a valid Gear to equip
            if (character.hasGearOn())
            {
                Console.WriteLine($"{character}'s current gear, {character.GetGear()}, will be returned to the party inventory.");
                activeParty.AddGear(character.GetGear());
            }
            Console.WriteLine($"{character} has equipped {chosenGear}");
            character.EquipGear(chosenGear);
            activeParty.RemoveGear(chosenGear);
        }
    }
}
