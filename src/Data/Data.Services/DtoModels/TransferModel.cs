namespace Data.Services.DtoModels
{
    using Constants;

    using CsvHelper.Configuration.Attributes;

    public class TransferModel
    {
        [Name(TransferModelConstant.REGION)]
        public string Region { get; set; }

        [Name(TransferModelConstant.COUNTRY)]
        public string Country { get; set; }

        [Name(TransferModelConstant.ITEM_TYPE)]
        public string ItemType { get; set; }

        [Name(TransferModelConstant.SALES_CHANNEL)]
        public string SalesChannel { get; set; }

        [Name(TransferModelConstant.ORDER_PRIORITY)]
        public string OrderPriority { get; set; }

        [Name(TransferModelConstant.ORDER_DATE)]
        public string OrderDate { get; set; }

        [Name(TransferModelConstant.ORDER_ID)]
        public string OrderId { get; set; }

        [Name(TransferModelConstant.SHIP_DATE)]
        public string ShipDate { get; set; }

        [Name(TransferModelConstant.UNITS_SOLD)]
        public string UnitsSold { get; set; }

        [Name(TransferModelConstant.UNIT_PRICE)]
        public string UnitPrice { get; set; }

        [Name(TransferModelConstant.UNIT_COST)]
        public string UnitCost { get; set; }

        [Name(TransferModelConstant.TOTAL_REVENUE)]
        public string TotalRevenue { get; set; }

        [Name(TransferModelConstant.TOTAL_COST)]
        public string TotalCost { get; set; }

        [Name(TransferModelConstant.TOTAL_PROFIT)]
        public string TotalProfit { get; set; }
    }
}
