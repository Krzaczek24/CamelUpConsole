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
            base.Render();

            int col = Dimensions.Inner.X;
            int row = Dimensions.Inner.Y;

            if (WithFrame)
                col += 2;
            if (Vertical)
                col++;

            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(col, row);

                IDrawnDice dice = game.DrawnDices.Skip(i).FirstOrDefault();
                if (dice != null)
                {
                    new LineRenderInfo("  ", TextAligment.None, background: ColorMapping.Dice[dice.Colour]).Render();
                    new LineRenderInfo(Math.Abs(dice.Value).ToString(), TextAligment.None, ConsoleColor.White, ConsoleColor.Black, 0).Render();
                }
                else
                {
                    new LineRenderInfo("   ", TextAligment.None, background: ConsoleColor.Black).Render();
                }

                if (Vertical)
                    row++;
                else
                    col += 4;
            }
        }
    }
}
