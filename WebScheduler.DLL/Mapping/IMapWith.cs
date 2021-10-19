using AutoMapper;

namespace WebScheduler.BLL.Mapping
{
    interface IMapWith<T>
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
