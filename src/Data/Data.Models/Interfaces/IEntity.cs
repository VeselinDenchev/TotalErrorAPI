namespace Data.Models.Interfaces
{
    internal interface IEntity<T> : IIdentity<T>, ICreatedInfo<T>, IModifiedInfo<T>, IDeletedInfo<T>
    {
    }
}
