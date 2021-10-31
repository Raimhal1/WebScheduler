using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Interfaces
{
    public interface IUserService : IBaseService<RegisterUserModel, UserDto, UserListVm>
    {

    }
}
