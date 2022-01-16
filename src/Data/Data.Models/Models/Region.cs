namespace Data.Models.Models
{
    public class Region : BaseModel
    {
        public ICollection<Country> Countries { get; set; }
    }
}
