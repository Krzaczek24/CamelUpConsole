using CamelUpConsole.ConsoleHelper;
using CamelUpConsole.Enums;
using CamelUpConsole.Mappings;
using System;
using System.Linq;

namespace CamelUpConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Camel Up";
            Intro.Render();

            ConsoleKey key = ConsoleKey.Q;
            var options = MenuMapping.LevelOptions[MenuLevels.ActionChoose];
            MenuBar.Render(options, 1);
            do
            {
                if (!options.AvailableKeys.Contains(key))
                {
                    MenuBar.PrintMessage($"There is no action available for [{key.ToString().ToUpper()}] key");
                    MenuBar.SetCursorInOptionSelect();
                }
            }
            while ((key = Console.ReadKey().Key) != ConsoleKey.Q);

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