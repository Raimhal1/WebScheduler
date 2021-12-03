using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;

namespace WebScheduler.Controllers
{
    [Authorize]
    public class ReportController : BaseController
    {
        private readonly IReportService _fileService;

        public ReportController(IReportService fileService) =>
            _fileService = fileService;

        [HttpGet]
        [Route("api/my/events/report/{extension}")]
        public async Task<FileContentResult> GetUserEventsReport(string extension, CancellationToken cancellationToken)
        {
            var report = await _fileService.CreateEventsReport(UserId, extension, cancellationToken);
            return File(report.Content, report.ContentType, report.FileName);
        }

        [HttpGet]
        [Route("api/events/report/{extension}")]
        public async Task<FileContentResult> GetUserEventsMemberReport(string extension, CancellationToken cancellationToken)
        {
            var report = await _fileService.CreateEventsMemberReport(UserId, extension, cancellationToken);
            return File(report.Content, report.ContentType, report.FileName);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/events/next-month-report/{extension}")]
        public async Task<FileContentResult> GetUserEventsReportForNextMonth(string extension, CancellationToken cancellationToken)
        {
            var report = await _fileService.CreateEventsReportForNextMonth(UserId, extension, cancellationToken);
            return File(report.Content, report.ContentType, report.FileName);
        }
       
    }
}
