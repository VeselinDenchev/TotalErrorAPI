namespace Tests
{
    using Data.Services.DtoModels;
    using Data.Services.Implementations;
    using Data.TotalErrorDbContext;

    public class StartUp
    {
        public static void Main()
        {
            FileReader fileReader = new FileReader(new TotalErrorDbContext());
            List<TransferModel> models = fileReader.ReadFileFromDirectory(@"E:\read_files");

            //TransformerModel transformerModel = new TransformerModel();
            //var list = transformerModel.Convert(models);

            //DatabaseService db = new DatabaseService(new ApplicationDbContext());
            //db.SaveDataToDb(list);

            /*MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Company, CompanyDto>());
            Mapper mapper = new Mapper(config);
            MainCompanyService mainCompanyService = new MainCompanyService(new ApplicationDbContext(), mapper);
            var res = mainCompanyService.GetCompanies();*/
        }
    }
}

