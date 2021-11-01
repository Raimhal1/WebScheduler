﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Events.Commands.AssignUser
{
    public class AssignUserValidator : AbstractValidator<AssignUserCommand>
    {
        public AssignUserValidator()
        {
            RuleFor(assignUserCommand =>
                assignUserCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(assignUserCommand =>
                assignUserCommand.Id).NotEqual(Guid.Empty);

        }
    }
}