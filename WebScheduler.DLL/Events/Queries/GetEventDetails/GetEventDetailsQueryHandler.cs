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
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.Queries.GetEventDetails
{
    class GetEventDetailsQueryHandler : IRequestHandler<GetEventDetailsQuery, EventDetailsVm>
    {
        private readonly IEventDbContext _context;
        private readonly IUserDbContext _userContext;
        private readonly IRoleDbContext _roleContext;
        private readonly IMapper _mapper;
        private string AdminRoleName = "Admin";

        public GetEventDetailsQueryHandler(IEventDbContext context,
            IUserDbContext userContext, IRoleDbContext roleContext, IMapper mapper) => 
            (_context, _userContext, _roleContext, _mapper) 
            = (context, userContext, roleContext, mapper);

        public async Task<EventDetailsVm> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
        {
            Event entity;

            var role = await _roleContext.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Name == AdminRoleName
                && r.Users.Any(u => u.Id == request.UserId));

            Expression<Func<Event, bool>> expression;

            if (role == null)
            {
                expression = e => e.UserId == request.UserId
                || e.Users.Any(u => e.UserId == request.UserId);
            }
            else
                expression = e => true;

            entity = await _context.Events
              .Include(e => e.Users)
              .Where(expression)
              .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Event), request.Id);

            var eventVm = _mapper.Map<EventDetailsVm>(entity);
            eventVm.Users = _mapper.Map<List<UserVm>>(entity.Users);

            return eventVm;
        }
    }
}
