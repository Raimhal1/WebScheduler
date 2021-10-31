using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.BLL.Events.Commands.AssignUser;
using WebScheduler.BLL.Mapping;

namespace WebScheduler.BLL.Events.DTOs
{
    public class AssignUserToEventDto: IMapWith<AssignUserCommand>
    {
        public Guid Id { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AssignUserToEventDto, AssignUserCommand>();
        }
    }
}
