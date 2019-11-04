using System.Collections.Generic;

namespace Tomatina.ExplorerViews.Data
{
    public class CountryCode
    {
        public string Name { get; set; }
        public string Native { get; set; }
        public string Phone { get; set; }
        public string Continent { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public List<string> Languages { get; set; }
    }
}
