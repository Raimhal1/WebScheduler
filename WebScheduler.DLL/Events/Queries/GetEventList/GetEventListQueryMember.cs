using MediatR;
using System;

namespace WebScheduler.BLL.Events.Queries.GetEventList
{
    public class GetEventListQueryMember : IRequest<EventListVm>
    {
        public Guid UserId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
