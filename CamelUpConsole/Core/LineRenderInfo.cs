using CamelUpConsole.Enums;
using System;

namespace CamelUpConsole.Core
{
    internal class LineRenderInfo
    {
        public string Text { get; set; } = string.Empty;
        public TextAligment TextAligment { get; set; }
        public int Margin { get; set; }
        public int MaxLength { get; set; }
        public ConsoleColor? Foreground { get; set; }
        public ConsoleColor? Background { get; set; }

        public static LineRenderInfo BlankLine { get; } = new LineRenderInfo(string.Empty);

        public LineRenderInfo(string text, TextAligment aligment = TextAligment.Left, ConsoleColor? foreground = null, ConsoleColor? background = null, int margin = 0, int maxLength = 0)
        {
            Text = text;
            TextAligment = aligment;
            Margin = margin;
            MaxLength = maxLength > 0 ? maxLength : Console.WindowWidth;
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
                    return ToLeftFullLine(Text, Margin, MaxLength);
                case TextAligment.Right:
                    return ToRightFullLine(Text, Margin, MaxLength);
                case TextAligment.Center:
                    return CenteredFullLine(Text, Margin, MaxLength);
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
            && Console.CursorLeft + formattedText.Length >= MaxLength)
                formattedText = formattedText.Substring(0, formattedText.Length - 1);
            Console.Write(formattedText);
        }

        public static string CenteredFullLine(string str, int margin = 0, int maxWidth = 0)
        {
            maxWidth = maxWidth > 0 ? maxWidth : Console.WindowWidth;
            int lengthWithMargins = str.Length + 2 * margin;

            if (lengthWithMargins < maxWidth)
                return str.PadLeft((int)Math.Round((maxWidth + str.Length - 1) / 2.0, MidpointRounding.AwayFromZero))
                    .PadRight(maxWidth);

            if (lengthWithMargins > maxWidth)
                str = str.Substring(margin, maxWidth - 2 * margin);

            return str.PadLeft(margin + str.Length).PadRight(maxWidth);
        }

        public static string ToLeftFullLine(string str, int margin = 0, int maxWidth = 0)
        {
            maxWidth = maxWidth > 0 ? maxWidth : Console.WindowWidth;
            return str.PadLeft(margin + str.Length).PadRight(maxWidth);
        }

        public static string ToRightFullLine(string str, int margin = 0, int maxWidth = 0)
        {
            maxWidth = maxWidth > 0 ? maxWidth : Console.WindowWidth;
            return str.PadRight(margin + str.Length).PadLeft(maxWidth);
        }
    }
}
