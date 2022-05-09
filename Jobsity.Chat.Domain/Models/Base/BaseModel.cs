using System;

namespace Jobsity.Chat.Domain.Models.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; protected set; }

        protected abstract void Valitation();

    }
}
