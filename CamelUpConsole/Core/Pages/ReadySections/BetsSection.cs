using CamelUpConsole.Enums;
using CamelUpEngine;
using System;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class BetsSection : Section
    {
        private Game game;

        public BetsSection(Game game, int x, int y, int width, int height, bool vertical = false) : base(x, y, width, height, true, "Bets", vertical: vertical)
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            if (Vertical)
            {
                Console.SetCursorPosition(Dimensions.Inner.X, Dimensions.Inner.Y);
                new LineRenderInfo("Winner", TextAligment.Left, ConsoleColor.White, ConsoleColor.Black, 0, Dimensions.Inner.Width).Render();
                Console.SetCursorPosition(Dimensions.Inner.X + Dimensions.Inner.Width - 2, Dimensions.Inner.Y);
                new LineRenderInfo(game.WinnerBets.Count.ToString().PadLeft(2), TextAligment.None, ConsoleColor.Green, ConsoleColor.Black, 0).Render();
                Console.SetCursorPosition(Dimensions.Inner.X, Dimensions.Inner.Y + 1);
                new LineRenderInfo("Loser", TextAligment.Left, ConsoleColor.White, ConsoleColor.Black, 0, Dimensions.Inner.Width).Render();
                Console.SetCursorPosition(Dimensions.Inner.X + Dimensions.Inner.Width - 2, Dimensions.Inner.Y + 1);
                new LineRenderInfo(game.LoserBets.Count.ToString().PadLeft(2), TextAligment.None, ConsoleColor.Green, ConsoleColor.Black, 0).Render();
            }
            else
            {
                Console.SetCursorPosition(Dimensions.Inner.X, Dimensions.Inner.Y);
                new LineRenderInfo("Winner:", TextAligment.None, ConsoleColor.White, ConsoleColor.Black, 0).Render();
                new LineRenderInfo(game.WinnerBets.Count.ToString().PadLeft(2), TextAligment.None, ConsoleColor.Green, ConsoleColor.Black, 0).Render();
                new LineRenderInfo(" Loser:", TextAligment.None, ConsoleColor.White, ConsoleColor.Black, 0).Render();
                new LineRenderInfo(game.LoserBets.Count.ToString().PadLeft(2), TextAligment.None, ConsoleColor.Green, ConsoleColor.Black, 0).Render();
            }
        }
    }
}
