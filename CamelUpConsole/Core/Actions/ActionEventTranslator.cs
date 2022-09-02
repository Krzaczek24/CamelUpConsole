using CamelUpEngine.Core.Actions;
using CamelUpEngine.Core.Actions.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CamelUpConsole.Core.Actions
{
    internal static class ActionEventTranslator
    {
        private static IReadOnlyDictionary<string, Func<IActionEvent, ActionEventDescription>> ActionDescriptions = new Dictionary<string, Func<IActionEvent, ActionEventDescription>>()
        {
            [nameof(IAudienceTilePlacementEvent)] = (IActionEvent e) => new ActionEventDescription("All audience tiles has been returned"),
            [nameof(IChangedCurrentPlayerEvent)] = (IActionEvent e) => new ActionEventDescription($"{((IChangedCurrentPlayerEvent)e).NewPlayer.Name}'s turn", ConsoleColor.Magenta),
        };

        public static ActionEventDescription GetPrettyDescription(this IActionEvent actionEvent)
        {
            return ActionDescriptions.TryGetValue($"I{actionEvent.GetType().Name}", out Func<IActionEvent, ActionEventDescription> func) ? func(actionEvent) : null;
        }

        public static IEnumerable<ActionEventDescription> GetPrettyDescription(this IEnumerable<IActionEvent> actionEvents)
        {
            return actionEvents.Select(GetPrettyDescription).Where(e => e != null).ToList();
        }
    }
}
