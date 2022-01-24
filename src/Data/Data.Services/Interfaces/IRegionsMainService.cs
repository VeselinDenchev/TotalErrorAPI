namespace Data.Services.Interfaces
{
    using AutoMapper;

    using Data.Services.DtoModels;

    public interface IRegionsMainService
    {
        public List<RegionDto> GetRegions();

        public void AddRegion(string regionName);

        public void UpdateRegion(string currentRegionName, string newRegionName);

        public void DeleteRegion(string regionName);
    }
}
