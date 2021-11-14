using MediatR;
using System;

namespace WebScheduler.BLL.Events.Commands.AssignUser
{
    public class AssignUserCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
