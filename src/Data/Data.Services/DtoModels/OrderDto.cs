namespace Data.Services.DtoModels
{
    using Data.Models.Enums;

    public class OrderDto
    {
        public OrderPriorities OrderPriority { get; set; }

        public DateTime OrderDate { get; set; }

        public SalesChannels SalesChannel { get; set; }

        public ICollection<SaleDto> Sales { get; set; }

        public CountryDto Country { get; set; }
    }
}
