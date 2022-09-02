using CamelUpEngine.Core.Actions;
using CamelUpEngine.Core.Actions.Events;
using CamelUpEngine.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CamelUpConsole.Core.Actions
{
    internal static class ActionEventTranslator
    {
        private static IReadOnlyDictionary<string, Func<IActionEvent, ActionEventDescription>> ActionDescriptions = new Dictionary<string, Func<IActionEvent, ActionEventDescription>>()
        {
            [nameof(IAudienceTilePlacementEvent)] = AudienceTilePlacement,
            [nameof(IBettingEvent)] = Betting,
            [nameof(ICamelsMovedEvent)] = CamelMoved,
            [nameof(ICamelsStoodOnAudienceTileEvent)] = CamelsStoodOnAudienceTile,
            [nameof(IChangedCurrentPlayerEvent)] = ChangedCurrentPlayer,
            [nameof(ICoinsAddedEvent)] = CoinsAdded,
            [nameof(IDiceDrawnEvent)] = DiceDrawn,
            [nameof(IEndOfTurnEvent)] = EndOfTurn,
            [nameof(IGameOverEvent)] = GameOver,
            [nameof(IMadCamelColourSwitchedEvent)] = MadCamelColourSwitched,
            [nameof(ITypingCardDrawnEvent)] = TypingCardDrawn
        };

        public static ActionEventDescription GetPrettyDescription(this IActionEvent actionEvent)
        {
            return ActionDescriptions.TryGetValue($"I{actionEvent.GetType().Name}", out Func<IActionEvent, ActionEventDescription> func) ? func(actionEvent) : null;
        }

        public static IEnumerable<ActionEventDescription> GetPrettyDescription(this IEnumerable<IActionEvent> actionEvents)
        {
            List<ActionEventDescription> result = new();

            foreach (ActionEventDescription description in actionEvents.Select(GetPrettyDescription).Where(e => e != null))
            {
                if (description.IsMultiLine)
                    result.AddRange(description.Text.Split('\n').Select(d => new ActionEventDescription(d, description.ForegroundColor, description.BackgroundColor)));
                else
                    result.Add(description);
            }

            return result;
        }

        private static ActionEventDescription AudienceTilePlacement(IActionEvent @event)
        {
            var e = (IAudienceTilePlacementEvent)@event;

            string player = e.AudienceTile.Owner.Name;
            string tile = e.AudienceTile.Side.ToString().ToLower();
            int fieldIndex = e.FieldIndex;

            return new ActionEventDescription($"{player} placed {tile} audience tile on {fieldIndex}. field");
        }

        private static ActionEventDescription Betting(IActionEvent @event)
        {
            var e = (IBettingEvent)@event;

            string player = e.Player.Name;
            string betType = e.BetType.ToString().ToLower();

            return new ActionEventDescription($"{player} betted {betType}");
        }

        private static ActionEventDescription CamelMoved(IActionEvent @event)
        {
            var e = (ICamelsMovedEvent)@event;

            StringBuilder camels = new StringBuilder($"{e.Camels.Last().Colour} camel moved");
            if (e.Camels.Count > 1)
                camels.Append($" with {e.Camels.Count} other camels");
            camels.Append($" from {e.FromFieldIndex}. field to {e.ToFieldIndex}. field");

            return new ActionEventDescription(camels.ToString());
        }

        private static ActionEventDescription CamelsStoodOnAudienceTile(IActionEvent @event)
        {
            var e = (ICamelsStoodOnAudienceTileEvent)@event;

            string player = e.AudienceTile.Owner.Name;
            string tile = e.AudienceTile.Side.ToString().ToLower();

            return new ActionEventDescription($"Camels stood on {player}'s {tile} audience tile", ConsoleColor.Cyan);
        }

        private static ActionEventDescription ChangedCurrentPlayer(IActionEvent @event)
        {
            var e = (IChangedCurrentPlayerEvent)@event;
            return new ActionEventDescription($"{e.NewPlayer.Name}'s turn", ConsoleColor.Magenta);
        }

        private static ActionEventDescription CoinsAdded(IActionEvent @event)
        {
            var e = (ICoinsAddedEvent)@event;

            string player = e.Player.Name;
            string direction = e.CoinsCount > 0 ? "received" : "lost";
            int coins = Math.Abs(e.CoinsCount);

            return new ActionEventDescription($"{player} {direction} {coins} coins", ConsoleColor.Yellow);
        }

        private static ActionEventDescription DiceDrawn(IActionEvent @event)
        {
            var e = (IDiceDrawnEvent)@event;

            string player = e.Player.Name;
            string color = e.DrawnDice.Colour.ToString().ToLower();
            int value = e.DrawnDice.Value;

            return new ActionEventDescription($"{player} drawn {color} dice with value of {value}");
        }

        private static ActionEventDescription EndOfTurn(IActionEvent @event)
        {
            return new ActionEventDescription($"End of turn - all dices has been drawn or game is over", ConsoleColor.Magenta);
        }

        private static ActionEventDescription GameOver(IActionEvent @event)
        {
            var e = (IGameOverEvent)@event;

            string firstCamelColor = e.FirstCamel.Colour.ToString().ToLower();
            string lastCamelColor = e.LastCamel.Colour.ToString().ToLower();
            IList<string> players = e.Winners.Select(p => p.Name.PadRight(14)).ToList();
            IList<string> ranking = new List<string>() { string.Empty, string.Empty, string.Empty, string.Empty };
            int index = 0;
            foreach (string player in players)
                ranking[index++ % ranking.Count] += $"{index}. {player}";
            return new ActionEventDescription($"Game over - fastest was {firstCamelColor} camel and slowest one is {lastCamelColor} camel\nPlayers ranking:\n{string.Join("\n", ranking)}", ConsoleColor.Blue);
        }

        private static ActionEventDescription MadCamelColourSwitched(IActionEvent @event)
        {
            var e = (IMadCamelColourSwitchedEvent)@event;

            string fromColor = e.From.ToString();
            string toColor = e.To.ToString().ToLower();
            string reason = "DON'T KNOW WHY";
            if (e.SwitchReason == MadCamelColourSwitchReason.OnlyOneMadCamelIsCarryingNonMadCamels)
                reason = "only second one mad camel had non-mad camels on back";
            else if (e.SwitchReason == MadCamelColourSwitchReason.OtherMadCamelIsDirectlyOnBackOfOtherOne)
                reason = "there was other mad camel directly on first ones back";

            return new ActionEventDescription($"{fromColor} camel has benn switched to {toColor} camel because\n{reason}", ConsoleColor.Green);
        }

        private static ActionEventDescription TypingCardDrawn(IActionEvent @event)
        {
            var e = (ITypingCardDrawnEvent)@event;

            string player = e.Player.Name;
            string color = e.TypingCard.Colour.ToString().ToLower();
            int value = (int)e.TypingCard.Value;

            return new ActionEventDescription($"{player} drawn {color} card with value of {value}");
        }
    }
}
