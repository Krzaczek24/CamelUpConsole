using CamelUpConsole.Core.MenuBar.Models;
using CamelUpConsole.Enums;
using System;
using System.Collections.Generic;

namespace CamelUpConsole.Mappings
{
    internal static class MenuMapping
    {
        public const ConsoleKey ConfirmKey = ConsoleKey.Enter;
        public const ConsoleKey BackKey = ConsoleKey.Escape;
        public const ConsoleKey QuitKey = ConsoleKey.F4;

        private static Option GoBack => new Option(BackKey.ToString().Substring(0, 3), " Go back ");
        private static Option ExitGame => new Option(QuitKey.ToString(), " Quit game ");

        public static IReadOnlyDictionary<MenuLevels, Options> LevelOptions { get; } = new Dictionary<MenuLevels, Options>()
        {
            [MenuLevels.BackOrQuit] = new Options(GoBack, ExitGame),
            [MenuLevels.ComputerPlayersCount] = new("2 Two ", "3 Three ", "4 Four ", "5 Five ", "6 Six ", "7 Seven ", GoBack, ExitGame),
            [MenuLevels.Confirmation] = new(new Option("Enter/Y", " Yes "), new Option("Esc/N", " No ")),
            [MenuLevels.GameActionChoose] = new("D Draw dice ", "C Draw typing card ", "A Put audience tile ", "B Make bet ", new Option(BackKey.ToString().Substring(0, 3), " End game "), ExitGame),
            [MenuLevels.GameMode] = new("S Singleplayer ", "M Multiplayer ", GoBack, ExitGame),
            [MenuLevels.HumanPlayersCount] = new("3 Three ", "4 Four ", "5 Five ", "6 Six ", "7 Seven ", "8 Eight ", GoBack, ExitGame),
            [MenuLevels.MainMenu] = new("N New game ", "A About game ", ExitGame),
            [MenuLevels.Scrollable] = new("↑ Sroll up ", "↓ Sroll down ", new Option("PgUp", " Page up "), new Option("PgDn", " Page down "), GoBack, ExitGame)
        };

        public static IReadOnlyDictionary<MenuLevels, int> LevelOptionsAlignToRight { get; } = new Dictionary<MenuLevels, int>()
        {
            [MenuLevels.BackOrQuit] = 2,
            [MenuLevels.ComputerPlayersCount] = 2,
            [MenuLevels.Confirmation] = 0,
            [MenuLevels.GameActionChoose] = 2,
            [MenuLevels.GameMode] = 2,
            [MenuLevels.HumanPlayersCount] = 2,
            [MenuLevels.MainMenu] = 2,
            [MenuLevels.Scrollable] = 2
        };
    }
}
