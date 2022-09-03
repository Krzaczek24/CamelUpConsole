using CamelUpConsole.Core.MenuBar.Models;
using CamelUpConsole.Enums;
using CamelUpEngine;
using CamelUpEngine.GameObjects.Available;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CamelUpConsole.Mappings
{
    internal static class MenuMapping
    {
        public const ConsoleKey ConfirmKey = ConsoleKey.Enter;
        public const ConsoleKey BackKey = ConsoleKey.Escape;
        public const ConsoleKey QuitKey = ConsoleKey.F4;

        private static Option ScrollHistory = new Option("↑↓", " Scroll history ");
        private static Option GoBack => new Option(BackKey.ToString().Substring(0, 3), " Go back ");
        private static Option LeaveGame => new Option(BackKey.ToString().Substring(0, 3), " Leave game ");
        private static Option ExitApp => new Option(QuitKey.ToString(), " Quit app ");

        public static Options GetGameOptions(Game game, DynamicOption historyProgress = null)
        {
            Options options;

            if (game.GameIsOver)
            {
                options = new(ScrollHistory, GoBack, ExitApp);
                if (historyProgress != null)
                    options = options.Insert(1, historyProgress);
                return options;
            }

            if (game.TurnIsOver)
            {
                options = new("N Go to next turn ", ScrollHistory, GoBack, ExitApp);
                if (historyProgress != null)
                    options = options.Insert(2, historyProgress);
                return options;
            }

            options = new("D Draw dice ", ScrollHistory, LeaveGame, ExitApp);
            if (historyProgress != null)
                options = options.Insert(2, historyProgress);
            if (game.AvailableBetCards.Any())
                options = options.Insert(1, new("B Make bet "));
            if (game.AudienceTileAvailableFields.Any())
                options = options.Insert(1, new("A Put audience tile "));
            if (game.AvailableTypingCards.Any())
                options = options.Insert(1, new("C Draw card "));
            return options;
        }

        public static Options GetAvailableTypingCardOptions(Game game, DynamicOption historyProgress = null)
        {
            Options options = new(ScrollHistory, GoBack, ExitApp);
            if (historyProgress != null)
                options = options.Insert(1, historyProgress);
            IList<IAvailableTypingCard> availableCards = game.AvailableTypingCards.Reverse().ToList();
            foreach (IAvailableTypingCard card in availableCards)
                options = options.Insert(0, new Option($"{card.Colour.ToString().First()} {card.Colour} ({(int)card.Value}) "));
            return options;
        }

        public static Options GetAvailableAudienceFieldsOptions(Game game, DynamicOption historyProgress = null)
        {
            Options options = new(ScrollHistory, GoBack, ExitApp);
            if (historyProgress != null)
                options = options.Insert(1, historyProgress);
            //IList<IAvailableBetCard> availableBetCards = game.AvailableBetCards.Reverse().ToList();
            //foreach (IAvailableBetCard betCard in availableBetCards)
            //    options = options.Insert(0, new Option($"{betCard.Colour.ToString().First()} {betCard.Colour} "));
            return options;
        }

        public static Options GetAudienceSidesOptions(DynamicOption historyProgress = null)
        {
            Options options = new("C Cheering ", "B Booing ", ScrollHistory, GoBack, ExitApp);
            if (historyProgress != null)
                options = options.Insert(3, historyProgress);
            return options;
        }

        public static Options GetAvailableBetsOptions(Game game, DynamicOption historyProgress = null)
        {
            Options options = new(ScrollHistory, GoBack, ExitApp);
            if (historyProgress != null)
                options = options.Insert(1, historyProgress);
            IList<IAvailableBetCard> availableBetCards = game.AvailableBetCards.Reverse().ToList();
            foreach (IAvailableBetCard betCard in availableBetCards)
                options = options.Insert(0, new Option($"{betCard.Colour.ToString().First()} {betCard.Colour} "));
            return options;
        }

        public static Options GetBetsTypesOptions(DynamicOption historyProgress = null)
        {
            Options options = new("W Winner ", "L Loser ", ScrollHistory, GoBack, ExitApp);
            if (historyProgress != null)
                options = options.Insert(3, historyProgress);
            return options;
        }

        public static IReadOnlyDictionary<MenuLevels, Options> LevelOptions { get; } = new Dictionary<MenuLevels, Options>()
        {
            [MenuLevels.BackOrQuit] = new Options(GoBack, ExitApp),
            [MenuLevels.ComputerPlayersCount] = new("2 Two ", "3 Three ", "4 Four ", "5 Five ", "6 Six ", "7 Seven ", GoBack, ExitApp),
            [MenuLevels.Confirmation] = new(new Option("Y|Enter", " Yes "), new Option("N|Esc", " No ")),
            [MenuLevels.AddedSomePlayer] = new(new Option("Del", " Remove last player "), GoBack, ExitApp),
            [MenuLevels.GameMode] = new("S Singleplayer ", "M Multiplayer ", GoBack, ExitApp),
            [MenuLevels.HumanPlayersCount] = new("3 Three ", "4 Four ", "5 Five ", "6 Six ", "7 Seven ", "8 Eight ", GoBack, ExitApp),
            [MenuLevels.MainMenu] = new("N New game ", "A About game ", ExitApp),
            [MenuLevels.MainMenuWithGame] = new("N New game ", "C Continue game ", "A About game ", ExitApp),
            [MenuLevels.Scrollable] = new("↑ Scroll up ", "↓ Scroll down ", new Option("PgUp", " Page up "), new Option("PgDn", " Page down "), GoBack, ExitApp)
        };

        public static IReadOnlyDictionary<MenuLevels, int> LevelOptionsAlignToRight { get; } = new Dictionary<MenuLevels, int>()
        {
            [MenuLevels.AddedSomePlayer] = -1,
            [MenuLevels.BackOrQuit] = -1,
            [MenuLevels.ComputerPlayersCount] = 2,
            [MenuLevels.Confirmation] = 0,
            [MenuLevels.GameActionChoose] = 4,
            [MenuLevels.GameMode] = 2,
            [MenuLevels.HumanPlayersCount] = 2,
            [MenuLevels.MainMenu] = 2,
            [MenuLevels.MainMenuWithGame] = 2,
            [MenuLevels.Scrollable] = 2
        };
    }
}
