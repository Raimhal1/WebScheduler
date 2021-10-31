using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;
using WebScheduler.BLL.Validation;

namespace WebScheduler.BLL.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler 
        : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventDbContext _context;
        private readonly IUserDbContext _users;

        public CreateEventCommandHandler(IEventDbContext context, IUserDbContext users) =>
            (_context, _users) = (context, users);
            

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
                Users = new List<User> { user },
                EventFiles = new List<EventFile>(),
                Id = Guid.NewGuid(),
            };

            entity.Status = Validation.Status.ChangeStatus(entity.StartEventDate, entity.EndEventDate);

            await _context.Events.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
