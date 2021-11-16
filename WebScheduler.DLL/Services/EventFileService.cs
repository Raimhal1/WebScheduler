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
        private readonly int MaxFileCount = 5;

        public EventFileService(IEventDbContext context, IEventFileDbContext eventFileContext,
            IMapper mapper, IFileSettingsService fileSettingsService) =>
            (_context, _eventFilesContext, _mapper, _fileSettingsService) 
            = (context, eventFileContext, mapper, fileSettingsService);

        public async Task<EventFileDto> GetFile(Guid fileId, Guid eventId)
        {
            var file = await _eventFilesContext.EventFiles
                .FirstOrDefaultAsync(f => f.Id == fileId && f.EventId == eventId);

            if(file == null)
                throw new NotFoundException(nameof(EventFile), fileId); 

            return  _mapper.Map<EventFileDto>(file);
        }

        public async Task AddFilesToEvent(Guid eventId, IList<IFormFile> files, CancellationToken cancellationToken)
        {
            var entity = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == eventId,
                cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Event), eventId);

            if (entity.EventFiles.Count + files.Count > MaxFileCount)
                throw new Exception(message: "Too many files for the event");

            var eventFiles = GenerateEventFiles(files);
            var fileList = _mapper.Map<List<EventFile>>(eventFiles);

            foreach (var file in fileList)    
                entity.EventFiles.Add(file);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ChangeFileName(Guid fileId,  Guid eventId, string Name, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(Name))
                return;

            var file = await _eventFilesContext.EventFiles
                .FirstOrDefaultAsync(f =>
                f.Id == fileId 
                && f.EventId == eventId,
                cancellationToken);

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
                .FirstOrDefaultAsync(e => e.Id == eventId, cancellationToken);

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
                .FirstOrDefaultAsync(f => f.Id == fileId, cancellationToken);

            if (file == null)
                throw new NotFoundException(nameof(EventFile), fileId);

            _eventFilesContext.EventFiles.Remove(file);
            await _eventFilesContext.SaveChangesAsync(cancellationToken);
        }


        private async Task<List<EventFileDto>> GenerateEventFiles(IList<IFormFile> fromFiles)
        {
            var files = await _fileSettingsService.CreateGeneralFiles(fromFiles);
            if (files == null || files.Count == default)
                throw new InvalidCastException();
            return _mapper.Map<List<EventFileDto>>(files);
        }

    }
}
