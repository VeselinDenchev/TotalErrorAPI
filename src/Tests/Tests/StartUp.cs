﻿namespace Tests
{
    using System.Collections.Generic;
    using System.Globalization;

    using Data.Models.Models;
    using Data.Services.DtoModels;
    using Data.Services.Implementations;
    using Data.TotalErrorDbContext;

    public class StartUp
    {
        public static void Main()
        {
            BaseService baseService = new BaseService(new TotalErrorDbContext());
            List<Dictionary<string, List<TransferModel>>> transferModelsByFile = baseService.ReadFilesFromDirectory(@"E:\read_files");

            DataObject data = baseService.Convert(transferModelsByFile);
            List<string> dates = new List<string>();
            foreach (Dictionary<string, List<TransferModel>> date in transferModelsByFile)
            {
                foreach (KeyValuePair<string, List<TransferModel>> transferModelKeyValuePair in date)
                {
                    dates.Add(transferModelKeyValuePair.Key);
                }
            }
            List<DateTime> dateTimeList = new List<DateTime>();
            foreach (string date in dates)
            {
                DateTime dateTime = DateTime.Parse(date);
                dateTimeList.Add(dateTime);
            }
            data.LastReadFiles = dateTimeList;

            baseService.Convert(transferModelsByFile);
            baseService.SaveDataToDatabase(data);

            /*MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Company, CompanyDto>());
            Mapper mapper = new Mapper(config);
            MainCompanyService mainCompanyService = new MainCompanyService(new ApplicationDbContext(), mapper);
            var res = mainCompanyService.GetCompanies();*/
        }
    }
}

