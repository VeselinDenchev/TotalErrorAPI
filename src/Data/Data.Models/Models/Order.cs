namespace Data.Models.Models
{
    using Data.Models.Enums;

    public class Order : BaseModel
    {
        public string OrderPriority { get; set; }

        public DateTime OrderDate { get; set; }

        public string SalesChannel { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public Country Country { get; set; }

        //public string CountryId { get; set; }
    }
}
