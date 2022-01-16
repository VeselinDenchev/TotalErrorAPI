namespace Data.Models.Interfaces
{
    internal interface IEntity : IIdentity<string>, ICreatedInfo<string>, IModifiedInfo<string>, IDeletedInfo<string>
    {
    }
}
