using System;
using FluentValidation;

namespace WebScheduler.BLL.Events.Queries.GetEventList
{
    public class GetEventListQueryValidator : AbstractValidator<GetEventListQuery>
    { 
    
        public GetEventListQueryValidator()
        {
            RuleFor(e => e.UserId).NotEqual(Guid.Empty);
        }
    }
}
