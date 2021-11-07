using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.Queries.GetEventList
{
    class GetEventListQueryMemberHandler : IRequestHandler<GetEventListQueryMember, EventListVm>
    {
        private readonly IEventDbContext _context;
        private readonly IUserDbContext _userContext;
        private readonly IMapper _mapper;

        public GetEventListQueryMemberHandler(IEventDbContext context, IUserDbContext userContext, IMapper mapper)
            => (_context, _userContext, _mapper) = (context, userContext, mapper);

        public async Task<EventListVm> Handle(GetEventListQueryMember request, CancellationToken cancellationToken)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user == null || user.Id == Guid.Empty)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var eventQuery = await _context.Events
                .Include(e => e.Users)
                .Where(e => e.Users.Contains(user) && e.UserId != user.Id)
                .ProjectTo<EventLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var eventListVm = new EventListVm { Events = eventQuery };

            for (int i = 0; i < eventListVm.Events.Count; i++)
            {
                eventListVm.Events[i].Users = _mapper.Map<List<UserVm>>(eventQuery[i].Users);
            }
            return eventListVm;
        }
    }
}
