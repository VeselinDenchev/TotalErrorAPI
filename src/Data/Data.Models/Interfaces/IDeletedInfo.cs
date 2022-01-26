namespace Data.Models.Interfaces
{
    public interface IDeletedInfo//<T>
    {
        public DateTime? DeletedAt { get; set; }

        bool IsDeleted { get; set; }
    }
}
