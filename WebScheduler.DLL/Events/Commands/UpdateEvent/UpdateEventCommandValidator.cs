using FluentValidation;
using System;
using WebScheduler.BLL.Validation;

namespace WebScheduler.BLL.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(updateEventCommand =>
                updateEventCommand.EventName).NotEmpty().MaximumLength(50);
            RuleFor(updateEventCommand =>
               updateEventCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updateEventCommand =>
               updateEventCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateEventCommand =>
                updateEventCommand.StartEventDate).Must(DateValidator.BeAValidDate);
            RuleFor(updateEventCommand =>
                updateEventCommand.EndEventDate).Must(DateValidator.BeAValidDate)
                .GreaterThan(updateEventCommand => updateEventCommand.StartEventDate);
            RuleFor(updateEventCommand =>
                updateEventCommand.ShortDescription).NotEmpty().MaximumLength(50);
            RuleFor(updateEventCommand =>
                updateEventCommand.Description).NotEmpty().MaximumLength(2000);
        }
    }
}
