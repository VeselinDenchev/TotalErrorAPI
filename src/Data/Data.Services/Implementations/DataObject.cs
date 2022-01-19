using Data.Models.Models;

namespace Data.Services.Implementations
{
    public class DataObject
    {
        public HashSet<Country> Countries { get; set; }

        public HashSet<ItemType> ItemTypes { get; set; }

        public List<string> LastReadFiles { get; set; }

        public HashSet<Order> Orders { get; set; }

        public HashSet<Region> Regions { get; set; }

        public HashSet<Sale> Sales { get; set; }
    }
}
