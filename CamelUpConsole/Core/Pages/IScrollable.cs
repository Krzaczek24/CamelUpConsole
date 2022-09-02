namespace CamelUpConsole.Core.Pages
{
    internal interface IScrollable
    {
        public int LinesCount { get; }
        public int Percent { get; }
        public int LinesProgress { get; }
    }
}
