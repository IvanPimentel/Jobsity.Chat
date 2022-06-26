using System;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Results;

namespace Jobsity.Chat.Domain.Models.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; protected set; }

        [NotMapped]
        public ValidationResult ValidationResult;
        public abstract bool IsValid();

    }
}
