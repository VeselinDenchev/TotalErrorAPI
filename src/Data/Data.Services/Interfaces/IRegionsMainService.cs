namespace Data.Services.Interfaces
{
    using AutoMapper;

    using Data.Services.DtoModels;

    public interface IRegionsMainService
    {
        public List<RegionDto> GetRegions();
    }
}
