using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.BLL.Interfaces;
using WebScheduler.BLL.Validation;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Services
{
    class UserService : IUserService
    {
        private readonly IUserDbContext _userContext;
        private readonly IRoleDbContext _roleContext;
        private readonly IMapper _mapper;
        private readonly int saltSize = 16;

        public UserService(IUserDbContext userContext, IRoleDbContext roleContext,IMapper mapper) =>
            (_userContext, _roleContext ,_mapper) = (userContext, roleContext, mapper);

        public async Task<Guid> CreateAsync(RegisterUserModel model, CancellationToken cancellationToken)
        {
            if (await _userContext.Users.SingleOrDefaultAsync(u =>
                 u.Email == model.Email, cancellationToken) != null)
                throw new InvalidCastException("A user with this email already exists");

            var user = _mapper.Map<User>(model);
            user.Id = Guid.NewGuid();
            user.Events = new List<Event>();

            var role = await _roleContext.Roles
                .FirstOrDefaultAsync(r =>
                r.Name == "User", cancellationToken);

            user.Roles = new List<Role> { role };
            user.RefreshTokens = new List<RefreshToken>();
            user.Reports = new List<Report>();

            user.Salt = Hasher.GenerateSalt(saltSize);
            user.Password = Hasher.GetSaltedHash(user.Password, user.Salt);

            await _userContext.Users.AddAsync(user, cancellationToken);
            await _userContext.SaveChangesAsync(cancellationToken);

            return user.Id;

        }

        public async Task  UpdateAsync(Guid id, RegisterUserModel model, CancellationToken cancellationToken)
        {
            var user = await _userContext.Users
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(User), id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = user.UserName;
            user.Email = model.Email;
            user.Password = Hasher.GetSaltedHash(model.Password, user.Salt);

            _userContext.Users.Update(user);

            await _userContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if(id != Guid.Empty)
            {
                var user = await _userContext.Users.FindAsync(new object[] { id }, cancellationToken);
                if (user == null)
                    throw new NotFoundException(nameof(User), id);
                _userContext.Users.Remove(user);
                await _userContext.SaveChangesAsync(cancellationToken);   
                
            }
        }
        public async Task<List<UserDto>> GetAll()
        {
            return await _userContext.Users
                .Include(u=> u.Events)
                .Include(u=> u.RefreshTokens)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<UserDto> GetByIdAsync(Guid id) 
        { 
            Expression<Func<User, bool>> expression = u => u.Id == id;

            var user = await getUser(expression, id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<User> getUser(Expression<Func<User, bool>> expression, object key)
        {
            var user = await _userContext.Users
                .Include(u => u.Roles)
                .Include(u => u.Events)
                .FirstOrDefaultAsync(expression);

            if (user == null)
                throw new NotFoundException(nameof(User), key);

            return user;

        }

        public async Task<Guid> getIdFromEmail(string email)
        {
            Expression<Func<User, bool>> expression = u => u.Email == email;
            var user = await getUser(expression, email);
            return user.Id;
        }
    }
}
