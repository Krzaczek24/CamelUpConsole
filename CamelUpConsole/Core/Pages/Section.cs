using System;

namespace CamelUpConsole.Core.Pages
{
    internal class SectionDimensions
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public SectionDimensions(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }

    internal class Dimensions
    {
        public SectionDimensions Outer { get; }
        public SectionDimensions Inner { get; }

        public Dimensions(SectionDimensions outer, SectionDimensions inner)
        {
            Inner = inner;
            Outer = outer;
        }
    }

    internal abstract class Section
    {
        public Dimensions Dimensions { get; }
        public bool WithFrame { get; }
        public string Header { get; }
        public ConsoleColor FrameColor { get; }
        public ConsoleColor HeaderColor { get; }
        protected bool RerenderFrameOrHeader { get; set; } = true;

        public Section(int x, int y, int width, int height, bool withFrame = true, string header = null, ConsoleColor frameColor = ConsoleColor.DarkYellow, ConsoleColor headerColor = ConsoleColor.Magenta)
        {
            int innerX = x, innerY = y, innerWidth = width, innerHeight = height;

            if (WithFrame = withFrame)
            {
                innerX += 1;
                innerY += 1;
                innerWidth -= 2;
                innerHeight -= 2;
            }

            if (!string.IsNullOrEmpty(Header = header))
            {
                innerY += 2;
                innerHeight -= 2;
            }

            Dimensions = new(new(x, y, width, height), new(innerX, innerY, innerWidth, innerHeight));
            FrameColor = frameColor;
            HeaderColor = headerColor;
        }

        public virtual void Render()
        {
            if (RerenderFrameOrHeader)
            {
                Console.SetCursorPosition(Dimensions.Outer.X, Dimensions.Outer.Y);

                if (WithFrame)
                {
                    Console.ForegroundColor = FrameColor;
                    Console.Write($"+{string.Empty.PadRight(Dimensions.Inner.Width, '-')}+");
                    for (int row = Dimensions.Outer.Y + 1; row < Dimensions.Outer.Y + Dimensions.Outer.Height - 1; row++)
                    {
                        Console.SetCursorPosition(Dimensions.Outer.X, row);
                        Console.Write($"|{string.Empty.PadRight(Dimensions.Inner.Width)}|");
                    }
                    Console.SetCursorPosition(Dimensions.Outer.X, Dimensions.Outer.Y + Dimensions.Outer.Height - 1);
                    Console.Write($"+{string.Empty.PadRight(Dimensions.Inner.Width, '-')}+");
                }

                if (!string.IsNullOrEmpty(Header))
                {
                    Console.ForegroundColor = FrameColor;
                    Console.SetCursorPosition(Dimensions.Outer.X, Dimensions.Inner.Y - 1);
                    if (WithFrame)
                        Console.Write($"+{string.Empty.PadRight(Dimensions.Inner.Width, '-')}+");
                    else
                        Console.Write(string.Empty.PadRight(Dimensions.Outer.Width, '-'));

                    Console.SetCursorPosition(Dimensions.Inner.X, Dimensions.Inner.Y - 2);
                    new LineRenderInfo(Header, Enums.TextAligment.Center, HeaderColor, maxLength: Dimensions.Inner.Width).Render();
                }

                RerenderFrameOrHeader = false;
            }
        }
    }
}
