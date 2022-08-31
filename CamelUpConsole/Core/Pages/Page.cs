using System;
using System.Collections.Generic;
using System.Linq;

namespace CamelUpConsole.Core.Pages
{
    internal abstract class Page
    {
        public int CurrentLine { get; protected set; } = 0;
        public int LinesCount => Lines.Count;
        public int LinesToRender = 26;
        public virtual bool OverrideRemainingLines { get; set; } = true;

        protected abstract IReadOnlyCollection<LineRenderInfo> Lines { get; }

        public virtual void Render()
        {
            Console.SetCursorPosition(0, 0);
            var linesToRender = Lines.Skip(CurrentLine).Take(LinesToRender).ToList();
            linesToRender.ForEach(line => line.Render());

            if (OverrideRemainingLines && LinesCount < LinesToRender)
            {
                for (int i = LinesCount; i < LinesToRender; i++)
                {
                    LineRenderInfo.BlankLine.Render();
                }
            }
        }
    }
}
