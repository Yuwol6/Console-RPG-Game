using ConsoleRpg.Characters;
using ConsoleRpg.Parties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Controllers
{
    /// <summary>
    /// Represents a controller that automatically takes turns for characters.
    /// </summary>
    public class ComputerController : PartyController
    {
        Random random = new Random();
        public ComputerController(Side side) : base(side) {}

        /// <summary>
        /// Executes an automated turn for a character using a prioritized set of actions.
        /// </summary>
        /// <param name="characterManager">Provides access to the active characters in the game.</param>
        /// <param name="character">The character taking the turn.</param>
        /// <param name="activeParty">The party of the character.</param>
        /// <remarks>Action priority: drink a potion, equip gear, use gear attack, then basic attack. Some actions may be skipped 
        /// based on chance or availability.</remarks>
        public override void TakeTurn(CharacterManager characterManager, Character character, Party activeParty)
        {
            int potionChance = random.Next(4);
            int gearChance = random.Next(2);
            if (activeParty.PotionCount > 0 && character.IsUnderHalfHP() && potionChance == 1)
            {
                activeParty.UseItem(Item.Potion, character);
            }
            else if (!character.HasGearOn() && activeParty.HasGear() && gearChance == 1)
            {
                Console.WriteLine($"{character} has equipped {activeParty.GetFirstGear()}");
                character.EquipGear(activeParty.GetFirstGear());
                activeParty.RemoveGear(activeParty.GetFirstGear());
            }
            else if (character.HasGearOn())
            {
                character.GearAttack(characterManager.GetOppositeSideCharacter(character));
            }
            else character.Attack(characterManager.GetOppositeSideCharacter(character));
        }
    }
}
