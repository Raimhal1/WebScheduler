using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Interfaces
{
    public interface IAssesService
    {
        bool HasAssesToEvent(Guid userId, Guid eventId);
        bool HasAssesToEventFile(Guid userId, Guid fileId);
    }
}
