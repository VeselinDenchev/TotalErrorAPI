
namespace Data.Services.MainServices
{
    using AutoMapper;

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
            var regions = this.Mapper.Map<List<RegionDto>>(this.DbContext.Regions.ToList());

            return regions;
        }
    }
}
