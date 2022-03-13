using Microsoft.EntityFrameworkCore;
using WebScheduler.Domain.Models;

namespace WebScheduler.Domain.Interfaces
{
    public interface IReportDbContext : IBaseDbContext
    {
        DbSet<Report> Reports { get; set; }
    }
}
