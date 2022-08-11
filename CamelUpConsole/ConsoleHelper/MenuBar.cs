﻿using CamelUpConsole.ConsoleHelper.MenuBarModels;
using System;

namespace CamelUpConsole.ConsoleHelper
{
    internal static class MenuBar
    {
        private const string SELECT_OPTION = "Select option: ";

        public static int OptionsSpacing { get; set; } = 1;
        public static int OptionsMaxLength { get; set; } = 12;

        public static void Render(Options options, int optionsToRight = 0)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            Console.Write(string.Empty.PadRight(Console.WindowWidth, '_'));

            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write(string.Empty.PadRight(Console.WindowWidth));

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.Write(SELECT_OPTION.PadRight(Console.WindowWidth));

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write(string.Empty.PadRight(Console.WindowWidth));

            RenderOptions(options, optionsToRight);
            SetCursorInOptionSelect();
        }

        public static void PrintMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write(message.PadRight(Console.WindowWidth));
        }

        public static void SetCursorInOptionSelect()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(SELECT_OPTION.Length, Console.WindowHeight - 2);
        }

        public static void RenderOptions(Options options, int optionsToRight = 0)
        {
            int column = 0;
            int indexOfRightAligment = options.Count - optionsToRight;
            for (int index = 0; index < indexOfRightAligment; index++)
            {
                column += RenderSingleOption(options[index], column) + OptionsSpacing;
            }

            //column = Console.WindowWidth - optionsToRight * (OptionsMaxLength + OptionsSpacing) + 1;
            column = Console.WindowWidth;

            for (int index = options.Count - 1; index >= indexOfRightAligment; index--)
            {
                column -= RenderSingleOption(options[index], column, false) + OptionsSpacing;
            }
        }

        private static int RenderSingleOption(Option option, int column, bool alignToLeft = true)
        {
            string description = OptionsMaxLength <= 0 ? option.Description : option.GetPrettyDescription(OptionsMaxLength - 1);

            Console.SetCursorPosition(alignToLeft ? column : column - (description.Length + 1), Console.WindowHeight - 1);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(option.Key);
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(description);

            return description.Length + 1;
        }
    }
}