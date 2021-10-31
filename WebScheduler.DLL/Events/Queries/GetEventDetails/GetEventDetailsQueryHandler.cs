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
        private readonly IMapper _mapper;

        public GetEventDetailsQueryHandler(IEventDbContext context, IUserDbContext userContext, IMapper mapper) => 
            (_context, _userContext, _mapper) = (context, userContext, mapper);

        public async Task<EventDetailsVm> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

            var entity = await _context.Events
                .Include(e => e.Users)
                .Where(e => e.UserId == request.UserId || e.Users.Contains(user))
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            entity.Status = Validation.Status.ChangeStatus(entity.StartEventDate, entity.EndEventDate);
            _context.Events.Update(entity);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }
            else if (user == null || user.Id == Guid.Empty)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            return _mapper.Map<EventDetailsVm>(entity);
        }
    }
}
