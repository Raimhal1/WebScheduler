using AutoMapper;
using WebScheduler.BLL.Mapping;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.DtoModels
{
    public class ReportDto : IMapWith<Report>
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Report, ReportDto>();
        }
    }
}
