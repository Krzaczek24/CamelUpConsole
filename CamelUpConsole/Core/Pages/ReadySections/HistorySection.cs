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

        public int CurrentLine { get; private set; } = 0;

        public int LinesCount => game.History.Count;
        public int Percent => CurrentLine * 100 / (LinesCount - Dimensions.Inner.Height);
        public int LinesProgress => CurrentLine * LinesCount / (LinesCount - Dimensions.Inner.Height);

        public void Reset() => CurrentLine = Math.Max(0, LinesCount - Dimensions.Inner.Height);
        public void ScrollUp() => CurrentLine = Math.Max(0, CurrentLine - 1);
        public void ScrollDown() => CurrentLine = Math.Min(LinesCount - Dimensions.Inner.Height, CurrentLine + 1);
        public void PageUp() => CurrentLine = Math.Max(0, CurrentLine - Dimensions.Inner.Height);
        public void PageDown() => CurrentLine = Math.Min(LinesCount - Dimensions.Inner.Height, CurrentLine + Dimensions.Inner.Height);

        public HistorySection(Game game, int x, int y, int width, int height, bool withFrame = true, string header = "History") : base(x, y, width, height, withFrame, header)
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            int row = Dimensions.Inner.Y;
            IList<ActionEventDescription> actions = game.History.GetPrettyDescription().Skip(CurrentLine).Take(Dimensions.Inner.Height).ToList();
            foreach (ActionEventDescription action in actions)
            {
                Console.SetCursorPosition(Dimensions.Inner.X, row++);
                new LineRenderInfo(action.Text, TextAligment.Left, action.ForegroundColor, action.BackgroundColor, 0, Dimensions.Inner.Width).Render();
            }
        }
    }
}
