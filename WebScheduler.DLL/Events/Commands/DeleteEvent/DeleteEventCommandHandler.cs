using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;
using WebScheduler.BLL.Validation.Exceptions;


namespace WebScheduler.BLL.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IEventDbContext _context;
        public DeleteEventCommandHandler(IEventDbContext context) =>
            _context = context;
        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events.FindAsync(new object[] { request.Id }, cancellationToken );
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }
            _context.Events.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
