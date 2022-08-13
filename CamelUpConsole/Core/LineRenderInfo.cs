using CamelUpConsole.Enums;
using System;

namespace CamelUpConsole.Core
{
    internal class LineRenderInfo
    {
        public string Text { get; set; } = string.Empty;
        public TextAligment TextAligment { get; set; }
        public int Margin { get; set; }
        public ConsoleColor? Foreground { get; set; }
        public ConsoleColor? Background { get; set; }

        public static LineRenderInfo BlankLine { get; } = new LineRenderInfo(string.Empty);

        public LineRenderInfo(string text, TextAligment aligment = TextAligment.Left, ConsoleColor? foreground = null, ConsoleColor? background = null, int margin = 6)
        {
            Text = text;
            TextAligment = aligment;
            Margin = margin;
            Foreground = foreground;
            Background = background;
        }

        public string GetFormattedText()
        {
            switch (TextAligment)
            {
                case TextAligment.Left:
                    return ToLeftFullLine(Text, Margin);
                case TextAligment.Right:
                    return ToRightFullLine(Text, Margin);
                case TextAligment.Center:
                    return CenteredFullLine(Text);
                default:
                    throw new NotImplementedException($"Unknown value [{TextAligment}] of {nameof(TextAligment)}");
            }
        }

        public void Render()
        {
            Console.ForegroundColor = Foreground ?? Console.ForegroundColor;
            Console.BackgroundColor = Background ?? Console.BackgroundColor;
            Console.Write(GetFormattedText());
        }

        public static string CenteredFullLine(string str)
        {            
            int maxWidth = Console.WindowWidth;
            int padLeft = (maxWidth + str.Length) / 2;
            return str.PadLeft(padLeft).PadRight(maxWidth);
        }

        public static string ToLeftFullLine(string str, int margin = 6)
        {
            return str.PadLeft(margin + str.Length).PadRight(Console.WindowWidth);
        }

        public static string ToRightFullLine(string str, int margin = 6)
        {
            return str.PadRight(margin + str.Length).PadLeft(Console.WindowWidth);
        }
    }
}
