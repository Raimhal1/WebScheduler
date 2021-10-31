using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.Commands.AssignUser
{
    public class AssignUserCommandHandler : IRequestHandler<AssignUserCommand>
    {
        private readonly IEventDbContext _context;
        private readonly IUserDbContext _users;
        public AssignUserCommandHandler(IEventDbContext context, IUserDbContext users) =>
            (_context, _users) = (context, users);

        public async Task<Unit> Handle(AssignUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events
                .Include(u => u.Users)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }
            else if (entity.UserId != request.UserId)
            {
                var user = await _users.Users
                    .FindAsync(request.UserId);

                if (user == null || user.Id == Guid.Empty)
                {
                    throw new NotFoundException(nameof(User), request.UserId);
                }
                entity.Users.Add(user);
            }

            _context.Events.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            await _users.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
