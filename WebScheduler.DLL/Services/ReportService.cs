using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;
using WebScheduler.BLL.Validation.Exceptions;
using WebScheduler.Domain.Interfaces;
using WebScheduler.Domain.Models;
using CsvHelper;
using System.Threading;
using WebScheduler.BLL.Events.Queries.GetEventList;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using WebScheduler.BLL.DtoModels;
using MediatR;
using AutoMapper.QueryableExtensions;

namespace WebScheduler.BLL.Services
{

    public class ReportService : IReportService
    {
        private readonly IFileDbContext _fileContext;
        private readonly IEventDbContext _context;
        private readonly IUserDbContext _userContext;
        private readonly IRoleDbContext _roleContext;
        private readonly IMapper _mapper;
        private string AdminRoleName = "Admin";
        public ReportService(IFileDbContext fileContext, IEventDbContext context,
            IUserDbContext userContext, IRoleDbContext roleContext, IMapper mapper) =>
            (_fileContext, _context, _userContext, _roleContext, _mapper)
            = (fileContext, context, userContext, roleContext, mapper);

        
        private Dictionary<string, Func<EventListVm, Stream>> ReportDictionary 
            = new Dictionary<string, Func<EventListVm, Stream>>()
        {
            { "csv",  CreateCsvReport },
            { "xml",  CreateXMLReport },
        };


        private static Stream CreateCsvReport(EventListVm events)
        {
            using(MemoryStream stream = new MemoryStream())
            {
                using(TextWriter textWriter = new StreamWriter(stream))
                {
                    using (CsvWriter csv = new CsvWriter(textWriter, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(events.Events);
                        textWriter.Flush();
                        return stream;
                    }
                }
            }
        }

        private static Stream CreateXMLReport(EventListVm events)
        {

            XmlSerializer xml = new XmlSerializer(typeof(EventDto));

            using(MemoryStream stream = new MemoryStream())
            {
                xml.Serialize(stream, events.Events);
                return stream;
            }
        }

        public async Task<Stream> CreateEventsReport(Guid id, string extension, CancellationToken cancellationToken)
        {
            var role = await _roleContext.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Name == AdminRoleName);

            List<EventLookupDto> eventQuery;

            if (role == null)
            {
                eventQuery = await _context.Events
                .Include(e => e.Users)
                .Where(e => e.UserId == id)
                .ProjectTo<EventLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            }
            else
            {
                eventQuery = await _context.Events
                .Include(e => e.Users)
                .ProjectTo<EventLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            }

            var eventListVm = new EventListVm { Events = eventQuery };

            for (int i = 0; i < eventListVm.Events.Count; i++)
            {
                eventListVm.Events[i].Users = _mapper.Map<List<UserVm>>(eventQuery[i].Users);
            }

            return ReportDictionary[extension].Invoke(eventListVm);
        }

        public async Task<string> CreateEventsMemberReport(Guid id, string extension)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateEventsReportForNextMonth(Guid id, string extension)
        {
            throw new NotImplementedException();
        }
    }
}
