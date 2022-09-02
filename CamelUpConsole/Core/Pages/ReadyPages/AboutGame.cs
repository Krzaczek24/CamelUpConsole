using System;
using System.Collections.Generic;

namespace CamelUpConsole.Core.Pages.ReadyPages
{
    internal class AboutGame : ScrollablePage
    {
        //https://patorjk.com/software/taag/#p=display&h=1&v=0&f=Small&t=Camel%20Up%0AInvented%20by%0ASteffen%20Bogen%0ADeveloped%20by%0ATomasz%20Drewek%0AProject%20start%20date%0A16-07-2022%0ARelease%20date%0A13-08-2022%0AVersion%0Av1.0.0%0AGitHub%20profile%0AKrzaczek24%0ARepository%0ACamelUpConsole%0AThank%20You

        protected override IReadOnlyCollection<LineRenderInfo> Lines { get; } = new List<LineRenderInfo>()
        {
            LineRenderInfo.BlankLine,
            new LineRenderInfo("  ___                    _   _   _       ", Enums.TextAligment.Center, ConsoleColor.DarkYellow, margin: 1),
            new LineRenderInfo(" / __| __ _  _ __   ___ | | | | | | _ __ ", Enums.TextAligment.Center, ConsoleColor.DarkYellow, margin: 1),
            new LineRenderInfo("| (__ / _` || '  \\ / -_)| | | |_| || '_ \\", Enums.TextAligment.Center, ConsoleColor.DarkYellow, margin: 1),
            new LineRenderInfo(" \\___|\\__,_||_|_|_|\\___||_|  \\___/ | .__/", Enums.TextAligment.Center, ConsoleColor.DarkYellow, margin: 1),
            new LineRenderInfo("                                   |_|   ", Enums.TextAligment.Center, ConsoleColor.DarkYellow, margin: 1),
            LineRenderInfo.BlankLine,

            new LineRenderInfo(" ___                      _            _   _          ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("|_ _| _ _ __ __ ___  _ _ | |_  ___  __| | | |__  _  _ ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo(" | | | ' \\\\ V // -_)| ' \\|  _|/ -_)/ _` | | '_ \\| || |", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("|___||_||_|\\_/ \\___||_||_|\\__|\\___|\\__,_| |_.__/ \\_, |", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("                                                 |__/ ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),

            new LineRenderInfo(" ___  _          __   __              ___                       ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("/ __|| |_  ___  / _| / _| ___  _ _   | _ ) ___  __ _  ___  _ _  ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("\\__ \\|  _|/ -_)|  _||  _|/ -_)| ' \\  | _ \\/ _ \\/ _` |/ -_)| ' \\ ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("|___/ \\__|\\___||_|  |_|  \\___||_||_| |___/\\___/\\__, |\\___||_||_|", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("                                               |___/            ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),

            new LineRenderInfo(" ___                  _                     _   _          ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("|   \\  ___ __ __ ___ | | ___  _ __  ___  __| | | |__  _  _ ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("| |) |/ -_)\\ V // -_)| |/ _ \\| '_ \\/ -_)/ _` | | '_ \\| || |", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("|___/ \\___| \\_/ \\___||_|\\___/| .__/\\___|\\__,_| |_.__/ \\_, |", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("                             |_|                      |__/ ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),

            new LineRenderInfo(" _____                            ___                          _   ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("|_   _|___  _ __   __ _  ___ ___ |   \\  _ _  ___ __ __ __ ___ | |__", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("  | | / _ \\| '  \\ / _` |(_-<|_ / | |) || '_|/ -_)\\ V  V // -_)| / /", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("  |_| \\___/|_|_|_|\\__,_|/__//__| |___/ |_|  \\___| \\_/\\_/ \\___||_\\_\\", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("                                                                   ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),

            new LineRenderInfo(" ___             _           _         _               _        _        _        ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("| _ \\ _ _  ___  (_) ___  __ | |_   ___| |_  __ _  _ _ | |_   __| | __ _ | |_  ___ ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("|  _/| '_|/ _ \\ | |/ -_)/ _||  _| (_-<|  _|/ _` || '_||  _| / _` |/ _` ||  _|/ -_)", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("|_|  |_|  \\___/_/ |\\___|\\__| \\__| /__/ \\__|\\__,_||_|   \\__| \\__,_|\\__,_| \\__|\\___|", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("              |__/                                                                ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),

            new LineRenderInfo(" _   __        __  ____     ___  __  ___  ___ ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("/ | / /  ___  /  \\|__  |___|_  )/  \\|_  )|_  )", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("| |/ _ \\|___|| () | / /|___|/ /| () |/ /  / / ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("|_|\\___/      \\__/ /_/     /___|\\__//___|/___|", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("                                              ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),

            new LineRenderInfo(" ___       _                          _        _        ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("| _ \\ ___ | | ___  __ _  ___ ___   __| | __ _ | |_  ___ ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("|   // -_)| |/ -_)/ _` |(_-</ -_) / _` |/ _` ||  _|/ -_)", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("|_|_\\\\___||_|\\___|\\__,_|/__/\\___| \\__,_|\\__,_| \\__|\\___|", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("                                                        ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),

            new LineRenderInfo(" _  ____       __   ___      ___  __  ___  ___ ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("/ ||__ / ___  /  \\ ( _ ) ___|_  )/  \\|_  )|_  )", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("| | |_ \\|___|| () |/ _ \\|___|/ /| () |/ /  / / ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("|_||___/      \\__/ \\___/    /___|\\__//___|/___|", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("                                               ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),

            new LineRenderInfo("__   __              _            ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("\\ \\ / /___  _ _  ___(_) ___  _ _  ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo(" \\ V // -_)| '_|(_-<| |/ _ \\| ' \\ ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("  \\_/ \\___||_|  /__/|_|\\___/|_||_|", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("                                  ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),

            new LineRenderInfo("      _     __     __  ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("__ __/ |   /  \\   /  \\ ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("\\ V /| | _| () |_| () |", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo(" \\_/ |_|(_)\\__/(_)\\__/ ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("                       ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),

            new LineRenderInfo("  ___  _  _    _  _        _                       __  _  _      ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo(" / __|(_)| |_ | || | _  _ | |__   _ __  _ _  ___  / _|(_)| | ___ ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("| (_ || ||  _|| __ || || || '_ \\ | '_ \\| '_|/ _ \\|  _|| || |/ -_)", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo(" \\___||_| \\__||_||_| \\_,_||_.__/ | .__/|_|  \\___/|_|  |_||_|\\___|", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("                                 |_|                             ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),

            new LineRenderInfo(" _  __                             _    ___  _ _  ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("| |/ / _ _  ___ __ _  __  ___ ___ | |__|_  )| | | ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("| ' < | '_||_ // _` |/ _||_ // -_)| / / / / |_  _|", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("|_|\\_\\|_|  /__|\\__,_|\\__|/__|\\___||_\\_\\/___|  |_| ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("                                                  ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),

            new LineRenderInfo(" ___                      _  _                   ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("| _ \\ ___  _ __  ___  ___(_)| |_  ___  _ _  _  _ ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("|   // -_)| '_ \\/ _ \\(_-<| ||  _|/ _ \\| '_|| || |", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("|_|_\\\\___|| .__/\\___//__/|_| \\__|\\___/|_|   \\_, |", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),
            new LineRenderInfo("          |_|                               |__/ ", Enums.TextAligment.Left, ConsoleColor.Green, margin: 1),

            new LineRenderInfo("  ___                    _  _   _         ___                      _      ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo(" / __| __ _  _ __   ___ | || | | | _ __  / __| ___  _ _   ___ ___ | | ___ ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("| (__ / _` || '  \\ / -_)| || |_| || '_ \\| (__ / _ \\| ' \\ (_-</ _ \\| |/ -_)", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo(" \\___|\\__,_||_|_|_|\\___||_| \\___/ | .__/ \\___|\\___/|_||_|/__/\\___/|_|\\___|", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),
            new LineRenderInfo("                                  |_|                                     ", Enums.TextAligment.Right, ConsoleColor.Magenta, margin: 1),

            LineRenderInfo.BlankLine,
            new LineRenderInfo(" _____  _                 _    __   __          ", Enums.TextAligment.Center, ConsoleColor.DarkYellow, margin: 1),
            new LineRenderInfo("|_   _|| |_   __ _  _ _  | |__ \\ \\ / /___  _  _ ", Enums.TextAligment.Center, ConsoleColor.DarkYellow, margin: 1),
            new LineRenderInfo("  | |  | ' \\ / _` || ' \\ | / /  \\ V // _ \\| || |", Enums.TextAligment.Center, ConsoleColor.DarkYellow, margin: 1),
            new LineRenderInfo("  |_|  |_||_|\\__,_||_||_||_\\_\\   |_| \\___/ \\_,_|", Enums.TextAligment.Center, ConsoleColor.DarkYellow, margin: 1),
            new LineRenderInfo("                                                ", Enums.TextAligment.Center, ConsoleColor.DarkYellow, margin: 1),
            LineRenderInfo.BlankLine,
        };
    }
}
