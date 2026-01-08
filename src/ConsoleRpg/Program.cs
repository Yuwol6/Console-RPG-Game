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

            GameSettings gameSettings = new GameSettings();
            Game game = new Game(gameSettings);

            game.RunGame();
        }
    }
}

