using AutoMapper;
using LibraryManagement2.Data.Entities;
using LibraryManagement2.Shared.DTO.MainData;

namespace LibraryManagement2.AutoMapper.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
   
            CreateMap<RegisterDto, User>();
            CreateMap<User, RegisterResponseDto>();
            CreateMap<User, UserTokenDto>();
        }
    }
}
