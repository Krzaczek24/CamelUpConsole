using CamelUpConsole.Enums;
using CamelUpConsole.Mappings;
using CamelUpEngine;
using CamelUpEngine.GameObjects;
using System;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class PlayersSection : Section
    {
        private const int playerColWidth = 13;
        private const int coinsColWidth = 3;
        private Game game;

        public PlayersSection(Game game, int x, int y, int width, int height) : base(x, y, width, height, true, "Players")
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            int row = Dimensions.Inner.Y;
            foreach (IPlayer player in game.Players)
            {
                Console.SetCursorPosition(Dimensions.Inner.X, row);
                if (player == game.CurrentPlayer)
                {
                    new LineRenderInfo(">", foreground: ConsoleColor.Magenta, maxLength: 1).Render();
                    new LineRenderInfo(player.Name, foreground: ConsoleColor.Cyan, maxLength: playerColWidth - 1).Render();
                }
                else
                {
                    new LineRenderInfo(player.Name, foreground: ConsoleColor.White, maxLength: playerColWidth).Render();
                }

                string coins = player.Coins.ToString().PadLeft(coinsColWidth);
                int lineEnd = Dimensions.Inner.Width - playerColWidth;
                new LineRenderInfo(coins, foreground: ConsoleColor.Green, maxLength: lineEnd).Render();

                Console.SetCursorPosition(Dimensions.Inner.X + playerColWidth + coinsColWidth + 2, row);
                foreach (ITypingCard card in player.TypingCards)
                {
                    new LineRenderInfo(string.Empty, background: ColorMapping.Card[card.Colour], maxLength: 1).Render();
                    new LineRenderInfo($"{(int)card.Value} ", TextAligment.Left, ConsoleColor.White, ConsoleColor.Black, 0, maxLength: 1).Render();
                }

                row++;
            }
        }
    }
}
