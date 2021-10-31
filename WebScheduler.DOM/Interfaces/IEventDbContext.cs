using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.Domain.Models;

namespace WebScheduler.Domain.Interfaces
{
    public interface IEventDbContext : IBaseDbContext
    {
        DbSet<Event> Events { get; set; }
    }
}
