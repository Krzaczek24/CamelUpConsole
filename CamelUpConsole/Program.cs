using CamelUpConsole.ConsoleHelper;
using CamelUpConsole.Enums;
using CamelUpConsole.Mappings;
using System;

namespace CamelUpConsole
{
    internal class Program
    {
        private static ConsoleKey key;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.Title = "Camel Up";
            Intro.Render();

            MainMenu();
        }

        private static void MainMenu()
        {
            while (true)
            {
                MenuBar.Render(MenuMapping.LevelOptions[MenuLevels.MainMenu], 1);
                MenuBar.PrintMessage("Hello my friend, welcome in Camel Up game, where sand is hot and camels are ready for ride. Let's go make some bets!");

                switch (key = Console.ReadKey().Key)
                {
                    case ConsoleKey.N:
                        NewGame();
                        break;
                    case ConsoleKey.Q:
                        if (Confirm("Are you sure that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(key);                        
                        break;
                }
            }
        }

        private static void NewGame()
        {
            while (true)
            {
                MenuBar.Render(MenuMapping.LevelOptions[MenuLevels.GameMode], 2);
                MenuBar.PrintMessage("Please my friend, which game mode you are willing to play?");

                switch (key = Console.ReadKey().Key)
                {
                    case ConsoleKey.S:
                        
                        break;
                    case ConsoleKey.M:

                        break;
                    case ConsoleKey.B:
                        return;
                    case ConsoleKey.Q:
                        if (Confirm("Are you sure that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(key);
                        break;
                }
            }
        }

        private static bool Confirm(string message, ConsoleColor color = ConsoleColor.DarkYellow)
        {
            var options = MenuMapping.LevelOptions[MenuLevels.Confirmation];
            MenuBar.Render(options);
            MenuBar.PrintMessage(message, color);

            while (true)
            {
                switch (key = Console.ReadKey().Key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        return false;
                    default:
                        MenuBar.PrintNoSupportedKeyError(key);
                        break;
                }
            }
        }

        private static void TestAscii()
        {
            for (int c = 0; c < 256; c++)
            {
                Console.Write($"{c} = ");
                Console.WriteLine((char)c);
            }
        }

        private static void TestColor()
        {
            string str = "Test";
            var colors = Enum.GetValues<ConsoleColor>();
            int row = 0, col = 0;
            foreach (var colorA in colors)
            {
                foreach (var colorB in colors)
                {
                    Console.BackgroundColor = colorA;
                    Console.ForegroundColor = colorB;
                    Console.SetCursorPosition(col * (str.Length + 1), row);
                    Console.Write(str);

                    col++;
                }
                col = 0;
                row++;
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }
}