using System;
using MediatR;

namespace WebScheduler.BLL.Events.Commands.DeleteEvent
{
    class DeleteEventCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
