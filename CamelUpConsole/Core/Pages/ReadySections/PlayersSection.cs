using CamelUpConsole.Enums;
using CamelUpEngine;
using CamelUpEngine.GameObjects;
using System;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class PlayersSection : Section
    {
        private Game game;

        public PlayersSection(Game game) : base(2, 1, 21, 12, true, "Players")
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

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

            foreach (IPlayer player in game.Players)
            {
                Console.SetCursorPosition(col, row++);
                ConsoleColor color = player == game.CurrentPlayer ? ConsoleColor.Cyan : ConsoleColor.White;
                new LineRenderInfo(player.Name, foreground: color, margin: 0, maxLength: (WithFrame ? Width - 2 : Width) - 3).Render();
                new LineRenderInfo(player.Coins.ToString(), TextAligment.Right, ConsoleColor.Green, margin: 0, maxLength: 3).Render();
            }
        }
    }
}
