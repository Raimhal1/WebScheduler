using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;

namespace WebScheduler.BLL.Interfaces
{
    public interface IEventFileService
    {
        Task<EventFileDto> GetFile(Guid fileId, Guid eventId);
        Task<List<EventFileDto>> GenerateEventFiles(IList<IFormFile> fromFiles);
        Task ChangeFileName(Guid fileId, Guid eventId, string Name, CancellationToken cancellationToken);
        Task DeleteFileFromEvent(Guid fileId, Guid eventId, CancellationToken cancellationToken);
        Task DeleteFile(Guid fileId, CancellationToken cancellationToken);
    }
}
