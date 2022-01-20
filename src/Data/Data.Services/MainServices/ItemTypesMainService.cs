namespace Data.Services.MainServices
{
    using AutoMapper;

    using Data.Services.DtoModels;
    using Data.Services.Interfaces;
    using Data.TotalErrorDbContext;

    public class ItemTypesMainService : IItemTypesMainService
    {
        public ItemTypesMainService(TotalErrorDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public TotalErrorDbContext DbContext { get; }

        public IMapper Mapper { get; }

        public List<ItemTypeDto> GetItemTypes()
        {
            var itemTypes = this.Mapper.Map<List<ItemTypeDto>>(this.DbContext.ItemTypes.ToList());

            return itemTypes;
        }
    }
}
