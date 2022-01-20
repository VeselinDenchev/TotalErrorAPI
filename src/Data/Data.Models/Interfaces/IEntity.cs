namespace Data.Models.Interfaces
{
    public interface IEntity<T> : IIdentity<T>, ICreatedInfo<T>, IModifiedInfo<T>, IDeletedInfo<T>
    {
    }
}
