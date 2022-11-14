using AutoMapper;
using VolunteerRequestApp.Server.Core;
using VolunteerRequestApp.Shared.Dtos;

namespace VolunteerRequestApp.Server.Infrastructure
{
    public class AppAutoMapper : Profile
    {
        public AppAutoMapper()
        {
            CreateMap<StatusDto, Status>();
            CreateMap<Status, StatusDto>();
        }
    }
}