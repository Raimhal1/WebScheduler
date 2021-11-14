using AutoMapper;
using System;
using WebScheduler.BLL.Mapping;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.DtoModels
{
    public class EventFileDto : IMapWith<EventFile>
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public Guid EventId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EventFile, EventFileDto>();
        }
    }
}
