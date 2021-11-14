using Microsoft.EntityFrameworkCore;
using WebScheduler.Domain.Models;

namespace WebScheduler.Domain.Interfaces
{
    public interface IEventFileDbContext : IBaseDbContext
    {
        DbSet<EventFile> EventFiles { get; set; }
    }
}
