namespace Tests
{
    using System.Collections.Generic;

    using Data.Models.Models;
    using Data.Services.DtoModels;
    using Data.Services.Implementations;
    using Data.TotalErrorDbContext;

    public class StartUp
    {
        public static void Main()
        {
            BaseService baseService = new BaseService(new TotalErrorDbContext());
            List<List<TransferModel>> transferModelsByFile = baseService.ReadFilesFromDirectory(@"E:\read_files", out List<string> fileNames);

            DataObject data = baseService.Convert(transferModelsByFile);
            data.LastReadFiles = fileNames;

            baseService.Convert(transferModelsByFile);
            baseService.SaveToDb(data);

            /*MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Company, CompanyDto>());
            Mapper mapper = new Mapper(config);
            MainCompanyService mainCompanyService = new MainCompanyService(new ApplicationDbContext(), mapper);
            var res = mainCompanyService.GetCompanies();*/
        }
    }
}

