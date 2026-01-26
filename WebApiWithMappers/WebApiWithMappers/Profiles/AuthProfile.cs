using AutoMapper;
using WebApiWithMappers.Entities.Auth;
using WebApiWithMappers.Entities.DTOs.AuthDtos;

namespace WebApiWithMappers.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
			CreateMap<RegisterDto, AppUser>().ReverseMap();
		}
    }
}
