using CamelUpConsole.ConsoleHelper.MenuBarModels;
using CamelUpConsole.Enums;
using System.Collections.Generic;

namespace CamelUpConsole.Mappings
{
    internal static class MenuMapping
    {
        public static IReadOnlyDictionary<MenuLevels, Options> LevelOptions { get; } = new Dictionary<MenuLevels, Options>()
        {
            [MenuLevels.StartGame] = new("N New game ", "Q Quit game "),
            [MenuLevels.ActionChoose] = new("D Draw dice ", "C Draw typing card ", "A Put audience tile ", "B Make bet ", "Q Quit game ")
        };
    }
}
