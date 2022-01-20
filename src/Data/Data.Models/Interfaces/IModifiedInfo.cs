namespace Data.Models.Interfaces
{
    public interface IModifiedInfo<T>
    {
        public DateTime ModifiedAt { get; set; }

        public T ModifiedById { get; set; }
    }
}
