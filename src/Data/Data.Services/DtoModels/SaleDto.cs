namespace Data.Services.DtoModels
{
    public class SaleDto
    {
        public DateTime ShipDate { get; set; }

        public int UnitsSold { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitCost { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal TotalCost { get; set; }

        public decimal TotalProfit { get; set; }

        //public OrderDto Order { get; set; }

        public ItemTypeDto ItemType { get; set; }

        //public string FileDate { get; set; }
    }
}
