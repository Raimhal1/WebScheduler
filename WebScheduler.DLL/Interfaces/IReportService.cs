using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Interfaces
{
    public interface IReportService
    {
        Task<string> CreateEventsReport(Guid id, string extension);
        Task<string> CreateEventsMemberReport(Guid id, string extension);
        Task<string> CreateEventsReportForNextMonth(Guid id, string extension);
    }
}
