
namespace Data.Services.MainServices
{
    using AutoMapper;

    using Data.Models.Models;
    using Data.Services.DtoModels;
    using Data.Services.Interfaces;
    using Data.TotalErrorDbContext;

    public class RegionsMainService : IRegionsMainService
    {
        public RegionsMainService(TotalErrorDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public TotalErrorDbContext DbContext { get; }

        public IMapper Mapper { get; }

        public List<RegionDto> GetRegions()
        {
            var regions = this.Mapper.Map<List<RegionDto>>(this.DbContext.Regions.Where(r => r.IsDeleted == false).ToList());

            return regions;
        }

        public void AddRegion(string regionName)
        {
            Region region = new Region();
            region.Name = regionName;

            this.DbContext.Regions.Add(region);
            this.DbContext.SaveChanges();
        }

        public void UpdateRegion(string oldRegionName, string newRegionName)
        {
            Region region = this.DbContext.Regions.Where(r => r.Name == oldRegionName && r.IsDeleted == false).FirstOrDefault();

            region.Name = newRegionName;

            this.DbContext.Regions.Update(region);
            this.DbContext.SaveChanges();
        }

        public void DeleteRegion(string regionName)
        {
            Region region = this.DbContext.Regions.Where(r => r.Name == regionName && r.IsDeleted == false).FirstOrDefault();

            region.IsDeleted = true;
            region.DeletedAt = DateTime.Now;

            this.DbContext.Regions.Update(region);
            this.DbContext.SaveChanges();
        }
    }
}
