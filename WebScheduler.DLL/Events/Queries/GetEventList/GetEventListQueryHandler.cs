using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.BLL.Events.Queries.GetEventList
{
    class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, EventListVm>
    {
        private readonly IEventDbContext _context;
        private readonly IMapper _mapper;

        public GetEventListQueryHandler(IEventDbContext context, IMapper mapper)
            => (_context, _mapper) = (context, mapper);

        public async Task<EventListVm> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            var eventQuery = await _context.Events
                .Include(e => e.Users)
                .Where(e => e.UserId == request.UserId)
                .ProjectTo<EventLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            foreach (var entity in eventQuery)
            {
                entity.Status = Validation.Status.ChangeStatus(entity.StartEventDate, entity.EndEventDate);
            }
            return new EventListVm { Events = eventQuery };
        }
    }
}
