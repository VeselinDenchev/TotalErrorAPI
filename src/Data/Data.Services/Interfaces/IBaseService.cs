namespace Data.Services.Interfaces
{
    using Data.Services.DtoModels;
    using Data.Services.Implementations;

    public interface IBaseService
    {
        public List<Dictionary<string, List<TransferModel>>> ReadFilesFromDirectory(string dir);

        public DataObject Convert(List<Dictionary<string, List<TransferModel>>> transferModelsByFile);

        public void SaveDataToDatabase(DataObject data);
    }
}
