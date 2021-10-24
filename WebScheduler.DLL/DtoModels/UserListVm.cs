using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScheduler.BLL.DtoModels
{
    public class UserListVm
    {
        public IList<UserDto> Users { get; set; }
    }
}
