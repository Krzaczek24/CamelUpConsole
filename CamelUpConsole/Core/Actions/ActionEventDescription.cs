using System;

namespace CamelUpConsole.Core.Actions
{
    internal class ActionEventDescription
    {
        public string Text { get; }
        public ConsoleColor ForegroundColor { get; }
        public ConsoleColor BackgroundColor { get; }
        public bool IsMultiLine => Text?.Contains('\n') ?? false;

        public ActionEventDescription(string text, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            Text = text;
            ForegroundColor = foreground;
            BackgroundColor = background;
        }
    }
}
