using System;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Interfaces
{
    public interface IAccessService
    {
        Task<bool> HasAccessToEvent(Guid userId, Guid eventId);
    }
}
