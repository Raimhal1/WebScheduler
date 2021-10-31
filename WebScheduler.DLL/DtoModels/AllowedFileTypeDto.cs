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
    public class AllowedFileTypeDto : IMapWith<AllowedFileType>
    {
        public string FileType { get; set; }
        public double FileSize { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AllowedFileType, AllowedFileTypeDto>().ReverseMap();
        }

    }
}
