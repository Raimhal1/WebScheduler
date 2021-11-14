using AutoMapper;
using WebScheduler.BLL.Mapping;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.DtoModels
{
    public class GeneralFileDto : IMapWith<EventFile>
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EventFile, GeneralFileDto>();
        }
    }
}
