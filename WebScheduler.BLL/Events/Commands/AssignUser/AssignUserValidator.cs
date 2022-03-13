using FluentValidation;
using System;

namespace WebScheduler.BLL.Events.Commands.AssignUser
{
    public class AssignUserValidator : AbstractValidator<AssignUserCommand>
    {
        public AssignUserValidator()
        {
            RuleFor(assignUserCommand =>
                assignUserCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(assignUserCommand =>
                assignUserCommand.EventId).NotEqual(Guid.Empty);

        }
    }
}
