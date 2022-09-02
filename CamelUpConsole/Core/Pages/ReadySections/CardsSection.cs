using CamelUpConsole.Enums;
using CamelUpEngine;
using CamelUpEngine.Core.Enums;
using System;
using CamelUpEngine.GameObjects.Available;
using CamelUpEngine.Helpers;
using System.Linq;
using CamelUpConsole.Mappings;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class CardsSection : Section
    {
        private Game game;

        public CardsSection(Game game, int x, int y, int width, int height, bool vertical = false) : base(x, y, width, height, true, "Cards", vertical: vertical)
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            int col = Vertical ? Dimensions.Inner.X + 2 : Dimensions.Inner.X;
            int row = Dimensions.Inner.Y;

            foreach (Colour colour in ColourHelper.AllCardColours)
            {
                IAvailableTypingCard card = game.AvailableTypingCards.SingleOrDefault(card => card.Colour == colour);

                Console.SetCursorPosition(col, row);

                if (card != null)
                {
                    int count = game.RemainingTypingCards[colour].Count - 1;
                    new LineRenderInfo(string.Empty.PadRight(count, '|'), TextAligment.None, ColorMapping.Card[card.Colour], ConsoleColor.Black, 0).Render();
                    new LineRenderInfo(" ", TextAligment.None, background: ColorMapping.Card[card.Colour]).Render();
                    new LineRenderInfo(((int)card.Value).ToString().PadRight(4 - count), TextAligment.None, ConsoleColor.White, ConsoleColor.Black, 0).Render();
                }
                else
                {
                    new LineRenderInfo("   ", TextAligment.None, background: ConsoleColor.Black).Render();
                }

                if (Vertical)
                    row++;
                else
                    col += 6;
            }
        }
    }
}
