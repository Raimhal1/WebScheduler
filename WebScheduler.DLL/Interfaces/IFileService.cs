using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Interfaces
{
    public interface IFileService
    {
        Task<string> CreateEventListReport(Guid id);
        Task<string> CreateEventListMemberReport(Guid id);
        Task<string> ConvertToByte64(IFormFile file);
    }
}
