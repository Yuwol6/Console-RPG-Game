using ConsoleRpg.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg
{
    /// <summary>
    /// Provides access to the active characters in the game.
    /// </summary>
    public class CharacterManager
    {
        private List<Character> activeCharacters = new List<Character>();

        public CharacterManager() {}

        /// <summary>
        /// Adds characters to the active character list.
        /// </summary>
        /// <param name="characters">The characters to add.</param>
        public void AddCharacters(List<Character> characters)
        {
            this.activeCharacters.AddRange(characters);
        }

        /// <summary>
        /// Adds characters to the front of the active character list.
        /// </summary>
        /// <param name="characters">The characters to add.</param>
        public void AddCharactersInFront(List<Character> characters)
        {
            foreach (Character character in characters)
            {
                this.activeCharacters.Insert(0, character);
            }
        }

        /// <summary>
        /// Removes character from the active character list.
        /// </summary>
        /// <param name="character">The character to be removed.</param>
        /// <remarks>The character specified must be in the active character list.</remarks>
        public void RemoveDeadCharacter(Character character)
        {
            if (character.markedForRemoval) this.activeCharacters.Remove(character);
        }

        /// <summary>
        /// Gets the active character list.
        /// </summary>
        /// <returns>A copy of the list of active characters in the game.</returns>
        public List<Character> GetCharacters()
        {
            return new List<Character>(activeCharacters);
        }

        /// <summary>
        /// Gets the first active character on the opposing side of the specified character.
        /// </summary>
        /// <param name="originalCharacter">The specified character to compare sides against.</param>
        /// <returns>The active character whose side differs from <paramref name="originalCharacter"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no opposing character exists in the active character list.</exception>
        public Character GetOppositeSideCharacter(Character originalCharacter)
        {
            foreach (Character character in this.activeCharacters)
            {
                if (character.Side != originalCharacter.Side) return character;
            }

            throw new InvalidOperationException("No active character exists on the opposing side.");
        }

        /// <summary>
        /// Determines whether the stage has ended due to one side having no active characters.
        /// </summary>
        /// <returns>
        /// A tuple containing a value indicating whether the game has ended,
        /// and the winning side if the game has ended.
        /// </returns>
        /// <remarks>The winningSide must not be used if the stage has not ended.</remarks>
        public (bool ended, Side winningSide) CheckWinningSide()
        {
            int friendlyCount = 0;
            int enemyCount = 0;

            foreach (Character character in this.activeCharacters)
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

        /// <summary>
        /// Writes the status of all active characters on the specified side to the console.
        /// </summary>
        /// <param name="side">The side whose characters' statuses are displayed.</param>
        public void ShowStatus(Side side)
        {
            for (int i = this.activeCharacters.Count - 1; i >= 0; i--)
            {
                if (this.activeCharacters[i].Side == side)
                {
                    if (side == Side.Enemy)
                    {
                        Console.Write("                                                   ");
                    }
                    Console.WriteLine(this.activeCharacters[i].GetStatus());
                }
            }
        }

        /// <summary>
        /// Levels up the hero in the active character list.
        /// </summary>
        public void LevelUpHero()
        {
            foreach (Character character in this.activeCharacters)
            {
                if (character is Hero hero) hero.LevelUp();
            }
        }
    }
}
