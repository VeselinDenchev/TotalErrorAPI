namespace Data.Services.DtoModels
{
    public class RegionDto
    {
        public string Name { get; set; }

        public ICollection<CountryDto> Countries { get; set; }
    }
}
