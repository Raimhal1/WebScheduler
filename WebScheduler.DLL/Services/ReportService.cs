using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;
using CsvHelper;
using System.Threading;
using WebScheduler.BLL.Events.Queries.GetEventList;
using System.Globalization;
using System.Xml.Serialization;
using WebScheduler.BLL.DtoModels;
using System.Linq.Expressions;
using WebScheduler.BLL.Events;
using WebScheduler.BLL.Validation.Exceptions;

namespace WebScheduler.BLL.Services
{

    public class ReportService : IReportService
    {
        private readonly IEventDbContext _context;
        private readonly IRoleDbContext _roleContext;
        private readonly IReportDbContext _reportContext;
        private readonly IUserDbContext _userContext;
        private readonly IMapper _mapper;
        private readonly string AdminRoleName = "Admin";
        public ReportService( IEventDbContext context, IRoleDbContext roleContext,
            IReportDbContext reportContext, IUserDbContext userContext, IMapper mapper) =>
            (_context, _roleContext, _userContext, _reportContext, _mapper)
            = (context, roleContext, userContext, reportContext, mapper);

        
        private readonly Dictionary<string, Func<List<EventLookupDto>, byte[]>> ReportDictionary 
            = new Dictionary<string, Func<List<EventLookupDto>, byte[]>>()
        {
            { "csv",  CreateCsvReport },
            { "xml",  CreateXMLReport },
        };


        private static byte[] CreateCsvReport(List<EventLookupDto> events)
        {
            using(MemoryStream stream = new MemoryStream())
            {
                using(TextWriter textWriter = new StreamWriter(stream))
                {
                    using (CsvWriter csv = new CsvWriter(textWriter, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(events);
                        textWriter.Flush();
                        return stream.ToArray();
                    }
                }
            }
        }

        private static byte[] CreateXMLReport(List<EventLookupDto> events)
        {
            using(MemoryStream stream = new MemoryStream())
            {
                    XmlSerializer xml = new XmlSerializer(typeof(List<EventLookupDto>));
                    xml.Serialize(stream, events);
                    return stream.ToArray();
            }
        }

        public async Task<ReportDto> CreateEventsReport(Guid id, string extension, CancellationToken cancellationToken)
        {
            var role = await _roleContext.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r =>
                r.Name == AdminRoleName 
                && r.Users.Any(u => u.Id == id),
                cancellationToken);

            Expression<Func<Event, bool>> expression;

            if (role == null)
                expression = e => e.UserId == id;
            else
                expression = e => true;

            return await GetReportTemplate(_context, _mapper, expression, extension, id, cancellationToken);
        }

        public async Task<ReportDto> CreateEventsMemberReport(Guid id, string extension, CancellationToken cancellationToken)
        {
            Expression<Func<Event, bool>> expression = e =>
                e.Users.Any(u => u.Id == id && u.Id != e.UserId);

            return await GetReportTemplate(_context, _mapper, expression, extension, id, cancellationToken);
        }

        public async Task<ReportDto> CreateEventsReportForNextMonth(Guid id, string extension, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            Expression<Func<Event,bool>> expression = e =>
                e.StartEventDate >= now 
                && e.StartEventDate <= now.AddDays(30);

            return await GetReportTemplate(_context, _mapper, expression, extension, id, cancellationToken);
        }

        private async Task<ReportDto> GetReportTemplate(IEventDbContext _context, IMapper _mapper,
            Expression<Func<Event, bool>> expression, string extension, Guid userId, CancellationToken cancellationToken)
        {
            var eventQuery = await LookUp.GetLookupEventListAll(_context, _mapper, expression, cancellationToken);

            var content = ReportDictionary[extension].Invoke(eventQuery);
            var now = DateTime.UtcNow;
            var fileName = $"{userId}_events_{now}.{extension}";
            var contentType = $"files/{extension}";

            Expression<Func<Report, bool>> reportExpression = r =>
                r.Content.SequenceEqual(content);

            var report = await _reportContext.Reports
                .FirstOrDefaultAsync(reportExpression,
                cancellationToken);

            if (report == null)
            {

                var user = await _userContext.Users
                    .Include(u => u.Reports)
                    .FirstOrDefaultAsync(u => u.Id == userId,
                    cancellationToken);

                if (user == null)
                    throw new NotFoundException(nameof(User), userId);

                report = new Report
                {
                    Id = Guid.NewGuid(),
                    FileName = fileName,
                    ContentType = contentType,
                    Content = content,
                    UserId = user.Id
                };
                await _reportContext.Reports.AddAsync(report, cancellationToken);
                await _reportContext.SaveChangesAsync(cancellationToken);
            }

            return _mapper.Map<ReportDto>(report);
        }

    }
}
