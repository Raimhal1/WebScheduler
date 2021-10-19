using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Interfaces
{
    public interface IDayEventService : IBaseService<Event>
    {
        Task<IEnumerable<Event>> GetUserDayEvents(Guid userId);
    }
}
