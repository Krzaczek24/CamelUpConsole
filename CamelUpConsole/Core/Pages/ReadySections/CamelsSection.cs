using CamelUpEngine;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class CamelsSection : Section
    {
        private Game game;

        public CamelsSection(Game game) : base(42, 7, 76, 18, true, "Camels")
        {
            this.game = game;
        }
    }
}
