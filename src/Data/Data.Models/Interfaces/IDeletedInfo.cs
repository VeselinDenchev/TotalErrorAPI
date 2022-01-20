namespace Data.Models.Interfaces
{
    public interface IDeletedInfo<T>
    {
        public DateTime? DeletedAt { get; set; }

        public T? DeletedById { get; set; }

        bool IsDeleted { get; set; }
    }
}
