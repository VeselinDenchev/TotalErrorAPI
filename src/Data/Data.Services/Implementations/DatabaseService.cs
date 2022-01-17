
namespace Data.Services.Implementations
{
    using Data.Models.Models;
    using Data.TotalErrorDbContext;

    public class DatabaseService
    {
        public DatabaseService(TotalErrorDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TotalErrorDbContext DbContext { get; }

        public void SaveDataToDb(List<Order> companies)
        {
            //using (ApplicationDb)
            //{
            //    ApplicationDb.Companies.AddRange(companies);
            //    var res=ApplicationDb.SaveChanges();
            //}
        }
    }
}
