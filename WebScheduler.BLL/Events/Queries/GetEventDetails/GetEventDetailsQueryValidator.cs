using System;
using FluentValidation;

namespace WebScheduler.BLL.Events.Queries.GetEventDetails
{
    public class GetEventDetailsQueryValidator : AbstractValidator<GetEventDetailsQuery>
    {
        public GetEventDetailsQueryValidator()
        {
            RuleFor(e => e.Id).NotEqual(Guid.Empty);
            RuleFor(e => e.UserId).NotEqual(Guid.Empty);
        }
    }
}
