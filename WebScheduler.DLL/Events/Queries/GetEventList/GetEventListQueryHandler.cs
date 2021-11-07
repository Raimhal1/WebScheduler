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
using WebScheduler.BLL.DtoModels;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.Queries.GetEventList
{
    class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, EventListVm>
    {
        private readonly IEventDbContext _context;
        private readonly IRoleDbContext _roleContext;
        private readonly IMapper _mapper;
        private string AdminRoleName = "Admin";

        public GetEventListQueryHandler(IEventDbContext context, IRoleDbContext roleContext, IMapper mapper)
            => (_context, _roleContext, _mapper) = (context, roleContext, mapper);

        public async Task<EventListVm> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleContext.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Name == AdminRoleName);

            List<EventLookupDto> eventQuery;

            if(role == null)
            {
                eventQuery = await _context.Events
                .Include(e => e.Users)
                .Where(e => e.UserId == request.UserId)
                .ProjectTo<EventLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            }
            else
            {
                eventQuery = await _context.Events
                .Include(e => e.Users)
                .ProjectTo<EventLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            }

            var eventListVm = new EventListVm { Events = eventQuery };

            for(int i = 0; i < eventListVm.Events.Count; i++)
            {
                eventListVm.Events[i].Users = _mapper.Map<List<UserVm>>(eventQuery[i].Users);
            }
            return eventListVm;
        }
    }
}
