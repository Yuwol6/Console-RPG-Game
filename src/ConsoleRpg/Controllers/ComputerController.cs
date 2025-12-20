using ConsoleRpg.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Controllers
{
    public class ComputerController : PartyController
    {
        Random random = new Random();
        public ComputerController(Side side) : base(side)
        {
            
        }

        public override void TakeTurn(CharacterManager characterManager, Character character, Party activeParty)
        {
            int potionChance = random.Next(4);
            int gearChance = random.Next(2);
            if (activeParty.PotionCount > 0 && character.IsUnderHalfHP() && potionChance == 1)
            {
                activeParty.UseItem(Item.Potion, character);
            }
            else if (!character.hasGearOn() && activeParty.HasGear() && gearChance == 1)
            {
                Console.WriteLine($"{character} has equipped {activeParty.GetFirstGear()}");
                character.EquipGear(activeParty.GetFirstGear());
                activeParty.RemoveGear(activeParty.GetFirstGear());
            }
            else if (character.hasGearOn())
            {
                character.GearAttack(characterManager.GetOppositeSideCharacter(character));
            }
            else character.Attack(characterManager.GetOppositeSideCharacter(character));
        }
    }
}
