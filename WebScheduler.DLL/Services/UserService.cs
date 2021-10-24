using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.BLL.Interfaces;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Services
{
    class UserService : IUserService
    {
        private readonly IUserDbContext _userContext;
        private readonly IMapper _mapper;

        public UserService(IUserDbContext userContext, IMapper mapper) =>
            (_userContext, _mapper) = (userContext, mapper);

        public async Task<Guid> CreateAsync(UserDto model, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(model);
            user.Id = Guid.NewGuid();
            await _userContext.Users.AddAsync(user, cancellationToken);
            await _userContext.SaveChangesAsync(cancellationToken);
            return user.Id;    

        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if(id != Guid.Empty)
            {
                var user = await _userContext.Users.FindAsync(new object[] { id }, cancellationToken);
                if (user == null)
                {
                    throw new NotFoundException(nameof(User), id);
                }
                _userContext.Users.Remove(user);
                await _userContext.SaveChangesAsync(cancellationToken);   
                
            }
        }

        public async Task<UserListVm> GetAll()
        {
            var users = await _userContext.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new UserListVm { Users = users };
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), id);
            }
            return user;
        }
    }
}
