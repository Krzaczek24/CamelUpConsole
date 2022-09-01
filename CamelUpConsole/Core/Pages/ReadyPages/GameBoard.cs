using CamelUpConsole.Core.Pages.ReadySections;
using CamelUpEngine;
using System.Collections.Generic;

namespace CamelUpConsole.Core.Pages.ReadyPages
{
    internal class GameBoard : Page
    {
        private bool rendered = false;
        private List<Section> sections;
        protected override IReadOnlyCollection<LineRenderInfo> Lines => new List<LineRenderInfo>();

        public GameBoard(Game game)
        {
            sections = new List<Section>()
            {
                new PlayersSection(game),
                new DicesSection(game),
                new CardsSection(game),
                new BetsSection(game),
                new CamelsSection(game),
                new HistorySection(game)
            };
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
