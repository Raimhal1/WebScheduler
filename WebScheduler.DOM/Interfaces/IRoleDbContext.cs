using Microsoft.EntityFrameworkCore;
using WebScheduler.Domain.Models;

namespace WebScheduler.Domain.Interfaces
{
    public interface IRoleDbContext : IBaseDbContext
    {
        DbSet<Role> Roles { get; set; }
    }
}

