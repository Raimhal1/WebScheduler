using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Interfaces
{
    public interface IUserService : IBaseService<Event>
    {
        public Task<User> FindUserByEmailAsync(string email);
    }
}
