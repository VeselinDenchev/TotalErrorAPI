namespace Data.Services.Implementations
{
    using Data.Models.Models;
    using Data.Services.Interfaces;
    using Data.TotalErrorDbContext;

    using System.Threading.Tasks;

    public class EntityService : IEntityService
    {
        public EntityService(TotalErrorDbContext dbContext)
        {
            this.DbContext = dbContext;

        }

        public TotalErrorDbContext DbContext { get; }

        public Task<int> SaveEntitiesToDb(List<Order> models)
        {
            throw new NotImplementedException();
        }
    }
}
