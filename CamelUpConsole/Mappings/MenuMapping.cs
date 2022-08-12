using CamelUpConsole.ConsoleHelper.MenuBarModels;
using CamelUpConsole.Enums;
using System.Collections.Generic;

namespace CamelUpConsole.Mappings
{
    internal static class MenuMapping
    {
        public static IReadOnlyDictionary<MenuLevels, Options> LevelOptions { get; } = new Dictionary<MenuLevels, Options>()
        {
            [MenuLevels.MainMenu] = new("N New game ", "Q Quit game "),
            [MenuLevels.GameMode] = new("S Singleplayer ", "M Multiplayer ", "B Go back ", "Q Quit game "),
            [MenuLevels.ActionChoose] = new("D Draw dice ", "C Draw typing card ", "A Put audience tile ", "B Make bet ", "E End game ", "Q Quit game "),
            [MenuLevels.Confirmation] = new("Y Yes ", "N No ")
        };
    }
}
