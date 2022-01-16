namespace Data.Models.Interfaces
{
    internal interface ICreatedInfo<T>
    {
        public DateTime CreatedAt { get; set; }

        public T CreatedById { get; set; }
    }
}
