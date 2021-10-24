﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
