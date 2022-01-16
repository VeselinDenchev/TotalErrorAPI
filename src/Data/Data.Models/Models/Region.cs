namespace Data.Models.Models
{
    internal class Region : BaseModel
    {
        public ICollection<Country> Countries { get; set; }

        public string CountryId { get; set; }
    }
}
