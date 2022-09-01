using CamelUpConsole.Enums;
using CamelUpEngine;
using CamelUpEngine.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class HistorySection : Section
    {
        private Game game;

        public HistorySection(Game game) : base(2, 14, 38, 11, true, "History")
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            int row = Dimensions.Inner.Y;
            IList<IActionEvent> actions = game.History.TakeLast(Dimensions.Inner.Height).ToList();
            foreach (IActionEvent action in actions)
            {
                Console.SetCursorPosition(Dimensions.Inner.X, row++);
                new LineRenderInfo(action.GetType().Name, TextAligment.None, ConsoleColor.White, ConsoleColor.Black, 0, Dimensions.Inner.Width).Render();
            }

            if (actions.Count >= Dimensions.Inner.Height)
            {
                Console.SetCursorPosition(Dimensions.Inner.X, Dimensions.Inner.Y);
                new LineRenderInfo("...", TextAligment.Left, ConsoleColor.Green, ConsoleColor.Black, 0, Dimensions.Inner.Width).Render();
            }
        }
    }
}
