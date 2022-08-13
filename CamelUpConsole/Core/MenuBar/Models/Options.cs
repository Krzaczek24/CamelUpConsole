using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CamelUpConsole.Core.MenuBar.Models
{
    internal class Options : IEnumerable<Option>
    {
        private List<Option> entries;
        public int Count => entries.Count;

        public IReadOnlyCollection<string> Keys => entries.Select(entry => entry.Key).ToList();

        public Options(params Option[] options)
        {
            CreateOptions(options);
        }

        public Options(params string[] options)
        {
            CreateOptions(options.Select(option => new Option(option)).ToArray());
        }

        public Options(params object[] options)
        {
            CreateOptions(options.Select(option =>
            {
                if (option is string)
                    return new Option((string)option);
                else if (option is Option)
                    return (Option)option;
                else
                    return null;
            }).ToArray());
        }

        public Options Replace(int index, Option option)
        {
            Option oldOption = entries.SingleOrDefault(entry => entry.Key == option.Key);
            if (oldOption != null && entries.IndexOf(oldOption) != index)
                throw new ArgumentException("All option keys must be unique");

            var result = entries.ToList();
            result.RemoveAt(index);
            result.Insert(index, option);
            return new(result.ToArray());
        }

        public Options Insert(int index, Option option)
        {
            Option oldOption = entries.SingleOrDefault(entry => entry.Key == option.Key);
            if (oldOption != null)
                throw new ArgumentException("All option keys must be unique");

            var result = entries.ToList();
            result.Insert(index, option);
            return new(result.ToArray());
        }

        public Options Add(Option option)
        {
            Option oldOption = entries.SingleOrDefault(entry => entry.Key == option.Key);
            if (oldOption != null)
                throw new ArgumentException("All option keys must be unique");

            var result = entries.ToList();
            result.Add(option);
            return new(result.ToArray());
        }

        public Option this[int index] => entries.ElementAt(index);
        public string this[char key] => this[key.ToString()];
        public string this[string key] => entries.Single(option => option.Key.ToUpper() == key.ToUpper()).Description;

        private void CreateOptions(params Option[] options)
        {
            options = options.Where(option => option != null).ToArray();

            var keys = options.Select(option => option.Key);
            var uniqueKeys = keys.Select(key => key.ToUpper()).Distinct();
            if (keys.Count() != uniqueKeys.Count())
                throw new ArgumentException("All option keys must be unique");

            entries = new(options);
        }

        public IEnumerator<Option> GetEnumerator() => entries.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => entries.GetEnumerator();
    }
}
