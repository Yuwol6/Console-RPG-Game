using ConsoleRpg.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Controllers
{
    public class PartyController
    {
        protected Side sideToControl;
        public PartyController(Side side)
        {
            sideToControl = side;
        }

        public virtual void TakeTurn(CharacterManager characterManager, Character character, Party activeParty)
        {

        }

        public bool IsSameSide(Character character)
        {
            return character.Side == sideToControl;
        }
    }
}
