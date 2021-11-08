﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;

namespace WebScheduler.BLL.Interfaces
{
    public interface IReportService
    {
        Task<ReportDto> CreateEventsReport(Guid id, string extension, CancellationToken cancellationToken);
        Task<ReportDto> CreateEventsMemberReport(Guid id, string extension, CancellationToken cancellationToken);
        Task<ReportDto> CreateEventsReportForNextMonth(Guid id, string extension, CancellationToken cancellationToken);
    }
}
