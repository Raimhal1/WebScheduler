using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;

namespace WebScheduler.BLL.Interfaces
{
    public interface IFileSettingsService 
    {
        Task<AllowedFileTypeListVm> GetAllowedFileTypes();
        Task<int> AddFileType(AllowedFileTypeDto fileTypeDto, CancellationToken cancellationToken);
        Task ChangeFileType(int id, AllowedFileTypeDto fileTypeDto, CancellationToken cancellationToken);
        Task<List<GeneralFileDto>> CreateGeneralFiles(IList<IFormFile> fromFiles);

        Task DeleteFileType(int id, CancellationToken cancellationToken);
    }
}
