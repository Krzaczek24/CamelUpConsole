using CamelUpConsole.Core.MenuBar.Models;
using CamelUpConsole.Core.Pages;
using CamelUpConsole.Enums;
using CamelUpConsole.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CamelUpConsole.Core.MenuBar
{
    internal static class MenuBar
    {
        private static readonly IReadOnlyCollection<ConsoleKey> unsupportedKeys = new List<ConsoleKey>() { ConsoleKey.Backspace, ConsoleKey.Tab, ConsoleKey.Enter, ConsoleKey.Escape };
        private const string SELECT_OPTION = "Select option: ";

        public static class Settings
        {
            /// <summary>
            /// Distance in char count between options
            /// </summary>
            public static int Spacing { get; set; } = 1;
            /// <summary>
            /// If non positive then option fields are fitted to option descriptions, otherwise options lenght are constant
            /// </summary>
            public static int MaxLength { get; set; } = 0;

            public static class Colors
            {
                public static class Key
                {
                    public static ConsoleColor Background { get; set; } = ConsoleColor.DarkGray;
                    public static ConsoleColor Foreground { get; set; } = ConsoleColor.White;
                }

                public static class Description
                {
                    public static ConsoleColor Background { get; set; } = ConsoleColor.Cyan;
                    public static ConsoleColor Foreground { get; set; } = ConsoleColor.Black;
                }
            }
        }

        public static void Render(MenuLevels menuLevel)
        {
            Render(MenuMapping.LevelOptions[menuLevel], MenuMapping.LevelOptionsAlignToRight[menuLevel]);
        }

        public static void Render(ScrollablePage scrollablePage, ScrollableProgressType progressType = ScrollableProgressType.Position)
        {
            DynamicOption option = new("Line", GetProgressFunction(scrollablePage, progressType));
            Render(MenuMapping.LevelOptions[MenuLevels.Scrollable].Insert(4, option), MenuMapping.LevelOptionsAlignToRight[MenuLevels.Scrollable]);
        }

        public static void Render(Options options, int optionsToRight = 0)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            new LineRenderInfo(string.Empty.PadRight(Console.WindowWidth - 2, '_'), margin: 1).Render();

            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            LineRenderInfo.BlankLine.Render();

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            new LineRenderInfo(SELECT_OPTION, margin: 1).Render();

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write(string.Empty.PadRight(Console.WindowWidth - 1));

            RenderOptions(options, optionsToRight);
            SetCursorInOptionSelect();
        }

        public static void PrintNoSupportedKeyError(ConsoleKeyInfo keyInfo)
        {
            string keyString;
            if (unsupportedKeys.Contains(keyInfo.Key) || keyInfo.KeyChar == '\0')
                keyString = keyInfo.Key.ToString();
            else
                keyString = keyInfo.KeyChar.ToString();
            PrintError($"There is no action available for [{keyString}] key");
        }
        public static void PrintError(string message) => PrintMessage(message, ConsoleColor.DarkRed);
        public static void PrintWarning(string message) => PrintMessage(message, ConsoleColor.DarkYellow);
        public static void PrintMessage(string message, ConsoleColor color = ConsoleColor.Green)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(1, Console.WindowHeight - 3);
            Console.Write(message.PadRight(Console.WindowWidth));
            SetCursorInOptionSelect();
        }

        public static void SetCursorInOptionSelect()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(SELECT_OPTION.Length + 1, Console.WindowHeight - 2);
        }

        public static void RenderOptions(Options options, int optionsToRight = 0)
        {
            int column = 1;
            int indexOfRightAligment = options.Count - optionsToRight;
            for (int index = 0; index < indexOfRightAligment; index++)
            {
                column += RenderSingleOption(options[index], column) + Settings.Spacing;
            }

            column = Console.WindowWidth - 1;

            for (int index = options.Count - 1; index >= indexOfRightAligment; index--)
            {
                column -= RenderSingleOption(options[index], column, false) + Settings.Spacing;
            }
        }

        private static int RenderSingleOption(Option option, int column, bool alignToLeft = true)
        {
            string description = Settings.MaxLength <= 0 ? option.Description : option.GetPrettyDescription(Settings.MaxLength - 1);

            Console.SetCursorPosition(alignToLeft ? column : column - (description.Length + option.Key.Length), Console.WindowHeight - 1);
            Console.BackgroundColor = Settings.Colors.Key.Background;
            Console.ForegroundColor = Settings.Colors.Key.Foreground;
            Console.Write(option.Key);
            Console.BackgroundColor = Settings.Colors.Description.Background;
            Console.ForegroundColor = Settings.Colors.Description.Foreground;
            Console.Write(description);

            return option.Key.Length + description.Length;
        }

        private static Func<string> GetProgressFunction(ScrollablePage scrollablePage, ScrollableProgressType progressType)
        {
            switch (progressType)
            {
                case ScrollableProgressType.None:
                    return () => string.Empty;
                case ScrollableProgressType.Position:
                    return () => $" {scrollablePage.LinesProgress}/{scrollablePage.LinesCount} ";
                case ScrollableProgressType.Percent:
                    return () => $" {scrollablePage.Percent}% ";
                case ScrollableProgressType.Both:
                    return () => $" {scrollablePage.LinesProgress}/{scrollablePage.LinesCount} ({scrollablePage.Percent}%) ";
                default:
                    throw new NotImplementedException($"Unknown value [{progressType}] of {nameof(ScrollableProgressType)}");
            }
        }
    }
}
