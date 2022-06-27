using FluentValidation;

namespace Jobsity.Chat.Domain.Models.Validators
{
    public class ChatRoomValidator : AbstractValidator<ChatRoom>
    {
        public ChatRoomValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 30)
                .WithMessage("Name of Chat Room must be greater than 3 characters and less than 30 characters");
        }
    }
}
