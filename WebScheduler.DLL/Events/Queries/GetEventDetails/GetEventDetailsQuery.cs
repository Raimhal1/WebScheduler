using System;
using MediatR;


namespace WebScheduler.BLL.Events.Queries.GetEventDetails
{
    class GetEventDetailsQuery : IRequest<EventDetailsVm>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
