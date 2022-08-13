using CamelUpConsole.Constants;
using CamelUpConsole.Core.MenuBar;
using CamelUpConsole.Core.Pages.ReadyPages;
using CamelUpConsole.Enums;
using CamelUpConsole.Mappings;
using CamelUpEngine.Extensions;
using System;

namespace CamelUpConsole
{
    internal class Program
    {
        private static ConsoleKeyInfo keyInfo;
        private static bool renderPage;
        private static bool renderMenu;

        static void Main(string[] args)
        {
            if (OperatingSystem.IsWindows())
            {
                Console.SetBufferSize(120, 30);
                Console.SetWindowSize(120, 30);
            }
            
            Console.Clear();
            Console.Title = "Camel Up";

            MainMenu();
        }

        private static void MainMenu()
        {
            Intro intro = new();
            renderPage = renderMenu = true;
            while (true)
            {
                if (renderPage)
                {
                    intro.Render();
                }

                if (renderMenu)
                {
                    MenuBar.Render(MenuLevels.MainMenu);
                    MenuBar.PrintMessage("Hello my friend, welcome in Camel Up game, where sand is hot and camels are ready for ride. Let's go make some bets!");
                }

                renderPage = false;
                renderMenu = true;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case ConsoleKey.N:
                        NewGame();
                        break;
                    case ConsoleKey.A:
                        About();
                        renderPage = true;
                        break;
                    case MenuMapping.BackKey:
                    case MenuMapping.QuitKey:
                        if (Confirm("Are you sure that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo);
                        renderMenu = false;
                        break;
                }
            }
        }

        private static void NewGame()
        {
            renderMenu = true;
            while (true)
            {
                if (renderMenu)
                {
                    MenuBar.Render(MenuLevels.GameMode);
                    MenuBar.PrintMessage("Which game mode you are willing to play my friend?");
                }

                renderMenu = true;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case ConsoleKey.S:
                        SinglePlayer();
                        break;
                    case ConsoleKey.M:
                        MultiPlayer();
                        break;
                    case MenuMapping.BackKey:
                        return;
                    case MenuMapping.QuitKey:
                        if (Confirm("Are you sure my friend that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo);
                        renderMenu = false;
                        break;
                }
            }
        }

        private static void SinglePlayer()
        {
            renderMenu = true;
            while (true)
            {
                if (renderMenu)
                {
                    MenuBar.Render(MenuLevels.ComputerPlayersCount);
                    MenuBar.PrintMessage("How many computer players you want to encounter, my friend?");
                }

                renderMenu = true;
                switch ((keyInfo = Console.ReadKey(true)).Key)
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
                        renderMenu = false;
                        break;
                    case MenuMapping.BackKey:
                        return;
                    case MenuMapping.QuitKey:
                        if (Confirm("Are you sure that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo);
                        renderMenu = false;
                        break;
                }
            }
        }

        private static void MultiPlayer()
        {
            renderMenu = true;
            while (true)
            {
                if (renderMenu)
                {
                    MenuBar.Render(MenuLevels.HumanPlayersCount);
                    MenuBar.PrintMessage("How big is your team, my friend?");
                }

                renderMenu = true;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    // TODO: implement multiplayer
                    case MenuMapping.BackKey:
                        return;
                    case MenuMapping.QuitKey:
                        if (Confirm("Are you sure that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo);
                        renderMenu = false;
                        break;
                }
            }
        }

        private static bool Confirm(string message, ConsoleColor color = ConsoleColor.DarkYellow)
        {
            renderMenu = true;
            while (true)
            {
                if (renderMenu)
                {
                    MenuBar.Render(MenuLevels.Confirmation);
                    MenuBar.PrintMessage(message, color);                    
                }

                renderMenu = true;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case MenuMapping.ConfirmKey:
                    case ConsoleKey.Y:
                        return true;
                    case MenuMapping.BackKey:
                    case ConsoleKey.N:
                        return false;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo);
                        renderMenu = false;
                        break;
                }
            }
        }

        private static void About()
        {
            AboutGame aboutGame = new AboutGame();
            renderPage = renderMenu = true;
            while (true)
            {
                if (renderPage)
                {
                    aboutGame.Render();
                }

                if (renderMenu)
                {
                    MenuBar.Render(aboutGame);
                    MenuBar.PrintMessage("My dear friend, I am so pleased for that you came here to check this information.");
                }

                renderPage = true;
                renderMenu = true;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case ConsoleKey.UpArrow:
                        aboutGame.ScrollUp();
                        break;
                    case ConsoleKey.DownArrow:
                        aboutGame.ScrollDown();
                        break;
                    case ConsoleKey.PageUp:
                        aboutGame.PageUp();
                        break;
                    case ConsoleKey.PageDown:
                        aboutGame.PageDown();
                        break;
                    case MenuMapping.BackKey:
                        return;
                    case MenuMapping.QuitKey:
                        if (Confirm("Are you sure that you want to exit game?"))
                            Environment.Exit(0);
                        renderPage = false;
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo);
                        renderPage = renderMenu = false;
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