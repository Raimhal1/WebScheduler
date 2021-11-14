using System;
using System.Threading.Tasks;


namespace WebScheduler.BLL.Interfaces
{
    public interface ITrackingService 
    {
        Task TrackChange<Entity, LogEntity>(Entity entity, Guid id) 
            where Entity : class 
            where LogEntity : class;
    }
}
