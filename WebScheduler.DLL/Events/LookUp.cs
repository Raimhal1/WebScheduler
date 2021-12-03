using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.BLL.Events.Queries.GetEventList;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events
{
    public static class LookUp
    {
        public static async Task<List<EventLookupDto>> GetLookupEventList(IEventDbContext _context,
            IMapper _mapper, Expression<Func<Event, bool>> expression, CancellationToken cancellationToken)
        {
            var eventQuery = await _context.Events
                .Include(e => e.Users)
                .Where(expression)
                .ProjectTo<EventLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            for (int i = 0; i < eventQuery.Count; i++)
                eventQuery[i].Users = _mapper.Map<List<UserVm>>(eventQuery[i].Users);

            return eventQuery;
        }
    }
}
