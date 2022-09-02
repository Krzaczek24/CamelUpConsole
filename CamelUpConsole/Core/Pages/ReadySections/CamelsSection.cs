using CamelUpConsole.Enums;
using CamelUpConsole.Mappings;
using CamelUpEngine;
using CamelUpEngine.Core.Enums;
using CamelUpEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace CamelUpConsole.Core.Pages.ReadySections
{
    internal class CamelsSection : Section
    {
        private Game game;

        public CamelsSection(Game game, int x, int y, int width, int height) : base(x, y, width, height, true, "Camels")
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            int bottomRow = Dimensions.Inner.Y + Dimensions.Inner.Height;
            int col = Dimensions.Inner.X + 5;

            foreach (IField field in game.Fields)
            {
                Console.SetCursorPosition(col, bottomRow - 1);
                new LineRenderInfo(field.Index.ToString().PadRight(2).PadLeft(3), TextAligment.None, ConsoleColor.Magenta, ConsoleColor.Black).Render();

                Console.SetCursorPosition(col, bottomRow - 2);
                new LineRenderInfo("[", TextAligment.None, ConsoleColor.DarkMagenta, ConsoleColor.Black).Render();
                new LineRenderInfo(GetAudienceTileChar(field), TextAligment.None, ConsoleColor.Green, ConsoleColor.Black).Render();
                new LineRenderInfo("]", TextAligment.None, ConsoleColor.DarkMagenta, ConsoleColor.Black).Render();

                var camels = field.Camels.ToArray();
                int row = bottomRow - 3;
                foreach (ICamel camel in field.Camels.Reverse())
                {
                    Console.SetCursorPosition(col, row--);
                    new LineRenderInfo("   ", TextAligment.None, background: ColorMapping.Camel[camel.Colour]).Render();
                }

                while (row >= Dimensions.Inner.Y)
                {
                    Console.SetCursorPosition(col, row--);
                    new LineRenderInfo("   ", TextAligment.None, background: ConsoleColor.Black).Render();
                }

                col += 4;
            }         
        }

        private string GetAudienceTileChar(IField field)
        {
            char audienceTile = ' ';
            if (field.AudienceTile != null)
            {
                if (field.AudienceTile.Side == AudienceTileSide.Cheering)
                    audienceTile = '+';
                else
                    audienceTile = '-';
            }
            return audienceTile.ToString();
        }
    }
}
