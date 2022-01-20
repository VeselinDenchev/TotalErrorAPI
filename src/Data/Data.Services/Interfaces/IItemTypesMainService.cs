namespace Data.Services.Interfaces
{
    using Data.Services.DtoModels;

    public interface IItemTypesMainService
    {
        public List<ItemTypeDto> GetItemTypes();
    }
}
