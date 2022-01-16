namespace Data.Models.Interfaces
{
    public interface IIdentity<T>
    {
        public T Id { get; set; }
    }
}
