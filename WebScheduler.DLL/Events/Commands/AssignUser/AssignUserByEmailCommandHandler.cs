using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.Commands.AssignUser
{
    class AssignUserByEmailCommandHandler : IRequestHandler<AssignUserByEmailCommand>
    {
        private readonly IEventDbContext _context;
        private readonly IUserDbContext _users;

        public AssignUserByEmailCommandHandler(IEventDbContext context, IUserDbContext users) =>
            (_context, _users) = (context, users);

        public async Task<Unit> Handle(AssignUserByEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.Users
                .FirstOrDefaultAsync(u =>
                u.Email == request.Email, cancellationToken);

            if(user == null)
                throw new NotFoundException(nameof(User), request.Email);

            var entity = await _context.Events
                .Include(u => u.Users)
                .FirstOrDefaultAsync(e => e.Id == request.EventId, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Event), request.EventId);


            if (entity.Users.Any(u => u.Id == user.Id))
                throw new Exception(message: "User already assigned");

            entity.Users.Add(user);

            _context.Events.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            await _users.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
