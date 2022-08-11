using System;

namespace CamelUpConsole.ConsoleHelper.MenuBarModels
{
    internal class Option
    {
        public char Key { get; }
        public string Description { get; }

        public Option(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException($"Parameter [{nameof(description)}] cannot be empty");

            Key = char.ToUpper(description[0]);
            Description = description.Substring(1);
        }

        public Option(char key, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException($"Parameter [{nameof(description)}] cannot be empty");

            Key = char.ToUpper(key);
            Description = description;
        }

        public string GetPrettyDescription(int maxDescriptionLength = 8)
        {
            return Description.PadRight(maxDescriptionLength).Substring(0, maxDescriptionLength);
        }
    }
}
