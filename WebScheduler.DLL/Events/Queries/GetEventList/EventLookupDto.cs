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
        public DateTime StartEventDate { get; set; }

        public DateTime EndEventDate { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }
        public IList<User> Users { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventLookupDto>()
                .ForMember(eDto => eDto.Id,
                    opt => opt.MapFrom(e => e.Id))
                .ForMember(eDto => eDto.EventName,
                    opt => opt.MapFrom(e => e.EventName))
                .ForMember(eDto => eDto.StartEventDate,
                    opt => opt.MapFrom(e => e.StartEventDate))
                .ForMember(eDto => eDto.EndEventDate,
                    opt => opt.MapFrom(e => e.EndEventDate))
                .ForMember(eDto => eDto.ShortDescription,
                    opt => opt.MapFrom(e => e.ShortDescription))
                .ForMember(eDto => eDto.Description,
                    opt => opt.MapFrom(e => e.Description))
                .ForMember(eDto => eDto.Users,
                    opt => opt.MapFrom(e => e.Users));

        }
    }
}
