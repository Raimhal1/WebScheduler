using System;
using MediatR;

namespace WebScheduler.BLL.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
