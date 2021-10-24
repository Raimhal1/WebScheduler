using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebScheduler.BLL.Events.Commands.CreateEvent;
using WebScheduler.BLL.Mapping;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.DTOs
{
    public class CreateEventDto : IMapWith<CreateEventCommand>
    {
        public string EventName { get; set; }

        public DateTime StartEventDate { get; set; }

        public DateTime EndEventDate { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }
        public IList<User> Users { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateEventDto, CreateEventCommand>()
                .ForMember(eventCommand => eventCommand.EventName,
                    opt => opt.MapFrom(eventDto => eventDto.EventName))
                .ForMember(eventCommand => eventCommand.StartEventDate,
                    opt => opt.MapFrom(eventDto => eventDto.StartEventDate))
                .ForMember(eventCommand => eventCommand.EndEventDate,
                    opt => opt.MapFrom(eventDto => eventDto.EndEventDate))
                .ForMember(eventCommand => eventCommand.ShortDescription,
                    opt => opt.MapFrom(eventDto => eventDto.ShortDescription))
                .ForMember(eventCommand => eventCommand.Description,
                    opt => opt.MapFrom(eventDto => eventDto.Description))
                .ForMember(eventCommand => eventCommand.Users,
                    opt => opt.MapFrom(eventDto => eventDto.Users));
        }
    }
}
