using Microsoft.EntityFrameworkCore;
using WebScheduler.Domain.Models;

namespace WebScheduler.Domain.Interfaces
{
    public interface IAllowedFileTypeDbContext : IBaseDbContext
    {
        DbSet<AllowedFileType> AllowedFileTypes { get; set; }
    }
}
