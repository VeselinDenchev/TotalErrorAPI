namespace Data.Models.Models
{
    internal class Country : BaseModel
    {
        public Country()
            : base()
        {

        }

        public string Name { get; set; }

        public Region Region { get; set; }

        public string RegionId { get; set; }

        public ICollection<Country> MyProperty { get; set; }
    }
}
