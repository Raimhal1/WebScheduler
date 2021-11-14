using Microsoft.EntityFrameworkCore;
using WebScheduler.Domain.Models;

namespace WebScheduler.Domain.Interfaces
{
    public interface IEventDbContext : IBaseDbContext
    {
        DbSet<Event> Events { get; set; }
    }
}
