using CamelUpConsole.Enums;
using System;

namespace CamelUpConsole.Core
{
    internal class LineRenderInfo
    {
        private const int MARGIN_DEFAULT = 1;
        public string Text { get; set; } = string.Empty;
        public TextAligment TextAligment { get; set; }
        public int Margin { get; set; }
        public ConsoleColor? Foreground { get; set; }
        public ConsoleColor? Background { get; set; }

        public static LineRenderInfo BlankLine { get; } = new LineRenderInfo(string.Empty);

        public LineRenderInfo(string text, TextAligment aligment = TextAligment.Left, ConsoleColor? foreground = null, ConsoleColor? background = null, int margin = MARGIN_DEFAULT)
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
                case TextAligment.None:
                    return Text;
                case TextAligment.Left:
                    return ToLeftFullLine(Text, Margin);
                case TextAligment.Right:
                    return ToRightFullLine(Text, Margin);
                case TextAligment.Center:
                    return CenteredFullLine(Text, Margin);
                default:
                    throw new NotImplementedException($"Unknown value [{TextAligment}] of {nameof(TextAligment)}");
            }
        }

        public void Render()
        {
            Console.ForegroundColor = Foreground ?? Console.ForegroundColor;
            Console.BackgroundColor = Background ?? Console.BackgroundColor;
            string formattedText = GetFormattedText();
            if (Console.CursorTop + 1 == Console.WindowHeight 
            && Console.CursorLeft + formattedText.Length >= Console.WindowWidth)
                formattedText = formattedText.Substring(0, formattedText.Length - 1);
            Console.Write(formattedText);
        }

        public static string CenteredFullLine(string str, int margin = MARGIN_DEFAULT)
        {
            int maxWidth = Console.WindowWidth;
            int lengthWithMargins = str.Length + 2 * margin;

            if (lengthWithMargins < maxWidth)
                return str.PadLeft((int)Math.Round((maxWidth + str.Length - 1) / 2.0, MidpointRounding.AwayFromZero))
                    .PadRight(maxWidth);

            if (lengthWithMargins > maxWidth)
                str = str.Substring(margin, maxWidth - 2 * margin);

            return str.PadLeft(margin + str.Length)
                .PadRight(maxWidth);
        }

        public static string ToLeftFullLine(string str, int margin = MARGIN_DEFAULT)
        {
            return str.PadLeft(margin + str.Length).PadRight(Console.WindowWidth);
        }

        public static string ToRightFullLine(string str, int margin = MARGIN_DEFAULT)
        {
            return str.PadRight(margin + str.Length).PadLeft(Console.WindowWidth);
        }
    }
}
