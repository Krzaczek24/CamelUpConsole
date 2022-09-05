using CamelUpEngine.Core.Actions;
using CamelUpEngine.Core.Actions.Events;
using CamelUpEngine.Core.Enums;
using CamelUpEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CamelUpConsole.Core.MenuBar.MenuBar.Settings.Colors;

namespace CamelUpConsole.Core.Actions
{
    internal static class ActionEventTranslator
    {
        private static IReadOnlyDictionary<string, Func<IActionEvent, IEnumerable<ActionEventDescription>>> ActionDescriptions = new Dictionary<string, Func<IActionEvent, IEnumerable<ActionEventDescription>>>()
        {
            [nameof(IAudienceTilePlacementEvent)] = AudienceTilePlacement,
            [nameof(IBetsSummaryEvent)] = BetsSummary,
            [nameof(IBettingEvent)] = Betting,
            [nameof(ICamelsMovedEvent)] = CamelMoved,
            [nameof(ICamelsStoodOnAudienceTileEvent)] = CamelsStoodOnAudienceTile,
            [nameof(IChangedCurrentPlayerEvent)] = ChangedCurrentPlayer,
            [nameof(ICoinsAddedEvent)] = CoinsAdded,
            [nameof(IDiceDrawnEvent)] = DiceDrawn,
            [nameof(IEndOfTurnEvent)] = EndOfTurn,
            [nameof(IGameOverEvent)] = GameOver,
            [nameof(IMadCamelColourSwitchedEvent)] = MadCamelColourSwitched,
            [nameof(ITypingCardDrawnEvent)] = TypingCardDrawn,
            [nameof(ITypingCardsSummaryEvent)] = TypingCardsSummary
        };

        public static IEnumerable<ActionEventDescription> GetPrettyDescription(this IActionEvent actionEvent)
        {
            string key = $"I{actionEvent.GetType().Name}";
            if (ActionDescriptions.ContainsKey(key))
                return ActionDescriptions[key](actionEvent);
            return new List<ActionEventDescription>();
        }

        public static IEnumerable<ActionEventDescription> GetPrettyDescription(this IEnumerable<IActionEvent> actionEvents)
        {
            List<ActionEventDescription> result = new();

            IEnumerable<ActionEventDescription> descriptions = actionEvents.SelectMany(GetPrettyDescription).Where(e => e != null).ToList();
            foreach (ActionEventDescription description in descriptions)
            {
                if (description.IsMultiLine)
                    result.AddRange(description.Text.Split('\n').Select(d => new ActionEventDescription(d, description.ForegroundColor, description.BackgroundColor)));
                else
                    result.Add(description);
            }

            return result;
        }

        private static IEnumerable<ActionEventDescription> BetsSummary(IActionEvent @event)
        {
            var e = (IBetsSummaryEvent)@event;
            var descriptions = new List<ActionEventDescription>() { new ActionEventDescription("Bets summary:", ConsoleColor.Blue) };
            descriptions.Add(new ActionEventDescription("Winner bets:", ConsoleColor.DarkCyan));
            if (e.WinnerRewards.Any())
                descriptions.AddRange(e.WinnerRewards.SelectMany(subEvent => CoinsAdded(subEvent)).ToList());
            else
                descriptions.Add(new ActionEventDescription("No one make a bet", ConsoleColor.Cyan));
            descriptions.AddRange(e.WinnerRewards.SelectMany(subEvent => CoinsAdded(subEvent)).ToList());
            descriptions.Add(new ActionEventDescription("Loser bets:", ConsoleColor.DarkCyan));
            if (e.LoserRewards.Any())
                descriptions.AddRange(e.LoserRewards.SelectMany(subEvent => CoinsAdded(subEvent)).ToList());
            else
                descriptions.Add(new ActionEventDescription("No one make a bet", ConsoleColor.Cyan));
            return descriptions;
        }

        private static IEnumerable<ActionEventDescription> AudienceTilePlacement(IActionEvent @event)
        {
            var e = (IAudienceTilePlacementEvent)@event;

            string player = e.AudienceTile.Owner.Name;
            string tile = e.AudienceTile.Side.ToString().ToLower();
            int fieldIndex = e.FieldIndex;

            return new List<ActionEventDescription>() { new ActionEventDescription($"{player} placed {tile} audience tile on {fieldIndex}. field") };
        }

        private static IEnumerable<ActionEventDescription> Betting(IActionEvent @event)
        {
            var e = (IBettingEvent)@event;

            string player = e.Player.Name;
            string betType = e.BetType.ToString().ToLower();

            return new List<ActionEventDescription>() { new ActionEventDescription($"{player} betted {betType}") };
        }

        private static IEnumerable<ActionEventDescription> CamelMoved(IActionEvent @event)
        {
            var e = (ICamelsMovedEvent)@event;

            StringBuilder camels = new StringBuilder($"{e.Camels.Last().Colour} camel moved");
            if (e.Camels.Count > 1)
                camels.Append($" with {e.Camels.Count} other camels");
            camels.Append($" from {e.FromFieldIndex}. field to {e.ToFieldIndex}. field");

            return new List<ActionEventDescription>() { new ActionEventDescription(camels.ToString()) };
        }

        private static IEnumerable<ActionEventDescription> CamelsStoodOnAudienceTile(IActionEvent @event)
        {
            var e = (ICamelsStoodOnAudienceTileEvent)@event;

            string player = e.AudienceTile.Owner.Name;
            string tile = e.AudienceTile.Side.ToString().ToLower();

            return new List<ActionEventDescription>() { new ActionEventDescription($"Camels stood on {player}'s {tile} audience tile", ConsoleColor.Cyan) };
        }

        private static IEnumerable<ActionEventDescription> ChangedCurrentPlayer(IActionEvent @event)
        {
            var e = (IChangedCurrentPlayerEvent)@event;
            return new List<ActionEventDescription>() { new ActionEventDescription($"{e.NewPlayer.Name}'s turn", ConsoleColor.Magenta) };
        }

        private static IEnumerable<ActionEventDescription> CoinsAdded(IActionEvent @event)
        {
            var e = (ICoinsAddedEvent)@event;

            string player = e.Player.Name;
            string direction = e.CoinsCount >= 0 ? "received" : "lost";
            int coinsCount = Math.Abs(e.CoinsCount);
            string coins = coinsCount == 1 ? "coin" : "coins";

            return new List<ActionEventDescription>() { new ActionEventDescription($"{player} {direction} {coinsCount} {coins}", ConsoleColor.Yellow) };
        }

        private static IEnumerable<ActionEventDescription> DiceDrawn(IActionEvent @event)
        {
            var e = (IDiceDrawnEvent)@event;

            string player = e.Player.Name;
            string color = e.DrawnDice.Colour.ToString().ToLower();
            int value = e.DrawnDice.Value;

            return new List<ActionEventDescription>() { new ActionEventDescription($"{player} drawn {color} dice with value of {value}") };
        }

        private static IEnumerable<ActionEventDescription> EndOfTurn(IActionEvent @event)
        {
            return new List<ActionEventDescription>() { new ActionEventDescription($"End of turn - all dices has been drawn or game is over", ConsoleColor.Magenta) };
        }

        private static IEnumerable<ActionEventDescription> GameOver(IActionEvent @event)
        {
            var e = (IGameOverEvent)@event;

            string firstCamelColor = e.FirstCamel.Colour.ToString().ToLower();
            string lastCamelColor = e.LastCamel.Colour.ToString().ToLower();
            IList<string> players = e.PlayersRanking.Select(p => p.Name.PadRight(14)).ToList();
            IList<string> ranking = new List<string>() { string.Empty, string.Empty, string.Empty, string.Empty };
            int index = 0;
            foreach (string player in players)
                ranking[index++ % ranking.Count] += $"{index}. {player}";
            return new List<ActionEventDescription>()
            {
                new ActionEventDescription($"Game over - fastest was {firstCamelColor} camel and slowest one is {lastCamelColor} camel", ConsoleColor.Red),
                new ActionEventDescription($"Players ranking:", ConsoleColor.Blue),
                new ActionEventDescription(string.Join("\n", ranking), ConsoleColor.DarkCyan)
            };
        }

        private static IEnumerable<ActionEventDescription> MadCamelColourSwitched(IActionEvent @event)
        {
            var e = (IMadCamelColourSwitchedEvent)@event;

            string fromColor = e.From.ToString();
            string toColor = e.To.ToString().ToLower();
            string reason = "DON'T KNOW WHY";
            if (e.SwitchReason == MadCamelColourSwitchReason.OnlyOneMadCamelIsCarryingNonMadCamels)
                reason = "only second one mad camel had non-mad camels on back";
            else if (e.SwitchReason == MadCamelColourSwitchReason.OtherMadCamelIsDirectlyOnBackOfOtherOne)
                reason = "there was other mad camel directly on first ones back";

            return new List<ActionEventDescription>() { new ActionEventDescription($"{fromColor} camel has benn switched to {toColor} camel because\n{reason}", ConsoleColor.Green) };
        }

        private static IEnumerable<ActionEventDescription> TypingCardDrawn(IActionEvent @event)
        {
            var e = (ITypingCardDrawnEvent)@event;

            string player = e.Player.Name;
            string color = e.TypingCard.Colour.ToString().ToLower();
            int value = (int)e.TypingCard.Value;

            return new List<ActionEventDescription>() { new ActionEventDescription($"{player} drawn {color} card with value of {value}") };
        }

        public static IEnumerable<ActionEventDescription> TypingCardsSummary(IActionEvent @event)
        {
            var e = (ITypingCardsSummaryEvent)@event;

            var descriptions = new List<ActionEventDescription>() { new ActionEventDescription("Typing cards summary:", ConsoleColor.Blue) };
            if (e.SubEvents.Any())
                descriptions.AddRange(e.SubEvents.SelectMany(subEvent => CoinsAdded(subEvent)).ToList());
            else
                descriptions.Add(new ActionEventDescription("No one typed turn winner", ConsoleColor.Cyan));

            return descriptions;
        }
    }
}
