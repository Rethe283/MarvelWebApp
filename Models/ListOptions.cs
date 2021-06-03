using System.Collections.Generic;

namespace MarvelWebApp.Models
{
    public class ListOptions
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 15;

        public IDictionary<string, string> ToDictionary() => new Dictionary<string, string>()
        {
            { $"{nameof(Page)}", Page.ToString() },
            { $"{nameof(Size)}", Size.ToString() }
        };
    }
}
