using FluentValidation;
using System;
using WebScheduler.BLL.Validation;

namespace WebScheduler.BLL.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(createEventCommand =>
                createEventCommand.EventName).NotEmpty().MaximumLength(250);
            RuleFor(createEventCommand =>
               createEventCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(createEventCommand =>
               createEventCommand.Id).NotEqual(Guid.Empty);
            RuleFor(createEventCommand =>
                createEventCommand.StartEventDate).Must(DateValidation.BeAValidDate);
            RuleFor(createEventCommand =>
                createEventCommand.EndEventDate).Must(DateValidation.BeAValidDate);
            RuleFor(createEventCommand =>
                createEventCommand.ShortDescription).NotEmpty().MaximumLength(250);
            RuleFor(createEventCommand =>
                createEventCommand.Description).NotEmpty().MaximumLength(5000);
        }
    }
}
