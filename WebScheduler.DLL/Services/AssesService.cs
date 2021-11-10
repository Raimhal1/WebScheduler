using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.BLL.Services
{
    public class AssesService : IAssesService
    {
        private readonly IUserDbContext _userContext;
        private readonly IEventDbContext _context;

        public AssesService(IUserDbContext userContext, IEventDbContext context) =>
            (_userContext, _context) = (userContext, context);

        public async Task<bool> HasAssesToEvent(Guid userId, Guid eventId)
        {
            var user = await _userContext.Users
                .Include(u => u.Events)
                .FirstOrDefaultAsync(u => u.Id == userId 
                && u.Events.Any(e => e.Id == eventId));
            return user == null ? false : true;
        }

        public async Task<bool> HasAssesToEventFile(Guid userId, Guid fileId)
        {
            var Event = await _context.Events
                .Include(e => e.Users)
                .Include(e => e.EventFiles)
                .FirstOrDefaultAsync(e =>
                e.Users.Any(u => u.Id == userId)
                && e.EventFiles.Any(f => f.Id == fileId));
            return Event == null ? false : true;
        }
    }
}
