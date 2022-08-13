using CamelUpConsole.Enums;
using System;
using System.Collections.Generic;

namespace CamelUpConsole.Core.Pages.ReadyPages
{
    internal class Intro : Page
    {
        protected override IReadOnlyCollection<LineRenderInfo> Lines { get; } = new List<LineRenderInfo>()
        {
            LineRenderInfo.BlankLine,
            LineRenderInfo.BlankLine,
            new LineRenderInfo("    ________                                                          _____           ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("   /\\\\\\\\\\\\\\\\\\                                                        /\\\\\\\\\\\\          ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo(" /\\\\\\////////                                                        \\////\\\\\\         ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("/\\\\\\/               ________          ____    ____                       \\/\\\\\\        ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("/\\\\\\               /\\\\\\\\\\\\\\\\\\        /\\\\\\\\\\__/\\\\\\\\\\__      /\\\\\\\\\\\\\\\\      \\/\\\\\\       ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("\\/\\\\\\              \\////////\\\\\\     /\\\\\\///\\\\\\\\\\///\\\\\\    /\\\\\\/////\\\\\\     \\/\\\\\\      ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo(" \\//\\\\\\               /\\\\\\\\\\\\\\\\\\\\   \\/\\\\\\ \\//\\\\\\  \\/\\\\\\   /\\\\\\\\\\\\\\\\\\\\\\      \\/\\\\\\     ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("   \\///\\\\\\            /\\\\\\/////\\\\\\   \\/\\\\\\  \\/\\\\\\  \\/\\\\\\  \\//\\\\///////       \\/\\\\\\___ ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("      \\////\\\\\\\\\\\\\\\\\\  \\//\\\\\\\\\\\\\\\\/\\\\  \\/\\\\\\  \\/\\\\\\  \\/\\\\\\   \\//\\\\\\\\\\\\\\\\\\\\   /\\\\\\\\\\\\\\\\\\", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("          \\/////////    \\////////\\//   \\///   \\///   \\///     \\//////////   \\/////////", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("                             /\\\\\\        /\\\\\\                                         ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("                             \\/\\\\\\       \\/\\\\\\      ________                          ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("                              \\/\\\\\\       \\/\\\\\\    /\\\\\\\\\\\\\\\\\\_                        ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("                               \\/\\\\\\       \\/\\\\\\   /\\\\\\/////\\\\\\                       ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("                                \\/\\\\\\       \\/\\\\\\  \\/\\\\\\\\\\\\\\\\\\\\                       ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("                                 \\/\\\\\\       \\/\\\\\\  \\/\\\\\\//////                       ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("                                  \\//\\\\\\______/\\\\\\   \\/\\\\\\                            ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("                                    \\///\\\\\\\\\\\\\\\\\\/    \\/\\\\\\                           ", TextAligment.Center, ConsoleColor.DarkYellow),
            new LineRenderInfo("                                       \\/////////      \\///                           ", TextAligment.Center, ConsoleColor.DarkYellow),
            LineRenderInfo.BlankLine,
            LineRenderInfo.BlankLine,
            new LineRenderInfo("Camel Up game invented by Steffen Bogen, console version developed by Tomasz 'Krzaczek' Drewek", TextAligment.Center, ConsoleColor.Magenta)
        };
    }
}
