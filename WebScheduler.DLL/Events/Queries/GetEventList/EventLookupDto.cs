using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.BLL.Mapping;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.Queries.GetEventList
{
    public class EventLookupDto : IMapWith<Event>
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventLookupDto>()
                .ForMember(eDto => eDto.Id,
                    opt => opt.MapFrom(e => e.Id))
                .ForMember(eDto => eDto.EventName,
                    opt => opt.MapFrom(e => e.EventName));

        }
    }
}
