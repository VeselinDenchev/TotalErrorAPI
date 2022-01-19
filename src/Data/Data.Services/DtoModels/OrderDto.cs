namespace Data.Services.DtoModels
{
    public class OrderDto
    {
        public string OrderPriority { get; set; }

        public DateTime OrderDate { get; set; }

        public string SalesChannel { get; set; }

        public ICollection<SaleDto> Sales { get; set; }

        public CountryDto Country { get; set; }

        public string FileDate { get; set; }
    }
}
