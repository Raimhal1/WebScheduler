using System;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Interfaces
{
    public interface IAssesService
    {
        Task<bool> HasAssesToEvent(Guid userId, Guid eventId);
        Task<bool> HasAssesToEventFile(Guid userId, Guid fileId);
    }
}
