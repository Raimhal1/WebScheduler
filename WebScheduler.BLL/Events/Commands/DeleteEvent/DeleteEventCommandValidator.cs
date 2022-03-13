using System;
using FluentValidation;

namespace WebScheduler.BLL.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventCommandValidator()
        {
            RuleFor(deleteEventCommand => 
                deleteEventCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(deleteEventCommand =>
                deleteEventCommand.Id).NotEqual(Guid.Empty);

        }
    }
}
