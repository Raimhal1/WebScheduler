using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.BLL.Interfaces;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Services
{
    public class FileSettingsService : IFileSettingsService
    {
        private readonly IAllowedFileTypeDbContext _fileTypesContext;
        private readonly IMapper _mapper;

        public FileSettingsService(IAllowedFileTypeDbContext fileTypesContext, IMapper mapper) =>
            (_fileTypesContext, _mapper) = (fileTypesContext, mapper);


        public async Task<AllowedFileTypeListVm> GetAllowedFileTypes()
        {
            var allowedFileTypes = await _fileTypesContext.AllowedFileTypes
               .ProjectTo<AllowedFileTypeDto>(_mapper.ConfigurationProvider)
               .ToListAsync();

            return new AllowedFileTypeListVm { AllowedFileTypes = allowedFileTypes};
        }

        public async Task<int> AddFileType(AllowedFileTypeDto fileTypeDto, CancellationToken cancellationToken)
        {
            var fileType = await _fileTypesContext.AllowedFileTypes    
                .FirstOrDefaultAsync(t => t.FileType == fileTypeDto.FileType);    

            if(fileType != null) return default;    

            var type = _mapper.Map<AllowedFileType>(fileTypeDto);
            _fileTypesContext.AllowedFileTypes.Add(type);    

            await _fileTypesContext.SaveChangesAsync(cancellationToken);    
            return type.Id;    
        }

        public async Task ChangeFileType(int id, AllowedFileTypeDto fileTypeDto, CancellationToken cancellationToken)
        {

            var fileType = await _fileTypesContext.AllowedFileTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (fileType != null)
            {
                fileType = _mapper.Map<AllowedFileType>(fileTypeDto);
                fileType.Id = id;
                _fileTypesContext.AllowedFileTypes.Update(fileType);

                await _fileTypesContext.SaveChangesAsync(cancellationToken);
            }
        }


    }
}
