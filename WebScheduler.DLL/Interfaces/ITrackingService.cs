using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Interfaces
{
    public interface ITrackingService 
    {
        Task TrackChange<Entity, LogEntity>(Entity entity, Guid id) 
            where Entity : class 
            where LogEntity : class;
    }
}
