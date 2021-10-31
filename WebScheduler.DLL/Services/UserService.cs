﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.BLL.Interfaces;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.BLL.Validation.Serializer;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Services
{
    class UserService : IUserService
    {
        private readonly IUserDbContext _userContext;
        private readonly IRoleDbContext _roleContext;
        private readonly IMapper _mapper;

        public UserService(IUserDbContext userContext, IRoleDbContext roleContext,IMapper mapper) =>
            (_userContext, _roleContext ,_mapper) = (userContext, roleContext, mapper);

        public async Task<Guid> CreateAsync(RegisterUserModel model, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(model);
            user.Id = Guid.NewGuid();
            user.Events = new List<Event>();

            var role = await _roleContext.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            user.Roles = new List<Role> { role };
            user.RefreshTokens = new List<RefreshToken>();

            await _userContext.Users.AddAsync(user, cancellationToken);
            await _userContext.SaveChangesAsync(cancellationToken);

            return user.Id;

        }

        public async Task  UpdateAsync(Guid id, RegisterUserModel model, CancellationToken cancellationToken)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), id);
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = user.UserName;
            user.Email = model.Email;
            user.Password = model.Password;

            _userContext.Users.Update(user);

            await _userContext.SaveChangesAsync(cancellationToken);
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
                .Include(u=> u.Events)
                .Include(u=> u.RefreshTokens)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new UserListVm { Users = users };
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userContext.Users
                .Include(u=>u.Events)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
