using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebScheduler.Domain.Interfaces
{
    public interface IBaseDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancelllationToken);
    }
}
