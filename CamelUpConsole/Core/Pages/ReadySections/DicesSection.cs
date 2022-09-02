using CamelUpConsole.Enums;
using CamelUpEngine.GameObjects;
using CamelUpEngine;
using System;
using System.Linq;
using CamelUpConsole.Mappings;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class DicesSection : Section
    {
        private Game game;

        public DicesSection(Game game, int x, int y, int width, int height, bool vertical = false) : base(x, y, width, height, true, "Dices", vertical: vertical)
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

            int col = Dimensions.Inner.X;
            int row = Dimensions.Inner.Y;

            if (WithFrame)
                col += 2;
            if (Vertical)
                col++;

            foreach (IDrawnDice dice in game.DrawnDices)
            {
                Console.SetCursorPosition(col, row);
                new LineRenderInfo("  ", TextAligment.None, background: ColorMapping.Dice[dice.Colour]).Render();
                new LineRenderInfo(Math.Abs(dice.Value).ToString(), TextAligment.None, ConsoleColor.White, ConsoleColor.Black, 0).Render();

                if (Vertical)
                    row++;
                else
                    col += 4;
            }
        }
    }
}
