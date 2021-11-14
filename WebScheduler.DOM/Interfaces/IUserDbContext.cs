using Microsoft.EntityFrameworkCore;
using WebScheduler.Domain.Models;

namespace WebScheduler.Domain.Interfaces
{
    public interface IUserDbContext : IBaseDbContext
    {
        DbSet<User> Users { get; set; }
    }
}
