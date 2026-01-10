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
    /// Base class for all party controllers in the game.
    /// </summary>
    public class PartyController
    {
        protected Side sideToControl;
        public PartyController(Side side)
        {
            sideToControl = side;
        }

        /// <summary>
        /// Performs a turn for a character.
        /// </summary>
        /// <param name="characterManager">Provides access to the active characters in the game.</param>
        /// <param name="character">The character taking the turn.</param>
        /// <param name="activeParty">The party of the character.</param>
        public virtual void TakeTurn(CharacterManager characterManager, Character character, Party activeParty) {}

        /// <summary>
        /// Determines whether the controlled character and another character are on the same side.
        /// </summary>
        /// <param name="character">The other referenced character.</param>
        /// <returns>True if the two characters are on the same side; otherwise, false.</returns>
        public bool IsSameSide(Character character)
        {
            return character.Side == sideToControl;
        }
    }
}
