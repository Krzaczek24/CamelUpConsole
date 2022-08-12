using CamelUpConsole.ConsoleHelper;
using CamelUpConsole.Constants;
using CamelUpConsole.Enums;
using CamelUpEngine.Extensions;
using System;

namespace CamelUpConsole
{
    internal class Program
    {
        private static ConsoleKeyInfo keyInfo;
        private static bool render;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.Title = "Camel Up";
            Intro.Render();

            MainMenu();
        }

        private static void MainMenu()
        {
            render = true;
            while (true)
            {
                if (render)
                {
                    MenuBar.Render(MenuLevels.MainMenu);
                    MenuBar.PrintMessage("Hello my friend, welcome in Camel Up game, where sand is hot and camels are ready for ride. Let's go make some bets!");
                    render = false;
                }

                render = true;
                switch ((keyInfo = Console.ReadKey()).Key)
                {
                    case ConsoleKey.N:
                        NewGame();
                        break;
                    case ConsoleKey.Q:
                        if (Confirm("Are you sure that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo.Key);
                        render = false;
                        break;
                }
            }
        }

        private static void NewGame()
        {
            render = true;
            while (true)
            {
                if (render)
                {
                    MenuBar.Render(MenuLevels.GameMode);
                    MenuBar.PrintMessage("Which game mode you are willing to play my friend?");
                    render = false;
                }

                render = true;
                switch ((keyInfo = Console.ReadKey()).Key)
                {
                    case ConsoleKey.S:
                        SinglePlayer();
                        break;
                    case ConsoleKey.M:
                        MultiPlayer();
                        break;
                    case ConsoleKey.B:
                        return;
                    case ConsoleKey.Q:
                        if (Confirm("Are you sure my friend that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo.Key);
                        render = false;
                        break;
                }
            }
        }

        private static void SinglePlayer()
        {
            render = true;
            while (true)
            {
                if (render)
                {
                    MenuBar.Render(MenuLevels.ComputerPlayersCount);
                    MenuBar.PrintMessage("How many computer players you want to encounter, my friend?");
                    render = false;
                }

                render = true;
                switch ((keyInfo = Console.ReadKey()).Key)
                {
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                        int number = int.Parse(keyInfo.KeyChar.ToString());
                        var names = Collections.Names.GetRandom(number);
                        MenuBar.PrintMessage(string.Join(", ", names));
                        render = false;
                        break;
                    case ConsoleKey.B:
                        return;
                    case ConsoleKey.Q:
                        if (Confirm("Are you sure that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo.Key);
                        render = false;
                        break;
                }
            }
        }

        private static void MultiPlayer()
        {
            render = true;
            while (true)
            {
                if (render)
                {
                    MenuBar.Render(MenuLevels.HumanPlayersCount);
                    MenuBar.PrintMessage("How big is your team, my friend?");
                    render = false;
                }

                render = true;
                switch ((keyInfo = Console.ReadKey()).Key)
                {
                    // TODO: implement multiplayer
                    case ConsoleKey.B:
                        return;
                    case ConsoleKey.Q:
                        if (Confirm("Are you sure that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo.Key);
                        render = false;
                        break;
                }
            }
        }

        private static bool Confirm(string message, ConsoleColor color = ConsoleColor.DarkYellow)
        {
            render = true;
            while (true)
            {
                if (render)
                {
                    MenuBar.Render(MenuLevels.Confirmation);
                    MenuBar.PrintMessage(message, color);
                    render = false;
                }

                render = true;
                switch ((keyInfo = Console.ReadKey()).Key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        return false;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo.Key);
                        render = false;
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