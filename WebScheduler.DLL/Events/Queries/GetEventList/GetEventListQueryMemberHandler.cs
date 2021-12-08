using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.Queries.GetEventList
{
    class GetEventListQueryMemberHandler : IRequestHandler<GetEventListQueryMember, EventListVm>
    {
        private readonly IEventDbContext _context;
        private readonly IMapper _mapper;

        public GetEventListQueryMemberHandler(IEventDbContext context, IMapper mapper)
            => (_context, _mapper) = (context, mapper);

        public async Task<EventListVm> Handle(GetEventListQueryMember request, CancellationToken cancellationToken)
        {
            // member 
            Expression<Func<Event, bool>> expression = e => 
                e.Users.Any(u => u.Id == request.UserId && u.Id != e.UserId);

            var eventQuery = await LookUp.GetLookupEventList(_context, _mapper, expression, cancellationToken);

            return new EventListVm { Events = eventQuery};
        }
    }
}
