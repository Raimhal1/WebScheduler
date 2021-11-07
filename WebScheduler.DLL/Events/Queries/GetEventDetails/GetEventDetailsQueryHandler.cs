using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var user = await _userContext.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null || user.Id == Guid.Empty)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }
            Event entity;

            var role = await _roleContext.Roles
                .FirstOrDefaultAsync(r => r.Name == AdminRoleName);

            if(user.Roles.Contains(role))
            {
                entity = await _context.Events
                .Include(e => e.Users)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            }
            else
            {
                entity = await _context.Events
               .Include(e => e.Users)
               .Where(e => e.UserId == request.UserId || e.Users.Contains(user))
               .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            }

            if (entity == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }


            //entity.Status = entity.StartEventDate.UpdateStatus(entity.EndEventDate);

            _context.Events.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            var eventVm = _mapper.Map<EventDetailsVm>(entity);
            eventVm.Users = _mapper.Map<List<UserVm>>(entity.Users);
            return eventVm;
        }
    }
}
