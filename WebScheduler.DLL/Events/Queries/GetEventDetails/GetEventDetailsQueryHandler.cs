using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.Queries.GetEventDetails
{
    class GetEventDetailsQueryHandler : IRequestHandler<GetEventDetailsQuery, EventDetailsVm>
    {
        private readonly IEventDbContext _context;
        private readonly IMapper _mapper;

        public GetEventDetailsQueryHandler(IEventDbContext context, IMapper mapper) => 
            (_context, _mapper) = (context, mapper);

        public async Task<EventDetailsVm> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events
                .Include(e => e.Users)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            return _mapper.Map<EventDetailsVm>(entity);
        }
    }
}
