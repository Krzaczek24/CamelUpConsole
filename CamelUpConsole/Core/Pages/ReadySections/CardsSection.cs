using CamelUpConsole.Enums;
using CamelUpEngine;
using CamelUpEngine.Core.Enums;
using System.Collections.Generic;
using System;
using CamelUpEngine.GameObjects.Available;
using CamelUpEngine.Helpers;
using System.Linq;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class CardsSection : Section
    {
        private Game game;

        public CardsSection(Game game) : base(87, 1, 31, 5, true, "Available cards")
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            int col = Dimensions.Inner.X;

            foreach (Colour colour in ColourHelper.AllCardColours)
            {
                IAvailableTypingCard card = game.AvailableTypingCards.SingleOrDefault(card => card.Colour == colour);

                Console.SetCursorPosition(col, Dimensions.Inner.Y);

                if (card != null)
                {
                    int count = game.RemainingTypingCards[colour].Count - 1;
                    new LineRenderInfo(string.Empty.PadRight(count, '|'), TextAligment.None, CardColor[card.Colour], ConsoleColor.Black, 0).Render();
                    new LineRenderInfo(" ", TextAligment.None, background: CardColor[card.Colour], margin: 0).Render();
                    new LineRenderInfo(((int)card.Value).ToString().PadRight(4 - count), TextAligment.None, ConsoleColor.White, ConsoleColor.Black, 0).Render();
                }
                else
                {
                    new LineRenderInfo("   ", TextAligment.None, background: ConsoleColor.Black, margin: 0).Render();
                }
                
                col += 6;
            }
        }

        private static readonly IReadOnlyDictionary<Colour, ConsoleColor> CardColor = new Dictionary<Colour, ConsoleColor>()
        {
            [Colour.Red] = ConsoleColor.Red,
            [Colour.Yellow] = ConsoleColor.DarkYellow,
            [Colour.Green] = ConsoleColor.Green,
            [Colour.Blue] = ConsoleColor.Blue,
            [Colour.Violet] = ConsoleColor.Magenta
        };
    }
}
