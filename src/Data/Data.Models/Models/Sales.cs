namespace Data.Models.Models
{
    internal class Sales : BaseModel
    {
        public DateTime ShipDate { get; set; }

        public int UnitsSold { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitCost { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal TotalCost { get; set; }

        public decimal TotalProfit { get; set; }

        public Order Order { get; set; }

        public string OrderId { get; set; }

        public ItemType ItemType { get; set; }

        public string ItemTypeId { get; set; }
    }
}
