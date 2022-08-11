using System;

namespace CamelUpConsole.ConsoleHelper
{
    internal static class Intro
    {
        public static void Render()
        {
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                     ________                                                          _____                            ");
            Console.WriteLine("                    /\\\\\\\\\\\\\\\\\\                                                        /\\\\\\\\\\\\                           ");
            Console.WriteLine("                  /\\\\\\////////                                                        \\////\\\\\\                          ");
            Console.WriteLine("                 /\\\\\\/               ________          ____    ____                       \\/\\\\\\                         ");
            Console.WriteLine("                 /\\\\\\               /\\\\\\\\\\\\\\\\\\        /\\\\\\\\\\__/\\\\\\\\\\__      /\\\\\\\\\\\\\\\\      \\/\\\\\\                        ");
            Console.WriteLine("                 \\/\\\\\\              \\////////\\\\\\     /\\\\\\///\\\\\\\\\\///\\\\\\    /\\\\\\/////\\\\\\     \\/\\\\\\                       ");
            Console.WriteLine("                  \\//\\\\\\               /\\\\\\\\\\\\\\\\\\\\   \\/\\\\\\ \\//\\\\\\  \\/\\\\\\   /\\\\\\\\\\\\\\\\\\\\\\      \\/\\\\\\                      ");
            Console.WriteLine("                    \\///\\\\\\            /\\\\\\/////\\\\\\   \\/\\\\\\  \\/\\\\\\  \\/\\\\\\  \\//\\\\///////       \\/\\\\\\___                  ");
            Console.WriteLine("                       \\////\\\\\\\\\\\\\\\\\\  \\//\\\\\\\\\\\\\\\\/\\\\  \\/\\\\\\  \\/\\\\\\  \\/\\\\\\   \\//\\\\\\\\\\\\\\\\\\\\   /\\\\\\\\\\\\\\\\\\                 ");
            Console.WriteLine("                           \\/////////    \\////////\\//   \\///   \\///   \\///     \\//////////   \\/////////                 ");
            Console.WriteLine("                                              /\\\\\\        /\\\\\\                                                          ");
            Console.WriteLine("                                              \\/\\\\\\       \\/\\\\\\      ________                                           ");
            Console.WriteLine("                                               \\/\\\\\\       \\/\\\\\\    /\\\\\\\\\\\\\\\\\\_                                         ");
            Console.WriteLine("                                                \\/\\\\\\       \\/\\\\\\   /\\\\\\/////\\\\\\                                        ");
            Console.WriteLine("                                                 \\/\\\\\\       \\/\\\\\\  \\/\\\\\\\\\\\\\\\\\\\\                                        ");
            Console.WriteLine("                                                  \\/\\\\\\       \\/\\\\\\  \\/\\\\\\//////                                        ");
            Console.WriteLine("                                                   \\//\\\\\\______/\\\\\\   \\/\\\\\\                                             ");
            Console.WriteLine("                                                     \\///\\\\\\\\\\\\\\\\\\/    \\/\\\\\\                                            ");
            Console.WriteLine("                                                        \\/////////      \\///                                            ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                                                                                        ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("             Camel Up! game invented by Jack Sparrow, console version developed by Tomasz 'Krzaczek' Drewek             ");
        }
    }
}
