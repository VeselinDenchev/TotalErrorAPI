namespace Data.Services.DtoModels
{
    public class GroupedOrdersDtoModel<T>
    {
        public GroupedOrdersDtoModel(List<IGrouping<T, OrderDto>> groupedOrders,
            Dictionary<T, decimal> totalCostPerGroup,
            Dictionary<T, decimal> totalProfitPerGroup)
        {
            this.GroupedOrders = groupedOrders;
            this.TotalCostPerGroup = totalCostPerGroup;
            this.TotalProfitPerGroup = totalProfitPerGroup;
        }

        public List<IGrouping<T, OrderDto>> GroupedOrders { get; set; }

        public Dictionary<T, decimal> TotalCostPerGroup { get; set; }

        public Dictionary<T, decimal> TotalProfitPerGroup { get; set; }
    }
}
