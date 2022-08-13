using System;

namespace CamelUpConsole.Core.MenuBar.Models
{
    internal class Option
    {
        public string Key { get; protected set; }
        public virtual string Description { get; }

        public Option(string description)
        {
            ValidateDescription(description);
            Key = char.ToUpper(description[0]).ToString();
            Description = description.Substring(1);
        }

        public Option(char key, string description)
        {
            ValidateDescription(description);
            Key = char.ToUpper(key).ToString();
            Description = description;
        }

        public Option(string key, string description)
        {
            ValidateDescription(description);
            Key = key;
            Description = description;
        }

        protected Option() { }

        protected void ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException($"Parameter [{nameof(description)}] cannot be empty");
        }

        public string GetPrettyDescription(int maxDescriptionLength = 8)
        {
            return Description.PadRight(maxDescriptionLength).Substring(0, maxDescriptionLength);
        }
    }
}
