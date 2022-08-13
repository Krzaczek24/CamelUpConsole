using System;

namespace CamelUpConsole.Core.Pages
{
    internal abstract class ScrollablePage : Page
    {
        public override bool OverrideRemainingLines => false;

        public int Percent => CurrentLine * 100 / (LinesCount - LinesToRender);
        public int LinesProgress => CurrentLine * LinesCount / (LinesCount - LinesToRender);

        public void Reset() => CurrentLine = 0;
        public void ScrollUp() => CurrentLine = Math.Max(0, CurrentLine - 1);
        public void ScrollDown() => CurrentLine = Math.Min(Lines.Count - LinesToRender, CurrentLine + 1);
        public void PageUp() => CurrentLine = Math.Max(0, CurrentLine - LinesToRender);
        public void PageDown() => CurrentLine = Math.Min(Lines.Count - LinesToRender, CurrentLine + LinesToRender);
    }
}
