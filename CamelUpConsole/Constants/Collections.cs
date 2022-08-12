using System.Collections.Generic;

namespace CamelUpConsole.Constants
{
    internal static class Collections
    {
        public static IReadOnlyCollection<string> Names = new List<string>()
        {
            "Muhammad", "Ali", "Omar", "Ahmed", "Hassan", "Ibrahim", // male
            "Laila", "Maryam", "Fatima", "Lena", "Zahra", "Salma" // female
        };
    }
}
