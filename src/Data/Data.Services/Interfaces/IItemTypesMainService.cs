namespace Data.Services.Interfaces
{
    using Data.Services.DtoModels;

    public interface IItemTypesMainService
    {
        public List<ItemTypeDto> GetItemTypes();

        public void AddItemType(string itemTypeName);

        public void UpdateItemType(string currentItemType, string newItemType);

        public void DeleteItemType(string itemTypeName);
    }
}
