namespace Data.Models.Interfaces
{
    public interface IEntity<T> : IIdentity<T>, ICreatedInfo, IModifiedInfo, IDeletedInfo
    {
    }
}
