using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.BLL.Services
{
    public class AccessService : IAccessService
    {
        private readonly IUserDbContext _userContext;
        private readonly IEventDbContext _context;
        private readonly string AdminRoleName = "Admin";

        public AccessService(IUserDbContext userContext, IEventDbContext context) =>
            (_userContext, _context) = (userContext, context);

        public async Task<bool> HasAccessToEvent(Guid userId, Guid eventId)
        {
            var user = await _userContext.Users
                .Include(u => u.Events)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user.Roles.Any(r => r.Name == AdminRoleName))
                return true;

            var access = user.Events.Any(e => e.Id == eventId);
            return access;
        }
    }
}
