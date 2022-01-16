namespace Data.Models.Models
{
    using Data.Models.Interfaces;

    using System;

    internal class BaseModel : IEntity
    {
        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime CreatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string CreatedById { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime ModifiedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ModifiedById { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime DeletedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string DeletedById { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
