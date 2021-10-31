using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.BLL.Mapping;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.DtoModels
{
    public class EventFileDto : IMapWith<EventFile>
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Data { get; set; }
        public Guid EventId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EventFile, EventFileDto>()
                .ForMember(efDto => efDto.EventId, opt => 
                    opt.MapFrom(ef => ef.Event.Id));
        }
    }
}
