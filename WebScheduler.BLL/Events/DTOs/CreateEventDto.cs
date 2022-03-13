using AutoMapper;
using System;
using WebScheduler.BLL.Events.Commands.CreateEvent;
using WebScheduler.BLL.Mapping;

namespace WebScheduler.BLL.Events.DTOs
{
    public class CreateEventDto : IMapWith<CreateEventCommand>
    {
        public string EventName { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateEventDto, CreateEventCommand>();
        }

    }
}
