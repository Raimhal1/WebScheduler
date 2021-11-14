using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;
using WebScheduler.BLL.Interfaces;
using AutoMapper;

namespace WebScheduler.BLL.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler 
        : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventDbContext _context;
        private readonly IUserDbContext _users;
        private readonly IEventFileService _fileService;
        private readonly IMapper _mapper;

        public CreateEventCommandHandler(IEventDbContext context, IUserDbContext users,
            IEventFileService fileService, IMapper mapper) =>
            (_context, _users, _fileService, _mapper) 
            = (context, users, fileService, mapper);
            

        public async Task<Guid> Handle(CreateEventCommand request,
            CancellationToken cancellationToken)
        {

            var userId = request.UserId;
            var user = await _users.Users.FindAsync(userId);

            var entity = new Event {

                UserId = userId,
                EventName = request.EventName,
                StartEventDate = request.StartEventDate,
                EndEventDate = request.EndEventDate,
                ShortDescription = request.ShortDescription,
                Description = request.Description,
                EventFiles = new List<EventFile>(),
                Users = new List<User> { user },
                Id = Guid.NewGuid(),
            };

            var files = _fileService.GenerateEventFiles(request.files);
            if(files != null)
                entity.EventFiles = _mapper.Map<List<EventFile>>(files);
            await _context.Events.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
