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
    public class UserDto : IMapWith<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public virtual IList<Event> Events { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserDto, User>()
                .ForMember(user => user.FirstName,
                    opt => opt.MapFrom(userDto => userDto.FirstName))
                .ForMember(user => user.LastName,
                    opt => opt.MapFrom(userDto => userDto.LastName))
                .ForMember(user => user.UserName,
                    opt => opt.MapFrom(userDto => userDto.UserName))
                .ForMember(user => user.Email,
                    opt => opt.MapFrom(userDto => userDto.Email))
                .ForMember(user => user.Events,
                    opt => opt.MapFrom(userDto => userDto.Events))
                .ReverseMap();

        }
    }
}
