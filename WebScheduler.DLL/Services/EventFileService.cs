using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;
using AutoMapper;
using WebScheduler.Domain.Interfaces;
using WebScheduler.BLL.DtoModels;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Services
{
    public class EventFileService : IEventFileService
    {
        private readonly IEventDbContext _context;
        private readonly IEventFileDbContext _eventFilesContext;
        private readonly IMapper _mapper;
        private readonly IFileSettingsService _fileSettingsService;

        public EventFileService(IEventDbContext context, IEventFileDbContext eventFileContext,
            IMapper mapper, IFileSettingsService fileSettingsService) =>
            (_context, _eventFilesContext, _mapper, _fileSettingsService) 
            = (context, eventFileContext, mapper, fileSettingsService);

        public async Task<EventFileDto> GetFile(Guid fileId)
        {
            var file = await _eventFilesContext.EventFiles.FirstOrDefaultAsync(f => f.Id == fileId);

            if(file == null)
                throw new NotFoundException(nameof(EventFile), fileId); 

            return  _mapper.Map<EventFileDto>(file);
        }

        public async Task ChangeFileName(Guid fileId, string Name, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(Name))
                return;

            var file = await _eventFilesContext.EventFiles
                .FirstOrDefaultAsync(f => f.Id == fileId);

            if(file == null)
                throw new NotFoundException(nameof(EventFile), fileId);

            file.FileName = Name;

            _eventFilesContext.EventFiles.Update(file);
            await _eventFilesContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteFileFromEvent(Guid fileId, Guid eventId, CancellationToken cancellationToken)
        {
            var entity = await _context.Events
                .Include(e => e.EventFiles)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if(entity == null)
                throw new NotFoundException(nameof(Event), eventId);

            var file = entity.EventFiles.FirstOrDefault(f => f.Id == fileId);

            if (file == null)
                throw new NotFoundException(nameof(EventFile), fileId);

            entity.EventFiles.Remove(file);
            _context.Events.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

        }

        public async Task DeleteFile(Guid fileId, CancellationToken cancellationToken)
        {
            var file = await _eventFilesContext.EventFiles
                .FirstOrDefaultAsync(f => f.Id == fileId);

            if (file == null)
                throw new NotFoundException(nameof(EventFile), fileId);

            _eventFilesContext.EventFiles.Remove(file);
            await _eventFilesContext.SaveChangesAsync(cancellationToken);
        }


        public List<EventFileDto> GenerateEventFiles(IList<IFormFile> fromFiles)
        {
            var files = _fileSettingsService.CreateGeneralFiles(fromFiles);
            if (files == null)
                throw new NullReferenceException();
            return _mapper.Map<List<EventFileDto>>(files);
        }




    }
}
