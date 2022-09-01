using CamelUpConsole.Enums;
using CamelUpEngine;
using CamelUpEngine.Core.Enums;
using CamelUpEngine.GameObjects.Available;
using CamelUpEngine.Helpers;
using System.Linq;
using System;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class BetsSection : Section
    {
        private Game game;

        public BetsSection(Game game) : base(42, 1, 18, 5, true, "Bets")
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            Console.SetCursorPosition(Dimensions.Inner.X, Dimensions.Inner.Y);
            new LineRenderInfo("Winner:", TextAligment.None, ConsoleColor.White, ConsoleColor.Black, 0).Render();
            new LineRenderInfo(game.WinnerBets.Count.ToString(), TextAligment.None, ConsoleColor.Green, ConsoleColor.Black, 0).Render();
            new LineRenderInfo(" Loser:", TextAligment.None, ConsoleColor.White, ConsoleColor.Black, 0).Render();
            new LineRenderInfo(game.LoserBets.Count.ToString(), TextAligment.None, ConsoleColor.Green, ConsoleColor.Black, 0).Render();
        }
    }
}
