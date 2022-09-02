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
                new PlayersSection(game, 2, 1, 38, 13),
                new DicesSection(game, 2, 15, 11, 9, true),
                new CardsSection(game, 14, 15, 11, 9, true),
                new BetsSection(game, 26, 15, 14, 6, true),
                new CamelsSection(game, 42, 1, 76, 13),
                new HistorySection(game, 42, 15, 76, 11)
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
