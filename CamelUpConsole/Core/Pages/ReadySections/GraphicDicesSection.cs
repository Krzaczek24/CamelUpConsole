using CamelUpConsole.Enums;
using CamelUpEngine;
using CamelUpEngine.Core.Enums;
using CamelUpEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class GraphicDicesSection : Section
    {
        private Game game;

        public GraphicDicesSection(Game game) : base(25, 1, 36, 7, true, "Dices")
        {
            this.game = game;
        }

        public override void Render()
        {
            if (!game.DrawnDices.Any())
            {
                Rendered = false;
                base.Render();
                return;
            }

            int col = X;
            int row = Y;

            if (WithFrame)
            {
                col += 1;
                row += 1;
            }

            if (!string.IsNullOrEmpty(Header))
            {
                row += 2;
            }

            foreach (IDrawnDice dice in game.DrawnDices)
            {
                switch (Math.Abs(dice.Value))
                {
                    case 1:
                        Console.SetCursorPosition(col, row);
                        new LineRenderInfo("      ", TextAligment.None, EyeColor[dice.Colour], DiceColor[dice.Colour], 0).Render();
                        Console.SetCursorPosition(col, row + 1);
                        new LineRenderInfo("  ()  ", TextAligment.None, EyeColor[dice.Colour], DiceColor[dice.Colour], 0).Render();
                        Console.SetCursorPosition(col, row + 2);
                        new LineRenderInfo("      ", TextAligment.None, EyeColor[dice.Colour], DiceColor[dice.Colour], 0).Render();
                        break;
                    case 2:
                        Console.SetCursorPosition(col, row);
                        new LineRenderInfo("    ()", TextAligment.None, EyeColor[dice.Colour], DiceColor[dice.Colour], 0).Render();
                        Console.SetCursorPosition(col, row + 1);
                        new LineRenderInfo("      ", TextAligment.None, EyeColor[dice.Colour], DiceColor[dice.Colour], 0).Render();
                        Console.SetCursorPosition(col, row + 2);
                        new LineRenderInfo("()    ", TextAligment.None, EyeColor[dice.Colour], DiceColor[dice.Colour], 0).Render();
                        break;
                    case 3:
                        Console.SetCursorPosition(col, row);
                        new LineRenderInfo("    ()", TextAligment.None, EyeColor[dice.Colour], DiceColor[dice.Colour], 0).Render();
                        Console.SetCursorPosition(col, row + 1);
                        new LineRenderInfo("  ()  ", TextAligment.None, EyeColor[dice.Colour], DiceColor[dice.Colour], 0).Render();
                        Console.SetCursorPosition(col, row + 2);
                        new LineRenderInfo("()    ", TextAligment.None, EyeColor[dice.Colour], DiceColor[dice.Colour], 0).Render();
                        break;
                }

                col += 7;
            }
        }

        private static readonly IReadOnlyDictionary<Colour, ConsoleColor> EyeColor = new Dictionary<Colour, ConsoleColor>()
        {
            [Colour.Red] = ConsoleColor.White,
            [Colour.Yellow] = ConsoleColor.Black,
            [Colour.Green] = ConsoleColor.Black,
            [Colour.Blue] = ConsoleColor.White,
            [Colour.Violet] = ConsoleColor.White,
            [Colour.White] = ConsoleColor.White,
            [Colour.Black] = ConsoleColor.Black
        };

        private static readonly IReadOnlyDictionary<Colour, ConsoleColor> DiceColor = new Dictionary<Colour, ConsoleColor>()
        {
            [Colour.Red] = ConsoleColor.Red,
            [Colour.Yellow] = ConsoleColor.DarkYellow,
            [Colour.Green] = ConsoleColor.Green,
            [Colour.Blue] = ConsoleColor.Blue,
            [Colour.Violet] = ConsoleColor.Magenta,
            [Colour.White] = ConsoleColor.Gray,
            [Colour.Black] = ConsoleColor.Gray
        };
    }
}
