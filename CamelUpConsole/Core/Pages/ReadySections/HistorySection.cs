using CamelUpConsole.Core.Actions;
using CamelUpConsole.Enums;
using CamelUpEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class HistorySection : Section
    {
        private Game game;

        public HistorySection(Game game, int x, int y, int width, int height, bool withFrame = true, string header = "History") : base(x, y, width, height, withFrame, header)
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            int row = Dimensions.Inner.Y;
            IList<ActionEventDescription> actions = game.History.GetPrettyDescription().TakeLast(Dimensions.Inner.Height).ToList();
            foreach (ActionEventDescription action in actions)
            {
                Console.SetCursorPosition(Dimensions.Inner.X, row++);
                new LineRenderInfo(action.Text, TextAligment.Left, action.ForegroundColor, action.BackgroundColor, 0, Dimensions.Inner.Width).Render();
            }

            if (actions.Count >= Dimensions.Inner.Height)
            {
                Console.SetCursorPosition(Dimensions.Inner.X, Dimensions.Inner.Y);
                new LineRenderInfo("...", TextAligment.Left, ConsoleColor.Green, ConsoleColor.Black, 0, Dimensions.Inner.Width).Render();
            }
        }
    }
}
