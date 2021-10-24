﻿using System;
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
        public IList<User> Users { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventDetailsVm>();
        }
    }
}
