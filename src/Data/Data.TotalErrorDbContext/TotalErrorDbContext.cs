namespace Data.TotalErrorDbContext
{
    using Data.Models.Models;
    using Data.Models.Interfaces;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using System.Reflection;

    public class TotalErrorDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<ItemType> ItemTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<LastReadFile> LastReadFiles { get; set; }

        private IConfigurationRoot configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            configuration = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries<IEntity<string>>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt = DateTime.Now;
                    entity.Entity.ModifiedAt = DateTime.Now;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.ModifiedAt = DateTime.Now;
                }
                else if (entity.State == EntityState.Detached)
                {
                    entity.State = EntityState.Deleted;
                    entity.Entity.DeletedAt = DateTime.Now;
                    entity.Entity.IsDeleted = true;
                }
            }

            return base.SaveChanges();
        }
    }
}