using System.Threading;
using System.Threading.Tasks;

namespace WebScheduler.Domain.Interfaces
{
    public interface IBaseDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancelllationToken);
    }
}
