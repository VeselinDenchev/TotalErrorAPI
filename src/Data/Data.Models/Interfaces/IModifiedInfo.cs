namespace Data.Models.Interfaces
{
    internal interface IModifiedInfo<T>
    {
        public DateTime ModifiedAt { get; set; }

        public T ModifiedById { get; set; }
    }
}
