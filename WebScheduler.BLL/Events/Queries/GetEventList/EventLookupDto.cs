using AutoMapper;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using WebScheduler.BLL.DtoModels;
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
        public Status Status { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventLookupDto>();

        }
    }
}
