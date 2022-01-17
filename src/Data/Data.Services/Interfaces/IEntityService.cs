namespace Data.Services.Interfaces
{
    using Data.Models.Models;

    internal interface IEntityService
    {
        public Task<int> SaveEntitiesToDb(List<Order> models);
    }
}
