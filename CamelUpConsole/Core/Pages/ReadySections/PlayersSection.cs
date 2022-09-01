using CamelUpConsole.Enums;
using CamelUpEngine;
using CamelUpEngine.GameObjects;
using System;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class PlayersSection : Section
    {
        private const int coinsColWidth = 3;
        private Game game;

        public PlayersSection(Game game) : base(2, 1, 38, 12, true, "Players")
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            int row = Dimensions.Inner.Y;
            foreach (IPlayer player in game.Players)
            {
                Console.SetCursorPosition(Dimensions.Inner.X, row++);
                if (player == game.CurrentPlayer)
                {
                    new LineRenderInfo(">", foreground: ConsoleColor.Magenta, margin: 0, maxLength: 1).Render();
                    new LineRenderInfo(player.Name, foreground: ConsoleColor.Cyan, margin: 0, maxLength: 12).Render();
                }
                else
                {
                    new LineRenderInfo(player.Name, foreground: ConsoleColor.White, margin: 0, maxLength: 13).Render();
                }
                new LineRenderInfo(player.Coins.ToString(), TextAligment.Right, ConsoleColor.Green, margin: 0, maxLength: coinsColWidth).Render();
            }
        }
    }
}
