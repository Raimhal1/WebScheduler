using System;
using System.Collections.Generic;
using WebScheduler.BLL.Mapping;
using WebScheduler.Domain.Models;
using AutoMapper;
namespace WebScheduler.BLL.Events.Queries.GetEventDetails
{
    public class EventDetailsVm : IMapWith<Event>
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventDetailsVm>()
                .ForMember(eventVm => eventVm.EventName,
                    opt => opt.MapFrom(e => e.StartEventDate))
                .ForMember(eventVm => eventVm.EventName,
                    opt => opt.MapFrom(e => e.StartEventDate))
                .ForMember(eventVm => eventVm.EndEventDate,
                    opt => opt.MapFrom(e => e.EndEventDate))
                .ForMember(eventVm => eventVm.Id,
                    opt => opt.MapFrom(e => e.Id))
                .ForMember(eventVm => eventVm.ShortDescription,
                    opt => opt.MapFrom(e => e.ShortDescription))
                .ForMember(eventVm => eventVm.Description,
                    opt => opt.MapFrom(e => e.Description))
                .ForMember(eventVm => eventVm.Users,
                    opt => opt.MapFrom(e => e.Users));

        }
    }
}
