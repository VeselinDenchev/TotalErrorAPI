namespace Data.Models.Models
{
    using Data.Models.Interfaces;

    using System;

    public class BaseModel : IEntity<string>
    {
        public BaseModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
            this.ModifiedAt = DateTime.Now;
            this.IsDeleted = false;
        }

        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedById { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModifiedById { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedById { get; set; }

        public bool IsDeleted { get; set; }
    }
}
