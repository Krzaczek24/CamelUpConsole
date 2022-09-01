﻿using CamelUpConsole.Enums;
using CamelUpEngine.Core.Enums;
using CamelUpEngine.GameObjects;
using CamelUpEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class DicesSection : Section
    {
        private Game game;

        public DicesSection(Game game) : base(62, 1, 23, 5, true, "Dices")
        {
            this.game = game;
        }

        public override void Render()
        {
            if (!game.DrawnDices.Any())
            {
                RerenderFrameOrHeader = true;
                base.Render();
                return;
            }

            int col = WithFrame ? Dimensions.Inner.X + 1 : Dimensions.Inner.X;
            foreach (IDrawnDice dice in game.DrawnDices)
            {
                Console.SetCursorPosition(col, Dimensions.Inner.Y);
                new LineRenderInfo("  ", TextAligment.None, background: DiceColor[dice.Colour], margin: 0).Render();
                new LineRenderInfo(Math.Abs(dice.Value).ToString(), TextAligment.None, ConsoleColor.White, ConsoleColor.Black, 0).Render();
                col += 4;
            }
        }

        private static readonly IReadOnlyDictionary<Colour, ConsoleColor> DiceColor = new Dictionary<Colour, ConsoleColor>()
        {
            [Colour.Red] = ConsoleColor.Red,
            [Colour.Yellow] = ConsoleColor.DarkYellow,
            [Colour.Green] = ConsoleColor.Green,
            [Colour.Blue] = ConsoleColor.Blue,
            [Colour.Violet] = ConsoleColor.Magenta,
            [Colour.White] = ConsoleColor.White,
            [Colour.Black] = ConsoleColor.Black
        };
    }
}
