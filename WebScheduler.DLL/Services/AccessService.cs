using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Services
{

    public class AccessService : IAccessService
    {
        private readonly IUserDbContext _userContext;
        private readonly IUserService _userService;
        private readonly string AdminRoleName = "Admin";

        public AccessService(IUserDbContext userContext, IUserService userService) =>
            (_userContext, _userService) = (userContext, userService);

        public async Task<bool> HasAccessToEvent(Guid userId, Guid eventId)
        {
            Expression<Func<User, bool>> expression = u => u.Id == userId;
            var user = await _userService.getUser(expression, userId);

            if (user.Roles.Any(r => r.Name == AdminRoleName))
                return true;

            var access = user.Events.Any(e => e.Id == eventId);
            return access;
        }

        public async Task<bool> HasAccessToUser(Guid userId, Guid id)
        {
            Expression<Func<User, bool>> expression = u => u.Id == userId;
            var user = await _userService.getUser(expression, userId);

            if (user.Roles.Any(r => r.Name == AdminRoleName))
                return true;

            return userId == id;

        }
    }
}
