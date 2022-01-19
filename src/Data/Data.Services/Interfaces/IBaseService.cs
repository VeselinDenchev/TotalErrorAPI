namespace Data.Services.Interfaces
{
    using Data.Services.DtoModels;
    using Data.Services.Implementations;

    public interface IBaseService
    {
        public Dictionary<string, List<TransferModel>> ReadFilesFromDirectory(string dir);

        public DataObject Convert(Dictionary<string, List<TransferModel>> transferModelsByFile);

        public void SaveDataToDatabase(DataObject data);
    }
}
