using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebScheduler.BLL.DtoModels;
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
               .FirstOrDefaultAsync(r => r.Name == AdminRoleName
               && r.Users.Any(u => u.Id == request.UserId));

            Expression<Func<Event, bool>> expression;

            if (role == null)
                expression = e => e.UserId == request.UserId;
            else
                expression = e => true;

            var eventQuery = await LookUp.GetLookupEventList(_context, _mapper, expression, cancellationToken);

            for(int i = 0; i < eventQuery.Count; i++)
            {
                eventQuery[i].Users = _mapper.Map<List<UserVm>>(eventQuery[i].Users);
            }
            return new EventListVm {Events = eventQuery };
        }
    }
}
