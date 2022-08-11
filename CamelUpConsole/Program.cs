using CamelUpConsole.ConsoleHelper;
using CamelUpConsole.ConsoleHelper.MenuBarModels;
using System;

namespace CamelUpConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Camel Up!";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            //MenuBar.OptionsMaxLength = 20;
            MenuBar.OptionsMaxLength = 0;

            //TestAscii();
            //TestColor();

            do
            {
                Options options = new("D Draw dice ", "C Draw typing card ", "A Put audience tile ", "B Make bet ", "Q Quit game ");
                MenuBar.Render(options, 1);
            }
            while (Console.ReadKey().Key != ConsoleKey.Q);

            //Console.ReadKey();
        }

        private static void TestAscii()
        {
            for (int c = 0; c < 256; c++)
            {
                Console.Write($"{c} = ");
                Console.WriteLine((char)c);
            }
        }

        private static void TestColor()
        {
            string str = "Test";
            var colors = Enum.GetValues<ConsoleColor>();
            int row = 0, col = 0;
            foreach (var colorA in colors)
            {
                foreach (var colorB in colors)
                {
                    Console.BackgroundColor = colorA;
                    Console.ForegroundColor = colorB;
                    Console.SetCursorPosition(col * (str.Length + 1), row);
                    Console.Write(str);

                    col++;
                }
                col = 0;
                row++;
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }
}