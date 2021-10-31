using FluentValidation;
using System;
using WebScheduler.BLL.Validation;

namespace WebScheduler.BLL.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(createEventCommand =>
                createEventCommand.EventName).NotEmpty().MaximumLength(250);
            RuleFor(createEventCommand =>
               createEventCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(createEventCommand =>
                createEventCommand.StartEventDate).Must(DateValidator.BeAValidDate);
            RuleFor(createEventCommand =>
                createEventCommand.EndEventDate).Must(DateValidator.BeAValidDate)
                .GreaterThan(createEventCommand => createEventCommand.StartEventDate);
            RuleFor(createEventCommand => 
                createEventCommand.ShortDescription).NotEmpty().MaximumLength(250);
            RuleFor(createEventCommand =>
                createEventCommand.Description).NotEmpty().MaximumLength(5000);
        }


    }
}
