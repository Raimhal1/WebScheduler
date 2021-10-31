﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IFileDbContext _fileContext;
        private readonly IEventDbContext _context;
        private readonly IUserDbContext _userContext;
        private readonly IMapper _mapper;

        public FileService(IFileDbContext fileContext, IEventDbContext context, IUserDbContext userContext, IMapper mapper) =>
            (_fileContext, _context, _userContext, _mapper) = (fileContext, context, userContext, mapper);

        public Task<string> ConvertToByte64(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateEventListReport(Guid UserId)
        {
            if(UserId == Guid.Empty)
            {
                throw new NotFoundException(nameof(User), UserId);
            }

            var events = await _context.Events
                .Where(e => e.UserId == UserId).ToListAsync();


            if (events == null || events.Count == 0)
            {
                throw new NotFoundException(nameof(Event), $" where user id is equal {UserId}");
            }

            var builder = new StringBuilder();

            builder.AppendLine("id, Event name, Start event date, End event date," +
                " short description, description");

            foreach(var e in events)
            {
                builder.AppendLine($"{e.Id},{e.EventName},{e.StartEventDate}," +
                    $"{e.EndEventDate},{e.ShortDescription},{e.Description}");
            }

            return builder.ToString();
        }

        public async Task<string> CreateEventListMemberReport(Guid UserId)
        {
            var user = await _userContext.Users.FindAsync(UserId);

            if (user == null || user.Id == Guid.Empty)
            {
                throw new NotFoundException(nameof(User), UserId);
            }

            var events = await _context.Events
                .Include(e => e.Users)
                .Where(e => e.UserId != UserId && e.Users.Contains(user))
                .ToListAsync();

            if (events == null)
            {
                throw new NotFoundException(nameof(Event), $" where user id is equal {UserId}");
            }

            var builder = new StringBuilder();

            builder.AppendLine("id, Event name, Start event date, End event date," +
                " short description, description");

            foreach (var e in events)
            {
                builder.AppendLine($"{e.Id},{e.EventName},{e.StartEventDate}," +
                    $"{e.EndEventDate},{e.ShortDescription},{e.Description}");
            }

            return builder.ToString();
        }

    }
}
