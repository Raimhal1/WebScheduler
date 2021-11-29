using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.Domain.Interfaces;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Models;
using WebScheduler.BLL.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace WebScheduler.BLL.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IEventDbContext _context;
        private readonly IEventFileService _fileService;
        private readonly IRoleDbContext _roleContext;
        private readonly IMapper _mapper;
        private readonly string AdminRoleName = "Admin";
        public UpdateEventCommandHandler(IEventDbContext context,
            IEventFileService fileService, IRoleDbContext roleContext, IMapper mapper) =>
            (_context, _fileService, _roleContext, _mapper) = (context, fileService, roleContext, mapper);

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleContext.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Name == AdminRoleName
                && r.Users.Any(u => u.Id == request.UserId),
                cancellationToken);

            var entity = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if((entity == null || entity.UserId != request.UserId))
            {
                if(role == null)
                    throw new NotFoundException(nameof(Event), request.Id);
            }

            entity.EventName = request.EventName;
            entity.StartEventDate = request.StartEventDate;
            entity.EndEventDate = request.EndEventDate;
            entity.ShortDescription = request.ShortDescription;
            entity.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }    
    }
}
