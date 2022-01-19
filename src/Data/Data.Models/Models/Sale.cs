namespace Data.Models.Models
{
    using Data.Models.Interfaces;

    public class Sale : BaseModel, IFileDate
    {
        public DateTime ShipDate { get; set; }

        public int UnitsSold { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitCost { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal TotalCost { get; set; }

        public decimal TotalProfit { get; set; }

        public Order Order { get; set; }

        //public string OrderId { get; set; }

        public ItemType ItemType { get; set; }

        public string FileDate { get; set; }

        //public string ItemTypeId { get; set; }
    }
}
