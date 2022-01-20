﻿namespace Data.Models.Interfaces
{
    public interface ICreatedInfo<T>
    {
        public DateTime CreatedAt { get; set; }

        public T CreatedById { get; set; }
    }
}
