using CamelUpConsole.ConsoleHelper.MenuBarModels;
using CamelUpConsole.Enums;
using System.Collections.Generic;

namespace CamelUpConsole.Mappings
{
    internal static class MenuMapping
    {
        public static IReadOnlyDictionary<MenuLevels, Options> LevelOptions { get; } = new Dictionary<MenuLevels, Options>()
        {
            [MenuLevels.ActionChoose] = new("D Draw dice ", "C Draw typing card ", "A Put audience tile ", "B Make bet ", "E End game ", "Q Quit game "),
            [MenuLevels.ComputerPlayersCount] = new("2 Two ", "3 Three ", "4 Four ", "5 Five ", "6 Six ", "7 Seven ", "B Go back ", "Q Quit game "),
            [MenuLevels.Confirmation] = new("Y Yes ", "N No "),
            [MenuLevels.GameMode] = new("S Singleplayer ", "M Multiplayer ", "B Go back ", "Q Quit game "),
            [MenuLevels.HumanPlayersCount] = new("3 Three ", "4 Four ", "5 Five ", "6 Six ", "7 Seven ", "8 Eight ", "B Go back ", "Q Quit game "),
            [MenuLevels.MainMenu] = new("N New game ", "Q Quit game ")
        };

        public static IReadOnlyDictionary<MenuLevels, int> LevelOptionsAlignToRight { get; } = new Dictionary<MenuLevels, int>()
        {
            [MenuLevels.ActionChoose] = 2,
            [MenuLevels.ComputerPlayersCount] = 2,
            [MenuLevels.Confirmation] = 0,
            [MenuLevels.GameMode] = 2,
            [MenuLevels.HumanPlayersCount] = 2,
            [MenuLevels.MainMenu] = 1
        };
    }
}
