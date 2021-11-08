using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

            Expression<Func<Event, bool>> expression = e =>
                e.Users.Any(u => u.Id == request.UserId) && e.UserId != request.UserId;

            var eventQuery = await LookUp.GetLookupEventList(_context, _mapper, expression, cancellationToken);
            return new EventListVm { Events = eventQuery};
        }
    }
}
