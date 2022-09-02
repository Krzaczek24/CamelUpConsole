using CamelUpEngine.Core.Enums;
using System;
using System.Collections.Generic;

namespace CamelUpConsole.Mappings
{
    internal static class ColorMapping
    {
        public static readonly IReadOnlyDictionary<Colour, ConsoleColor> Camel = new Dictionary<Colour, ConsoleColor>()
        {
            [Colour.Red] = ConsoleColor.Red,
            [Colour.Yellow] = ConsoleColor.DarkYellow,
            [Colour.Green] = ConsoleColor.Green,
            [Colour.Blue] = ConsoleColor.Blue,
            [Colour.Violet] = ConsoleColor.Magenta,
            [Colour.White] = ConsoleColor.White,
            [Colour.Black] = ConsoleColor.DarkGray
        };

        public static readonly IReadOnlyDictionary<Colour, ConsoleColor> Card = new Dictionary<Colour, ConsoleColor>()
        {
            [Colour.Red] = ConsoleColor.Red,
            [Colour.Yellow] = ConsoleColor.DarkYellow,
            [Colour.Green] = ConsoleColor.Green,
            [Colour.Blue] = ConsoleColor.Blue,
            [Colour.Violet] = ConsoleColor.Magenta
        };

        public static readonly IReadOnlyDictionary<Colour, ConsoleColor> Dice = new Dictionary<Colour, ConsoleColor>()
        {
            [Colour.Red] = ConsoleColor.Red,
            [Colour.Yellow] = ConsoleColor.DarkYellow,
            [Colour.Green] = ConsoleColor.Green,
            [Colour.Blue] = ConsoleColor.Blue,
            [Colour.Violet] = ConsoleColor.Magenta,
            [Colour.White] = ConsoleColor.White,
            [Colour.Black] = ConsoleColor.DarkGray
        };
    }
}
