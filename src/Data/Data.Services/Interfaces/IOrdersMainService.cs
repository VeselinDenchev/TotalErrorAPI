namespace Data.Services.Interfaces
{
    using Data.Services.DtoModels;

    public interface IOrdersMainService
    {
        public List<OrderDto> GetOrders();
    }
}
