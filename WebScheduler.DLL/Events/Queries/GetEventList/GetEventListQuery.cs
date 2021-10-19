using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Events.Queries.GetEventList
{
    class GetEventListQuery : IRequest<EventListVm>
    {
        public Guid UserId { get; set; }
    }
}
