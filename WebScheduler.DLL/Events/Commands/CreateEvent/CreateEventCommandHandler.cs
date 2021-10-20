using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

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
            //var user = await _users.Users.FindAsync(request.UserId);

            var dayEvent = new Event {

                UserId = request.UserId,
                EventName = request.EventName,
                StartEventDate = request.StartEventDate,
                EndEventDate = request.EndEventDate,
                ShortDescription = request.ShortDescription,
                Description = request.Description,

                //Users = new List<User> { user },
                Id = Guid.NewGuid()

            };

            await _context.Events.AddAsync(dayEvent, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return dayEvent.Id;
        }
    }
}
