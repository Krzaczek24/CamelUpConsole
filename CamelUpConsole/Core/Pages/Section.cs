using System;

namespace CamelUpConsole.Core.Pages
{
    internal abstract class Section
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public bool WithFrame { get; }
        public string Header { get; }
        public ConsoleColor FrameColor { get; }
        public ConsoleColor HeaderColor { get; }
        protected bool Rendered { get; set; } = false;

        public Section(int x, int y, int width, int height, bool withFrame = true, string header = null, ConsoleColor frameColor = ConsoleColor.DarkYellow, ConsoleColor headerColor = ConsoleColor.Magenta)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            WithFrame = withFrame;
            Header = header;
            FrameColor = frameColor;
            HeaderColor = headerColor;
        }

        public virtual void Render()
        {
            if (!Rendered)
            {
                Console.SetCursorPosition(X, Y);

                if (WithFrame)
                {
                    Console.ForegroundColor = FrameColor;
                    Console.Write($"+{string.Empty.PadRight(Width - 2, '-')}+");
                    for (int row = Y + 1; row < Y + Height - 1; row++)
                    {
                        Console.SetCursorPosition(X, row);
                        Console.Write($"|{string.Empty.PadRight(Width - 2)}|");
                    }
                    Console.SetCursorPosition(X, Y + Height - 1);
                    Console.Write($"+{string.Empty.PadRight(Width - 2, '-')}+");

                    if (!string.IsNullOrEmpty(Header))
                    {
                        Console.SetCursorPosition(X, Y + 2);
                        Console.Write($"+{string.Empty.PadRight(Width - 2, '-')}+");

                        Console.SetCursorPosition(X + 1, Y + 1);
                        new LineRenderInfo(Header, Enums.TextAligment.Center, HeaderColor, maxLength: Width - 2).Render();
                    }
                }
                else if (!string.IsNullOrEmpty(Header))
                {
                    Console.SetCursorPosition(X, Y);
                    new LineRenderInfo(Header, Enums.TextAligment.Center, HeaderColor, maxLength: Width - 2).Render();

                    Console.ForegroundColor = FrameColor;
                    Console.SetCursorPosition(X, Y + 1);
                    Console.Write(string.Empty.PadRight(Width, '-'));
                }

                Rendered = true;
            }
        }
    }
}
