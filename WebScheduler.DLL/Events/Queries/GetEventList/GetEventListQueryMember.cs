using MediatR;
using System;

namespace WebScheduler.BLL.Events.Queries.GetEventList
{
    public class GetEventListQueryMember : IRequest<EventListVm>
    {
        public Guid UserId { get; set; }
    }
}
