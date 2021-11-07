using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> CreateEventsReport(Guid id, string extension, CancellationToken cancellationToken);
        Task<byte[]> CreateEventsMemberReport(Guid id, string extension, CancellationToken cancellationToken);
        Task<byte[]> CreateEventsReportForNextMonth(Guid id, string extension, CancellationToken cancellationToken);
    }
}
