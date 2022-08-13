using System;

namespace CamelUpConsole.Core.MenuBar.Models
{
    internal class DynamicOption : Option
    {
        private Func<string> dynamicDescription;

        public override string Description => dynamicDescription();

        public DynamicOption(string key, Func<string> description) : base()
        {
            Key = key;
            dynamicDescription = description;
        }
    }
}
