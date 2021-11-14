using AutoMapper;
using WebScheduler.BLL.Mapping;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.DtoModels
{
    public class UserVm : IMapWith<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserVm, User>()
                .ReverseMap();
        }
    }
}
