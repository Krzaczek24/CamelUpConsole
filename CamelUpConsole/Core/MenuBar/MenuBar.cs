using CamelUpConsole.Core.MenuBar.Models;
using CamelUpConsole.Core.Pages;
using CamelUpConsole.Enums;
using CamelUpConsole.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CamelUpConsole.Core.MenuBar
{
    internal static class MenuBar
    {
        private const string DEFAULT_SELECTION_TEXT = "Select option: ";
        private static readonly IReadOnlyCollection<ConsoleKey> unsupportedKeys = new List<ConsoleKey>() { ConsoleKey.Backspace, ConsoleKey.Tab, ConsoleKey.Enter, ConsoleKey.Escape };

        public static void Render(MenuLevels menuLevel)
        {
            Render(MenuMapping.LevelOptions[menuLevel], MenuMapping.LevelOptionsAlignToRight[menuLevel]);
        }

        public static void Render(Options options, MenuLevels menuLevel) => Render(options, MenuMapping.LevelOptionsAlignToRight[menuLevel]);
        public static void Render(Options options, int optionsToRight = 0)
        {
            RenderFrame();
            RenderSelectOption();
            RenderOptions(options, optionsToRight);
            SetCursorInOptionSelect();
        }

        public static void PrintNoSupportedKeyError(ConsoleKeyInfo keyInfo)
        {
            string keyString;
            if (unsupportedKeys.Contains(keyInfo.Key) || keyInfo.KeyChar == '\0')
                keyString = keyInfo.Key.ToString();
            else
                keyString = keyInfo.KeyChar.ToString().ToUpper();
            PrintError($"There is no available action for [{keyString}] key");
        }
        public static void PrintError(string message) => PrintMessage(message, ConsoleColor.DarkRed);
        public static void PrintWarning(string message) => PrintMessage(message, ConsoleColor.DarkYellow);
        public static void PrintMessage(string message, ConsoleColor color = ConsoleColor.Green)
        {
            int insideLength = Console.WindowWidth - 2 * (Settings.Margin + 1);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(Settings.Margin + 1, Console.WindowHeight - 3);
            Console.Write(message.PadRight(insideLength).Substring(0, insideLength));
            SetCursorInOptionSelect();
        }

        public static void SetCursorInOptionSelect()
        {
            Console.SetCursorPosition(Settings.Margin + 1 + Settings.SelectionText.Length, Console.WindowHeight - 2);
        }

        public static void RenderFrame()
        {
            if (Settings.FrameType != MenuFrameType.None)
            {
                Console.BackgroundColor = Settings.Colors.Frame.Background;
                Console.ForegroundColor = Settings.Colors.Frame.Foreground;

                if (Settings.FrameType == MenuFrameType.Frame)
                {
                    int insideLength = Console.WindowWidth - 2 * Settings.Margin - 1;

                    Console.SetCursorPosition(0, Console.WindowHeight - 4);
                    new LineRenderInfo(string.Empty.PadRight(Console.WindowWidth, '_'), TextAligment.Center, margin: Settings.Margin + 1).Render();

                    for (int row = Console.WindowHeight - 3; row < Console.WindowHeight; row++)
                    {
                        Console.SetCursorPosition(0, row);
                        new LineRenderInfo("|".PadRight(insideLength) + "|", TextAligment.Center, margin: Settings.Margin).Render();
                    }
                }
                else if (Settings.FrameType == MenuFrameType.Line)
                {
                    Console.SetCursorPosition(0, Console.WindowHeight - 4);
                    new LineRenderInfo(string.Empty.PadRight(Console.WindowWidth, (char)713), TextAligment.Left).Render();

                    for (int row = Console.WindowHeight - 3; row < Console.WindowHeight; row++)
                    {
                        Console.SetCursorPosition(0, row);
                        new LineRenderInfo(string.Empty.PadRight(Console.WindowWidth), TextAligment.Left, margin: Settings.Margin).Render();
                    }
                }
            }
        }

        public static void RenderSelectOption()
        {
            Console.BackgroundColor = Settings.Colors.SelectionText.Background;
            Console.ForegroundColor = Settings.Colors.SelectionText.Foreground;

            Console.SetCursorPosition(Settings.Margin + 1, Console.WindowHeight - 2);
            new LineRenderInfo(Settings.SelectionText, TextAligment.None).Render();
        }

        public static void RenderOptions(Options options, int optionsToRight = 0)
        {
            if (optionsToRight < 0)
            {
                optionsToRight = options.Count;
            }   

            int column = Settings.Margin + Settings.Spacing;
            int indexOfRightAligment = options.Count - optionsToRight;
            for (int index = 0; index < indexOfRightAligment; index++)
            {
                column += RenderSingleOption(options[index], column) + Settings.Spacing;
            }

            column = Console.WindowWidth - Settings.Margin - Settings.Spacing;

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

        public static Func<string> GetProgressFunction(IScrollable scrollable, ScrollableProgressType progressType = ScrollableProgressType.Position)
        {
            switch (progressType)
            {
                case ScrollableProgressType.None:
                    return () => string.Empty;
                case ScrollableProgressType.Position:
                    return () => $" {scrollable.LinesProgress}/{scrollable.LinesCount} ";
                case ScrollableProgressType.Percent:
                    return () => $" {scrollable.Percent}% ";
                case ScrollableProgressType.Both:
                    return () => $" {scrollable.LinesProgress}/{scrollable.LinesCount} ({scrollable.Percent}%) ";
                default:
                    throw new NotImplementedException($"Unknown value [{progressType}] of {nameof(ScrollableProgressType)}");
            }
        }

        public static class Settings
        {
            /// <summary>
            /// Distance in char count between options
            /// </summary>
            public static int Spacing { get => spacing; set => spacing = Math.Max(value, 0); }
            private static int spacing = 1;
            /// <summary>
            /// If non positive then option fields are fitted to option descriptions, otherwise options lenght are constant
            /// </summary>
            public static int MaxLength { get; set; } = 0;
            /// <summary>
            /// Distance between menu and console screen edges
            /// </summary>
            public static int Margin { get => FrameType == MenuFrameType.Frame ? margin : margin - 1; set => margin = Math.Max(value, 1); }
            private static int margin = 1;
            /// <summary>
            /// 
            /// </summary>
            public static string SelectionText { get; set; } = DEFAULT_SELECTION_TEXT;
            /// <summary>
            /// 
            /// </summary>
            public static MenuFrameType FrameType { get; set; } = MenuFrameType.Line;

            public static void ResetSelectionText() => SelectionText = DEFAULT_SELECTION_TEXT;

            public static class Colors
            {
                public static class Frame
                {
                    public static ConsoleColor Background { get; set; } = ConsoleColor.Black;
                    public static ConsoleColor Foreground { get; set; } = ConsoleColor.DarkYellow;
                }

                public static class SelectionText
                {
                    public static ConsoleColor Background { get; set; } = ConsoleColor.Black;
                    public static ConsoleColor Foreground { get; set; } = ConsoleColor.White;
                }

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
    }
}
