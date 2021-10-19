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

        public CreateEventCommandHandler(IEventDbContext context) =>
            _context = context;

        public async Task<Guid> Handle(CreateEventCommand request,
            CancellationToken cancellationToken)
        {
            var dayEvent = new Event {

                UserId = request.UserId,
                EventName = request.EventName,
                StartEventDate = request.StartEventDate,
                EndEventDate = request.EndEventDate,
                ShortDescription = request.ShortDescription,
                Description = request.Description,

                Users = new List<User>(),
                Id = Guid.NewGuid()

            };

            await _context.Events.AddAsync(dayEvent, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return dayEvent.Id;
        }
    }
}
