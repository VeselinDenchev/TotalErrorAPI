namespace Data.Services.Interfaces
{
    using Data.Models.Models;
    using Data.Services.DtoModels;

    public interface ITransformerModel
    {
        List<Order> Convert(List<TransferModel> transferModels);
    }
}
