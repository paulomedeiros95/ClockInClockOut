using AutoMapper;
using ClockInClockOut_Domain.Domains;
using ClockInClockOut_Dto.Request;
using ClockInClockOut_Dto.Response;

namespace ClockInClockOut_API.MapperConfig
{
    public class ProfileMapperConfiguration : Profile
    {
        public ProfileMapperConfiguration()
        {
            CreateMap<LoginRequestDTO, UserDomain>();

            CreateMap<UserDomain, UserResponseDTO>();
            CreateMap<UserRequestDTO, UserDomain>();

            CreateMap<ProjectDomain, ProjectResponseDTO>().ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.Select(u => u.ID).ToList()));
            CreateMap<ProjectRequestDTO, ProjectDomain>().ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.Select(u => new UserDomain { ID = u }).ToList()));

            CreateMap<TimeDomain, TimeResponseDTO>();
            CreateMap<TimeRequestDTO, TimeDomain>();
        }
    }
}
