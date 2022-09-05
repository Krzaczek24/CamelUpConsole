using CamelUpConsole.Constants;
using CamelUpConsole.Core.MenuBar;
using CamelUpConsole.Core.MenuBar.Models;
using CamelUpConsole.Core.Pages;
using CamelUpConsole.Core.Pages.ReadyPages;
using CamelUpConsole.Enums;
using CamelUpConsole.Mappings;
using CamelUpEngine;
using CamelUpEngine.Core.Actions;
using CamelUpEngine.Core.Actions.Events;
using CamelUpEngine.Core.Enums;
using CamelUpEngine.Extensions;
using CamelUpEngine.GameObjects;
using CamelUpEngine.GameObjects.Available;
using CamelUpEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

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
                        menuLevel = game == null ? MenuLevels.MainMenu : MenuLevels.MainMenuWithGame;
                        break;
                    case ConsoleKey.A:
                        About();
                        renderPage = true;
                        break;
                    case ConsoleKey.C:
                        if (menuLevel == MenuLevels.MainMenuWithGame)
                        {
                            Gameplay();
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
                                if (players.Select(p => p.ToUpper()).Contains(playerName.ToUpper()))
                                {
                                    MenuBar.PrintError("I am sorry, but you already added such player name.");
                                    renderMenu = false;
                                    break;
                                }
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
                            if (playerName.Length >= nameMaxLength)
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
            bool renderHistory = false;
            while (true)
            {
                while (!string.IsNullOrEmpty(singlePlayerName) && singlePlayerName != game.CurrentPlayer.Name && !game.TurnIsOver && !game.GameIsOver)
                {
                    GetAvailableActions().GetRandom()();
                    gameBoard.History.Reset();
                }

                if (renderPage)
                {
                    gameBoard.Render();
                }
                else if (renderHistory)
                {
                    gameBoard.History.Render();
                    MenuBar.SetCursorInOptionSelect();
                }

                if (renderMenu)
                {
                    DynamicOption option = new(string.Empty, MenuBar.GetProgressFunction(gameBoard.History));
                    MenuBar.Render(MenuMapping.GetGameOptions(game, option), MenuLevels.GameActionChoose);
                    MenuBar.PrintMessage("Please, choose one from available actions");
                }

                renderPage = true;
                renderMenu = true;
                renderHistory = false;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case ConsoleKey.D:
                        if (game.TurnIsOver || game.GameIsOver)
                        {
                            MenuBar.PrintNoSupportedKeyError(keyInfo);
                            renderPage = renderMenu = false;
                            break;
                        }
                        game.DrawDice();
                        gameBoard.History.Reset();
                        break;
                    case ConsoleKey.C:
                        if (!game.AvailableTypingCards.Any() || game.TurnIsOver || game.GameIsOver)
                        {
                            MenuBar.PrintNoSupportedKeyError(keyInfo);
                            renderPage = renderMenu = false;
                            break;
                        }
                        DrawCard(gameBoard);
                        gameBoard.History.Reset();
                        break;
                    case ConsoleKey.A:
                        if (!game.AudienceTileAvailableFields.Any() || game.TurnIsOver || game.GameIsOver)
                        {
                            MenuBar.PrintNoSupportedKeyError(keyInfo);
                            renderPage = renderMenu = false;
                            break;
                        }
                        PlaceAudienceTile(gameBoard);
                        gameBoard.History.Reset();
                        break;
                    case ConsoleKey.B:
                        if (!game.AvailableBetCards.Any() || game.TurnIsOver || game.GameIsOver)
                        {
                            MenuBar.PrintNoSupportedKeyError(keyInfo);
                            renderPage = renderMenu = false;
                            break;
                        }
                        MakeBet(gameBoard);
                        gameBoard.History.Reset();
                        break;
                    case ConsoleKey.N:
                        if (!game.TurnIsOver || game.GameIsOver)
                        {
                            MenuBar.PrintNoSupportedKeyError(keyInfo);
                            renderPage = renderMenu = false;
                            break;
                        }
                        game.GoToNextTurn();
                        gameBoard.History.Reset();
                        break;
                    case ConsoleKey.UpArrow:
                        gameBoard.History.ScrollUp();
                        renderPage = false;
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.DownArrow:
                        gameBoard.History.ScrollDown();
                        renderPage = false;
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageUp:
                        gameBoard.History.PageUp();
                        renderPage = false;
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageDown:
                        gameBoard.History.PageDown();
                        renderPage = false;
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.Spacebar:
                        gameBoard.History.Reset();
                        renderPage = false;
                        renderMenu = renderHistory = true;
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

        private static void DrawCard(GameBoard gameBoard)
        {
            renderMenu = true;
            bool renderHistory = false;
            while (true)
            {
                if (renderHistory)
                {
                    gameBoard.History.Render();
                    MenuBar.SetCursorInOptionSelect();
                }

                if (renderMenu)
                {
                    DynamicOption option = new(string.Empty, MenuBar.GetProgressFunction(gameBoard.History));
                    MenuBar.Render(MenuMapping.GetAvailableTypingCardOptions(game, option), MenuLevels.GameActionChoose);
                    MenuBar.PrintMessage("Please, choose typing card that you are willing to draw");
                }

                renderMenu = true;
                renderHistory = false;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case ConsoleKey.R:
                    case ConsoleKey.Y:
                    case ConsoleKey.G:
                    case ConsoleKey.B:
                    case ConsoleKey.V:
                        IAvailableTypingCard card = game.AvailableTypingCards.SingleOrDefault(card => card.Colour.ToString().First() == char.ToUpper(keyInfo.KeyChar));
                        if (card == null)
                        {
                            MenuBar.PrintNoSupportedKeyError(keyInfo);
                            renderMenu = false;
                            break;
                        }
                        game.DrawTypingCard(card);
                        return;
                    case ConsoleKey.UpArrow:
                        gameBoard.History.ScrollUp();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.DownArrow:
                        gameBoard.History.ScrollDown();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageUp:
                        gameBoard.History.PageUp();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageDown:
                        gameBoard.History.PageDown();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.Spacebar:
                        gameBoard.History.Reset();                        
                        renderMenu = renderHistory = true;
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

        private static void PlaceAudienceTile(GameBoard gameBoard)
        {
            string fieldIndex = string.Empty;
            renderMenu = true;
            bool renderHistory = false;
            while (true)
            {
                if (renderHistory)
                {
                    gameBoard.History.Render();
                    MenuBar.SetCursorInOptionSelect();
                }

                if (renderMenu)
                {
                    MenuBar.Settings.SelectionText = $"Enter selected field index: {fieldIndex}";
                    DynamicOption option = new(string.Empty, MenuBar.GetProgressFunction(gameBoard.History));
                    MenuBar.Render(MenuMapping.GetAvailableAudienceFieldsOptions(option), MenuLevels.GameActionChoose);
                    MenuBar.PrintMessage($"Please, type field index to place audience tile ({string.Join(", ", game.AudienceTileAvailableFields.Select(field => field.Index))})");
                }

                renderMenu = true;
                renderHistory = false;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case ConsoleKey.Enter:
                        IAvailableField field = null;
                        if (!int.TryParse(fieldIndex, out int index))
                        {
                            MenuBar.PrintError($"Please type one of following field indexes ({string.Join(", ", game.AudienceTileAvailableFields.Select(field => field.Index))})");
                            renderMenu = false;
                            break;
                        }
                        field = game.AudienceTileAvailableFields.SingleOrDefault(field => field.Index == index);
                        if (field == null)
                        {
                            MenuBar.PrintError($"There is no available field with index {fieldIndex}, please type one of these ({string.Join(", ", game.AudienceTileAvailableFields.Select(field => field.Index))})");
                            renderMenu = false;
                            break;
                        }
                        MenuBar.Settings.ResetSelectionText();
                        ChooseAudienceTileSide(gameBoard, field);
                        return;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.OemMinus:
                        if (!int.TryParse(fieldIndex, out index))
                        {
                            fieldIndex = game.AudienceTileAvailableFields.Last().Index.ToString();
                            break;
                        }
                        do
                            index = (--index + game.Fields.Count() - 1) % game.Fields.Count() + 1;
                        while (!game.AudienceTileAvailableFields.Select(field => field.Index).Contains(index));
                        fieldIndex = index.ToString();
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.OemPlus:
                        if (!int.TryParse(fieldIndex, out index))
                        {
                            fieldIndex = game.AudienceTileAvailableFields.First().Index.ToString();
                            break;
                        }
                        do
                            index = (++index + game.Fields.Count() - 1) % game.Fields.Count() + 1;
                        while (!game.AudienceTileAvailableFields.Select(field => field.Index).Contains(index));
                        fieldIndex = index.ToString();
                        break;
                    case ConsoleKey.Backspace:
                        if (fieldIndex.Length > 0)
                            fieldIndex = new string(fieldIndex.SkipLast(1).ToArray());
                        break;
                    case ConsoleKey.UpArrow:
                        gameBoard.History.ScrollUp();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.DownArrow:
                        gameBoard.History.ScrollDown();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageUp:
                        gameBoard.History.PageUp();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageDown:
                        gameBoard.History.PageDown();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.Spacebar:
                        gameBoard.History.Reset();
                        renderMenu = renderHistory = true;
                        break;
                    case MenuMapping.BackKey:
                        MenuBar.Settings.ResetSelectionText();
                        return;
                    case MenuMapping.QuitKey:
                        if (Confirm("Are you sure that you want to exit game?"))
                            Environment.Exit(0);
                        break;
                    default:
                        if (!char.IsDigit(keyInfo.KeyChar))
                        {
                            MenuBar.PrintError("I am sorry, for field index please use only digits.");
                            renderMenu = false;
                            break;
                        }
                        if (fieldIndex.Length >= game.Fields.Count.ToString().Length)
                        {
                            MenuBar.PrintError($"I am so sorry, but field index cannot be longer than {game.Fields.Count.ToString().Length} digits.");
                            renderMenu = false;
                            break;
                        }
                        fieldIndex += keyInfo.KeyChar;
                        break;
                }
            }
        }
        
        private static void ChooseAudienceTileSide(GameBoard gameBoard, IAvailableField availableField)
        {
            renderMenu = true;
            bool renderHistory = false;
            while (true)
            {
                if (renderHistory)
                {
                    gameBoard.History.Render();
                    MenuBar.SetCursorInOptionSelect();
                }

                if (renderMenu)
                {
                    DynamicOption option = new(string.Empty, MenuBar.GetProgressFunction(gameBoard.History));
                    MenuBar.Render(MenuMapping.GetAudienceSidesOptions(option), MenuLevels.GameActionChoose);
                    MenuBar.PrintMessage($"Please, choose audience tile side that you want to place on {availableField.Index}. field.");
                }

                renderMenu = true;
                renderHistory = false;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case ConsoleKey.C:
                        game.PlaceAudienceTile(availableField, AudienceTileSide.Cheering);
                        return;
                    case ConsoleKey.B:
                        game.PlaceAudienceTile(availableField, AudienceTileSide.Booing);
                        return;
                    case ConsoleKey.UpArrow:
                        gameBoard.History.ScrollUp();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.DownArrow:
                        gameBoard.History.ScrollDown();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageUp:
                        gameBoard.History.PageUp();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageDown:
                        gameBoard.History.PageDown();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.Spacebar:
                        gameBoard.History.Reset();
                        renderMenu = renderHistory = true;
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

        private static void MakeBet(GameBoard gameBoard)
        {
            renderMenu = true;
            bool renderHistory = false;
            while (true)
            {
                if (renderHistory)
                {
                    gameBoard.History.Render();
                    MenuBar.SetCursorInOptionSelect();
                }

                if (renderMenu)
                {
                    DynamicOption option = new(string.Empty, MenuBar.GetProgressFunction(gameBoard.History));
                    MenuBar.Render(MenuMapping.GetAvailableBetsOptions(game, option), MenuLevels.GameActionChoose);
                    MenuBar.PrintMessage("Please, choose camel color that you are willing to bet");
                }

                renderMenu = true;
                renderHistory = false;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case ConsoleKey.R:
                    case ConsoleKey.Y:
                    case ConsoleKey.G:
                    case ConsoleKey.B:
                    case ConsoleKey.V:
                        IAvailableBetCard card = game.AvailableBetCards.SingleOrDefault(card => card.Colour.ToString().First() == char.ToUpper(keyInfo.KeyChar));
                        if (card == null)
                        {
                            MenuBar.PrintNoSupportedKeyError(keyInfo);
                            renderMenu = false;
                            break;
                        }
                        ChooseBetType(gameBoard, card);
                        return;
                    case ConsoleKey.UpArrow:
                        gameBoard.History.ScrollUp();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.DownArrow:
                        gameBoard.History.ScrollDown();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageUp:
                        gameBoard.History.PageUp();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageDown:
                        gameBoard.History.PageDown();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.Spacebar:
                        gameBoard.History.Reset();
                        renderMenu = renderHistory = true;
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

        private static void ChooseBetType(GameBoard gameBoard, IAvailableBetCard betCard)
        {
            renderMenu = true;
            bool renderHistory = false;
            while (true)
            {
                if (renderHistory)
                {
                    gameBoard.History.Render();
                    MenuBar.SetCursorInOptionSelect();
                }

                if (renderMenu)
                {
                    DynamicOption option = new(string.Empty, MenuBar.GetProgressFunction(gameBoard.History));
                    MenuBar.Render(MenuMapping.GetBetsTypesOptions(option), MenuLevels.GameActionChoose);
                    MenuBar.PrintMessage("Please, choose bet type");
                }

                renderMenu = true;
                renderHistory = false;
                switch ((keyInfo = Console.ReadKey(true)).Key)
                {
                    case ConsoleKey.W:
                        game.MakeBet(betCard, BetType.Winner);
                        return;
                    case ConsoleKey.L:
                        game.MakeBet(betCard, BetType.Loser);
                        return;
                    case ConsoleKey.UpArrow:
                        gameBoard.History.ScrollUp();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.DownArrow:
                        gameBoard.History.ScrollDown();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageUp:
                        gameBoard.History.PageUp();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.PageDown:
                        gameBoard.History.PageDown();
                        renderMenu = renderHistory = true;
                        break;
                    case ConsoleKey.Spacebar:
                        gameBoard.History.Reset();
                        renderMenu = renderHistory = true;
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
                    DynamicOption option = new("Line", MenuBar.GetProgressFunction(aboutGame));
                    MenuBar.Render(MenuMapping.LevelOptions[MenuLevels.Scrollable].Insert(4, option), MenuMapping.LevelOptionsAlignToRight[MenuLevels.Scrollable]);
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

        private static IReadOnlyCollection<Func<ActionEvents>> GetAvailableActions()
        {
            List<Func<ActionEvents>> availableActions = new() { AutoDrawDice };

            if (game.AvailableTypingCards.Any())
                availableActions.Add(AutoDrawTypingCard);

            if (game.AudienceTileAvailableFields.Any())
                availableActions.Add(AutoPlaceAudienceTile);

            if (game.AvailableBetCards.Any())
                availableActions.Add(AutoMakeBet);

            return availableActions;
        }

        private static ActionEvents AutoDrawDice() => game.DrawDice();
        private static ActionEvents AutoDrawTypingCard()
        {
            IList<Colour> orderedCamelColours = game.Camels.Where(camel => !camel.IsMad).GetColours().ToList();
            IList<Colour> availableCardColours = game.AvailableTypingCards.GetColours().ToList();
            orderedCamelColours = orderedCamelColours.Intersect(availableCardColours).ToList();
            Colour selectedColour;

            if (Random.Shared.Next(3) == 0) // 33% for getting card for first camel available typing card
                selectedColour = orderedCamelColours.First();
            else
            {
                IList<Colour> colorsToSelect = new List<Colour>();
                int max = 1;
                int absoluteMax = (int)Math.Ceiling(orderedCamelColours.Count * 0.75);
                for (int index = orderedCamelColours.Count - 1; index >= 0; index--)
                {
                    for (int i = 1; i <= max; i++)
                        colorsToSelect.Add(orderedCamelColours[index]);
                    max = Math.Min(++max, absoluteMax);
                }
                selectedColour = colorsToSelect.GetRandom();
            }

            return game.DrawTypingCard(game.AvailableTypingCards.Single(card => card.Colour == selectedColour));
        }
        private static ActionEvents AutoPlaceAudienceTile() => game.PlaceAudienceTile(game.AudienceTileAvailableFields.GetRandom(), Enum.GetValues<AudienceTileSide>().GetRandom());
        private static ActionEvents AutoMakeBet() => game.MakeBet(game.AvailableBetCards.GetRandom(), Enum.GetValues<BetType>().GetRandom());
    }
}