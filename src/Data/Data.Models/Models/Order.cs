namespace Data.Models.Models
{
    using Data.Models.Enums;

    internal class Order : BaseModel
    {
        public OrderPriorities OrderPriority { get; set; }

        public DateTime OrderDate { get; set; }

        public SalesChannels SalesChannel { get; set; }

        public ICollection<Sales> Sales { get; set; }

        public string SaleId { get; set; }

        public Country Country { get; set; }

        public string CountryId { get; set; }
    }
}
