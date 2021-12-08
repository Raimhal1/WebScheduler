using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;

namespace WebScheduler.BLL.Interfaces
{
    public interface IEventFileService
    {
        Task<EventFileDto> GetFile(Guid fileId, Guid eventId, CancellationToken cancellation);
        Task<List<Guid>> GetFilesIds(Guid eventId, CancellationToken cancellation);
        Task AddFilesToEvent(Guid eventId, IList<IFormFile> files, CancellationToken cancellationToken);
        Task ChangeFileName(Guid fileId, Guid eventId, string Name, CancellationToken cancellationToken);
        Task DeleteFileFromEvent(Guid fileId, Guid eventId, CancellationToken cancellationToken);
        Task DeleteFile(Guid fileId, CancellationToken cancellationToken);
    }
}
