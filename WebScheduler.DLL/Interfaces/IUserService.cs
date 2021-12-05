using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Interfaces
{
    public interface IUserService : IBaseService<RegisterUserModel, UserDto>
    {
        Task<User> getUser(Expression<Func<User, bool>> expression, object key);
        Task<Guid> getIdFromEmail(string Email);
    }
}
