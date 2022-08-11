using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CamelUpConsole.ConsoleHelper.MenuBarModels
{
    internal class Options : IEnumerable<Option>
    {
        private List<Option> entries;
        public int Count => entries.Count;

        public IReadOnlyCollection<ConsoleKey> AvailableKeys
        {
            get
            {
                List<ConsoleKey> keys = new();
                char c;
                foreach (Option option in entries)
                {
                    c = char.ToUpper(option.Key);
                    if (Enum.TryParse($"{c}", out ConsoleKey key)
                    || Enum.TryParse($"D{c}", out key)
                    || Enum.TryParse($"F{c}", out key))
                        keys.Add(key);
                }
                return keys;
            }
        }

        public Options(params Option[] options)
        {
            CreateOptions(options);
        }

        public Options(params string[] options)
        {
            CreateOptions(options.Select(option => new Option(option)).ToArray());
        }

        public Option this[int index]
        {
            get => entries.ElementAt(index);
            set
            {
                Option option = entries.SingleOrDefault(entry => entry.Key == value.Key);
                if (option != null && entries.IndexOf(option) != index)
                    throw new ArgumentException("All option keys must be unique");

                entries.RemoveAt(index);
                entries.Insert(index, value);
            }
        }

        public string this[char key]
        {
            get => entries.Single(option => option.Key == key).Description;
            set
            {
                var option = entries.SingleOrDefault(option => option.Key == char.ToUpper(key));
                if (option != null)
                {
                    int index = entries.IndexOf(option);
                    entries.RemoveAt(index);
                    entries.Insert(index, new Option(char.ToUpper(key), value));
                }
                else
                {
                    entries.Add(new Option(char.ToUpper(key), value));
                }
            }
        }

        private void CreateOptions(params Option[] options)
        {
            options = options.Where(option => option != null).ToArray();

            var keys = options.Select(option => option.Key);
            var uniqueKeys = keys.Select(key => char.ToUpper(key)).Distinct();
            if (keys.Count() != uniqueKeys.Count())
                throw new ArgumentException("All option keys must be unique");

            entries = new(options);
        }

        public IEnumerator<Option> GetEnumerator() => entries.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => entries.GetEnumerator();
    }
}
