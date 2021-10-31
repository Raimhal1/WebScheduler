using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;

namespace WebScheduler.Controllers
{
    [Authorize]
    public class FileController : BaseController
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService) =>
            _fileService = fileService;

        [HttpGet]
        [Route("api/events/report/{extension}")]
        public async Task<FileContentResult> GetUserEventsReport(string extension)
        {
            var file = await _fileService.CreateEventListReport(UserId);
            var now = DateTime.UtcNow;
            var fileName = $"user_{UserId}_events_{now}" + $".{extension}";
            return File(Encoding.UTF8.GetBytes(file), $"files/{extension}", fileName);
        }

        [HttpGet]
        [Route("api/events/member/report/{extension}")]
        public async Task<FileContentResult> GetUserEventsMemberReport(string extension)
        {
            var file = await _fileService.CreateEventListMemberReport(UserId);
            var now = DateTime.UtcNow;
            var fileName = $"user_{UserId}_events_{now}" + $".{extension}";
            return File(Encoding.UTF8.GetBytes(file), $"files/{extension}", fileName);
        }

    }
}
