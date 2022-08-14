using CamelUpConsole.Core.Actions;
using CamelUpConsole.Core.MenuBar;
using CamelUpConsole.Core.Pages.ReadyPages;
using CamelUpConsole.Enums;
using CamelUpConsole.Mappings;
using CamelUpEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CamelUpConsole
{
    internal class Program
    {
        private static ConsoleKeyInfo keyInfo;
        private static bool renderPage;
        private static bool renderMenu;

        static void Main(string[] args)
        {
            //TestAscii();
            //Console.ReadLine();
            //return;

            MenuBar.Settings.Margin = 1;

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
                        return;
                    case ConsoleKey.M:
                        MultiPlayer();
                        return;
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
                        // TODO: implement singleplayer
                        //CreatingGame(int.Parse(keyInfo.KeyChar.ToString()));
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
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                        var game = CreatingGame(int.Parse(keyInfo.KeyChar.ToString()));
                        // TODO: continue here
                        return;
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

        private static Game CreatingGame(int playersCount)
        {
            const int nameMaxLength = 12;
            List<string> players = new();
            for (int index = 0; index < playersCount; index++)
            {
                bool commitedPlayerName = false;
                string playerName = string.Empty;
                renderMenu = true;
                while (!commitedPlayerName)
                {
                    if (renderMenu)
                    {
                        MenuBar.Settings.SelectionText = $"Enter {index + 1}. player name: {playerName}";
                        if (players.Any())
                        {
                            MenuBar.Render(MenuLevels.AddedSomePlayer);
                            MenuBar.PrintMessage($"Already passed player names: {string.Join(", ", players)}.");
                        }
                        else
                        {
                            MenuBar.Render(MenuLevels.BackOrQuit);
                            MenuBar.PrintMessage("I am so glad, you are willing to get part in our competition.");
                        }
                    }

                    renderMenu = true;
                    switch ((keyInfo = Console.ReadKey(true)).Key)
                    {
                        case ConsoleKey.Enter:
                            if (Game.IsPlayerNameValid(playerName))
                            {
                                players.Add(playerName);
                                commitedPlayerName = true;
                            }
                            else
                            {
                                playerName = string.Empty;
                            }
                            break;
                        case ConsoleKey.Backspace:
                            if (playerName.Length > 0)
                                playerName = new string(playerName.SkipLast(1).ToArray());
                            break;
                        case ConsoleKey.Delete:
                            if (players.Any() && Confirm($"Are you sure that you want to remove player {players.Last()}?"))
                                players.RemoveAt(--index);
                            break;
                        case MenuMapping.BackKey:
                            MenuBar.Settings.ResetSelectionText();
                            if (!players.Any() || Confirm("Are you sure that you want to go back? You will lost all already inserted names."))
                                return null;
                            break;
                        case MenuMapping.QuitKey:
                            if (Confirm("Are you sure that you want to exit game?"))
                                Environment.Exit(0);
                            break;
                        default:
                            if (!char.IsLetter(keyInfo.KeyChar))
                            {
                                MenuBar.PrintError("I am sorry, for player names please use only letters, without digits, spaces or special characters.");
                                renderMenu = false;
                                break;
                            }
                            if (playerName.Length > nameMaxLength)
                            {
                                MenuBar.PrintError($"I am so sorry, but player name cannot be longer than {nameMaxLength} characters.");
                                renderMenu = false;
                                break;
                            }
                            playerName += keyInfo.KeyChar;
                            break;
                    }
                }
            }

            MenuBar.Settings.ResetSelectionText();
            return new Game(players);
        }

        private static bool Confirm(string message, ConsoleColor color = ConsoleColor.DarkYellow)
        {
            string oldSelectionText = MenuBar.Settings.SelectionText;
            MenuBar.Settings.ResetSelectionText();

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
                        MenuBar.Settings.SelectionText = oldSelectionText;
                        return true;
                    case MenuMapping.BackKey:
                    case ConsoleKey.N:
                        MenuBar.Settings.SelectionText = oldSelectionText;
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
            for (int c = 0; c < 1024; c++)
            {
                try
                {
                    Console.Write($"{c} = ");
                    Console.WriteLine((char)c);
                }
                catch { }
            }
        }
    }
}