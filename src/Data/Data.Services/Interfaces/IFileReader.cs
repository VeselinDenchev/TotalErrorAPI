namespace Data.Services.Interfaces
{
    using Data.Services.DtoModels;

    internal interface IFileReader
    {
        List<TransferModel> ReadFileFromDirectory(string dir);
    }
}
