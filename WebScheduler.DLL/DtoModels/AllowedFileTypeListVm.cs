using System.Collections.Generic;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.DtoModels
{
    public class AllowedFileTypeListVm
    {
        public IList<AllowedFileType> AllowedFileTypes { get; set; }
    }
}
