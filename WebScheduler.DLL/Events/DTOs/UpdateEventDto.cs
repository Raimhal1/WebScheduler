using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebScheduler.BLL.Events.Commands.UpdateEvent;
using WebScheduler.BLL.Mapping;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.DTOs
{
    public class UpdateEventDto : IMapWith<UpdateEventCommand>
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }

        public DateTime StartEventDate { get; set; }

        public DateTime EndEventDate { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }
        public IList<User> Users { get; set; }
        public void Mapping (Profile profile)
        {
            profile.CreateMap<UpdateEventDto, UpdateEventCommand>()
                .ForMember(eventCommand => eventCommand.Id,
                    opt => opt.MapFrom(eventDto => eventDto.Id))
                 .ForMember(eventCommand => eventCommand.EventName,
                    opt => opt.MapFrom(eventDto => eventDto.EventName))
                .ForMember(eventCommand => eventCommand.StartEventDate,
                    opt => opt.MapFrom(eventDto => eventDto.StartEventDate))
                .ForMember(eventCommand => eventCommand.EndEventDate,
                    opt => opt.MapFrom(eventDto => eventDto.EndEventDate))
                .ForMember(eventCommand => eventCommand.ShortDescription,
                    opt => opt.MapFrom(eventDto => eventDto.Description))
                .ForMember(eventCommand => eventCommand.Users,
                    opt => opt.MapFrom(eventDto => eventDto.Users));
        }
    }
}
