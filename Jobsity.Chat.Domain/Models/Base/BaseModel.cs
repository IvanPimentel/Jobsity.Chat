using System;

namespace Jobsity.Chat.Domain.Models.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        protected abstract void Valitation();

    }
}
