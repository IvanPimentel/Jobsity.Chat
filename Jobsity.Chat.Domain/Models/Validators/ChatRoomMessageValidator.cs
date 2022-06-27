using FluentValidation;
using System;

namespace Jobsity.Chat.Domain.Models.Validators
{
    public class ChatRoomMessageValidator : AbstractValidator<ChatRoomMessage>
    {
        public ChatRoomMessageValidator()
        {
            RuleFor(x => x.ChatRoomId).NotEqual(Guid.Empty)
                .WithMessage("ChatRoomId is required");

            RuleFor(x => x.UserId).NotEqual(Guid.Empty)
                .When(x => !x.Integration)
                .WithMessage("UserId is required");

            RuleFor(x => x.Content)
                .NotEmpty()
                .Length(1, 100)
                .WithMessage("Message must be greater than 0 characters and less than 100 characters");
        }
    }
}
