using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;
using WebScheduler.BLL.Validation.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebScheduler.BLL.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IEventDbContext _context;
        private readonly IRoleDbContext _roleContext;
        private readonly string AdminRoleName = "Admin";
        public DeleteEventCommandHandler(IEventDbContext context, IRoleDbContext roleContext) =>
            (_context, _roleContext) = (context, roleContext);
        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleContext.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Name == AdminRoleName
                && r.Users.Any(u => u.Id == request.UserId),
                cancellationToken);

            var entity = await _context.Events.FindAsync(new object[] { request.Id }, cancellationToken );
            if (entity == null || entity.UserId != request.UserId)
            {
                if(role == null)
                    throw new NotFoundException(nameof(Event), request.Id);
            }
            _context.Events.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
