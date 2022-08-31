using CamelUpConsole.Core.Pages.ReadySections;
using CamelUpEngine;
using System.Collections.Generic;

namespace CamelUpConsole.Core.Pages.ReadyPages
{
    internal class GameBoard : Page
    {
        private bool rendered = false;
        private List<Section> sections = new();
        protected override IReadOnlyCollection<LineRenderInfo> Lines => new List<LineRenderInfo>();

        public GameBoard(Game game)
        {
            sections.Add(new PlayersSection(game));
            //sections.Add(new GraphicDicesSection(game));
            sections.Add(new DicesSection(game));
        }

        public override void Render()
        {
            if (!rendered)
            {
                base.Render();
                rendered = true;
            }

            sections.ForEach(section => section.Render());
        }
    }
}
