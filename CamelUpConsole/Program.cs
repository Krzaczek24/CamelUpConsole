using CamelUpConsole.Constants;
using CamelUpConsole.Core.MenuBar;
using CamelUpConsole.Core.Pages.ReadyPages;
using CamelUpConsole.Enums;
using CamelUpConsole.Mappings;
using CamelUpEngine;
using CamelUpEngine.Extensions;
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
        private static Game game = null;
        private static string singlePlayerName = null;

        static void Main(string[] args)
        {
            //TestAscii();
            //Console.ReadLine();
            //return;

            if (OperatingSystem.IsWindows())
            {
                const int width = 120;
                const int height = 30;
                Console.SetBufferSize(width, height);
                Console.SetWindowSize(width, height);
            }
            
            Console.Clear();
            Console.Title = "Camel Up";

            MainMenu();
        }

        private static void MainMenu()
        {
            MenuLevels menuLevel = MenuLevels.MainMenu;
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
                    MenuBar.Render(menuLevel);
                    MenuBar.PrintMessage("Hello my friend, welcome in Camel Up game, where sand is hot and camels are ready for ride. Let's go make some bets!");
                }

                renderPage = false;
                renderMenu = true;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case ConsoleKey.N:
                        NewGame();
                        menuLevel = game?.GameIsOver ?? true ? MenuLevels.MainMenu : MenuLevels.MainMenuWithGame;
                        break;
                    case ConsoleKey.A:
                        About();
                        renderPage = true;
                        break;
                    case ConsoleKey.C:
                        if (menuLevel == MenuLevels.MainMenuWithGame)
                        {
                            Gameplay();
                            menuLevel = game?.GameIsOver ?? true ? MenuLevels.MainMenu : MenuLevels.MainMenuWithGame;
                            break;
                        }
                        MenuBar.PrintNoSupportedKeyError(keyInfo);
                        renderMenu = false;
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
                    MenuBar.PrintMessage("How many computer players you want to encounter?");
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
                        singlePlayerName = string.Empty;
                        CreateGame(int.Parse(keyInfo.KeyChar.ToString()));
                        Gameplay();
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
                        singlePlayerName = null;
                        CreateGame(int.Parse(keyInfo.KeyChar.ToString()));
                        Gameplay();
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

        private static void CreateGame(int playersCount)
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
                        if (singlePlayerName == string.Empty)
                        {
                            MenuBar.Settings.SelectionText = $"Enter your name: {playerName}";
                        }
                        else
                        {
                            MenuBar.Settings.SelectionText = $"Enter {index + 1}. player name: {playerName}";
                        }
                        
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
                                MenuBar.PrintError("I am sorry, but passed player name is not valid. Please, try again.");
                                renderMenu = false;
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
                                return;
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

                if (singlePlayerName == string.Empty)
                {
                    players.AddRange(Collections.Names.GetRandom(playersCount));
                    singlePlayerName = players.First();
                    break;
                }
            }

            MenuBar.Settings.ResetSelectionText();
            game = new Game(players);
            return;
        }

        private static void Gameplay()
        {
            GameBoard gameBoard = new(game);
            renderPage = renderMenu = true;
            while (true)
            {
                if (renderPage)
                {
                    gameBoard.Render();
                }

                if (renderMenu)
                {
                    MenuBar.Render(MenuMapping.GetGameOptions(game), MenuLevels.GameActionChoose);
                    MenuBar.PrintMessage("Work in progress");
                }

                renderPage = true;
                renderMenu = true;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case ConsoleKey.D:
                        // DRAW DICE
                        game.DrawDice();
                        break;
                    case ConsoleKey.C:
                        if (!game.AvailableTypingCards.Any())
                        {
                            MenuBar.PrintNoSupportedKeyError(keyInfo);
                            renderPage = renderMenu = false;
                            break;
                        }
                        // DRAW TYPING CARD
                        game.DrawTypingCard(game.AvailableTypingCards.GetRandom());
                        break;
                    case ConsoleKey.A:
                        if (!game.AudienceTileAvailableFields.Any())
                        {
                            MenuBar.PrintNoSupportedKeyError(keyInfo);
                            renderPage = renderMenu = false;
                            break;
                        }
                        // PUT AUDIENCE TILE
                        game.PlaceAudienceTile(game.AudienceTileAvailableFields.GetRandom(), CamelUpEngine.Core.Enums.AudienceTileSide.Cheering);
                        break;
                    case ConsoleKey.B:
                        if (!game.AvailableBetCards.Any())
                        {
                            MenuBar.PrintNoSupportedKeyError(keyInfo);
                            renderPage = renderMenu = false;
                            break;
                        }
                        // MAKE BET
                        game.MakeBet(game.AvailableBetCards.GetRandom(), CamelUpEngine.Core.Enums.BetType.Winner);
                        break;
                    case MenuMapping.BackKey:
                        if (Confirm("Are you sure that you want to leave current game"))
                            return;
                        break;
                    case MenuMapping.QuitKey:
                        if (Confirm("Are you sure that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        MenuBar.PrintNoSupportedKeyError(keyInfo);
                        renderPage = renderMenu = false;
                        break;
                }
            }
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