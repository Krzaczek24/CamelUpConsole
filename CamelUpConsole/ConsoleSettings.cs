using System;

namespace CamelUpConsole
{
    internal class ConsoleSettings
    {
        public ConsoleColors Colors { get; set; } = new();
        public ConsoleCursor Cursor { get; set; } = new();

        public void SaveCurrent()
        {
            Colors.SaveCurrent();
            Cursor.SaveCurrent();
        }

        public void LoadSaved()
        {
            Colors.LoadSaved();
            Cursor.LoadSaved();
        }

        internal class ConsoleCursor
        {
            public int X { get; set; }
            public int Y { get; set; }

            public ConsoleCursor() => SaveCurrent();

            public void SaveCurrent()
            {
                X = Console.CursorLeft;
                Y = Console.CursorTop;
            }

            public void LoadSaved() => Console.SetCursorPosition(X, Y);
        }

        internal class ConsoleColors
        {
            public ConsoleColor BackgroundColor { get; set; }
            public ConsoleColor ForegroundColor { get; set; }

            public ConsoleColors() => SaveCurrent();

            public void SaveCurrent()
            {
                BackgroundColor = Console.BackgroundColor;
                ForegroundColor = Console.ForegroundColor;
            }

            public void LoadSaved()
            {
                Console.BackgroundColor = BackgroundColor;
                Console.ForegroundColor = ForegroundColor;
            }

            public static void Reverse()
            {
                ConsoleColor temp = Console.BackgroundColor;
                Console.BackgroundColor = Console.ForegroundColor;
                Console.ForegroundColor = temp;
            }
        }
    }    
}
