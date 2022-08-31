using CamelUpConsole.Core.MenuBar.Models;
using CamelUpConsole.Enums;
using CamelUpEngine;
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

        private static Option GoBack => new Option(BackKey.ToString().Substring(0, 3), " Go back ");
        private static Option ExitGame => new Option(QuitKey.ToString(), " Quit game ");

        public static Options GetGameOptions(Game game) => GetGameOptions(game.AvailableTypingCards.Any(), game.AudienceTileAvailableFields.Any(), game.AvailableBetCards.Any());
        public static Options GetGameOptions(bool typingCardAvailalbe, bool audienceTileAvailable, bool betAvailable)
        {
            Options options = new("D Draw dice ", new Option(BackKey.ToString().Substring(0, 3), " End game "), ExitGame);
            if (betAvailable)
                options = options.Insert(1, new("B Make bet "));
            if (audienceTileAvailable)
                options = options.Insert(1, new("A Put audience tile "));
            if (typingCardAvailalbe)
                options = options.Insert(1, new("C Draw typing card "));
            return options;
        }

        public static IReadOnlyDictionary<MenuLevels, Options> LevelOptions { get; } = new Dictionary<MenuLevels, Options>()
        {
            [MenuLevels.BackOrQuit] = new Options(GoBack, ExitGame),
            [MenuLevels.ComputerPlayersCount] = new("2 Two ", "3 Three ", "4 Four ", "5 Five ", "6 Six ", "7 Seven ", GoBack, ExitGame),
            [MenuLevels.Confirmation] = new(new Option("Y|Enter", " Yes "), new Option("N|Esc", " No ")),
            [MenuLevels.AddedSomePlayer] = new(new Option("Del", " Remove last player "), GoBack, ExitGame),
            [MenuLevels.GameMode] = new("S Singleplayer ", "M Multiplayer ", GoBack, ExitGame),
            [MenuLevels.HumanPlayersCount] = new("3 Three ", "4 Four ", "5 Five ", "6 Six ", "7 Seven ", "8 Eight ", GoBack, ExitGame),
            [MenuLevels.MainMenu] = new("N New game ", "A About game ", ExitGame),
            [MenuLevels.MainMenuWithGame] = new("N New game ", "C Continue last game ", "A About game ", ExitGame),
            [MenuLevels.Scrollable] = new("↑ Sroll up ", "↓ Sroll down ", new Option("PgUp", " Page up "), new Option("PgDn", " Page down "), GoBack, ExitGame)
        };

        public static IReadOnlyDictionary<MenuLevels, int> LevelOptionsAlignToRight { get; } = new Dictionary<MenuLevels, int>()
        {
            [MenuLevels.AddedSomePlayer] = -1,
            [MenuLevels.BackOrQuit] = -1,
            [MenuLevels.ComputerPlayersCount] = 2,
            [MenuLevels.Confirmation] = 0,
            [MenuLevels.GameActionChoose] = 2,
            [MenuLevels.GameMode] = 2,
            [MenuLevels.HumanPlayersCount] = 2,
            [MenuLevels.MainMenu] = 2,
            [MenuLevels.MainMenuWithGame] = 2,
            [MenuLevels.Scrollable] = 2
        };
    }
}
