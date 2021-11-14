using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.BLL.Interfaces;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.BLL.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly IReportDbContext _reportContext;
        private readonly IMapper _mapper;

        public TrackingService(IReportDbContext reportContext, IMapper mapper) =>
            (_reportContext, _mapper) = (reportContext, mapper);

        public async Task TrackChange<Entity, LogEntity>(Entity entity, Guid id)
            where Entity : class
            where LogEntity : class
        {
            var updateData = JsonConvert.SerializeObject(entity, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            var logData = new GeneralLogData
            {
                Time = DateTime.UtcNow,
                Data = updateData,
                enitityId = id
            };

            var loggedEntity = _mapper.Map<LogEntity>(logData);
            if (loggedEntity == null)
                throw new InvalidCastException("Invalid tracking entity");
        }
    }
}
