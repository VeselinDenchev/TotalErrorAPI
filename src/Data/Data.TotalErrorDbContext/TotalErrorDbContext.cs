namespace Data.TotalErrorDbContext
{
    using Data.Models.Models;

    using Microsoft.EntityFrameworkCore;


    public class TotalErrorDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<ItemType> ItemTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-R8623N1\SQLEXPRESS01;Database=TotalError;Trusted_Connection=True");
        }
    }
}