using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.Domain.Interfaces;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IEventDbContext _context;
        public UpdateEventCommandHandler(IEventDbContext context) =>
            _context = context;

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if(entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            entity.EventName = request.EventName;
            entity.StartEventDate = request.StartEventDate;
            entity.EndEventDate = request.EndEventDate;
            entity.ShortDescription = request.ShortDescription;
            entity.Description = request.Description;
            entity.Status = Validation.Status.ChangeStatus(entity.StartEventDate, entity.EndEventDate);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }    
    }
}
