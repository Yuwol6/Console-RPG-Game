using ConsoleRpg.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg
{
    public class CharacterManager
    {
        private List<Character> characters = new List<Character>();
        public int Stage { get; private set; } = 0;

        public CharacterManager()
        {

        }

        public void IncreaseStage()
        {
            Stage++;
        }

        public void AddCharacters(List<Character> characters)
        {
            this.characters.AddRange(characters);
        }

        public void RemoveCharacter(Character character)
        {
            this.characters.Remove(character);
        }

        public List<Character> GetCharacters()
        {
            return new List<Character>(characters);
        }

        public Character GetOppositeSideCharacter(Character originalCharacter)
        {
            foreach (Character character in this.characters)
            {
                if (character.Side != originalCharacter.Side) return character;
            }

            throw new InvalidOperationException("No character found on the opposite side. That's weird...");
        }

        public (bool ended, Side winningSide) CheckWinningSide()
        {
            int friendlyCount = 0;
            int enemyCount = 0;

            foreach (Character character in this.characters)
            {
                if (character.Side == Side.Friendly) friendlyCount++;
                else enemyCount++;
            }

            if (friendlyCount == 0)
            {
                return (true, Side.Enemy);
            }
            else if (enemyCount == 0)
            {
                return (true, Side.Friendly);
            }
            return (false, Side.Enemy);
        }

        public void ShowStatus(Side side)
        {
            foreach (Character character in this.characters)
            {
                if (character.Side == side)
                {
                    if (side == Side.Enemy)
                    {
                        Console.Write("                               ");
                    }
                    Console.WriteLine(character.GetStatus());
                }
            }
        }
    }
}
