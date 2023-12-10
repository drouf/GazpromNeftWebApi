using AutoMapper;
using GazpromNeftDomain.Entities;
using GazpromNeftWebApi.DTO;
using GazpromNeftWebApi.Requests;

namespace GazpromNeftWebApi
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() 
        {
            CreateMap<CreateUserRequest,User>();
            CreateMap<UpdateUserRequest,User>();
            CreateMap<PatchUserRequest, User>();
            CreateMap<User, UserDto>();
            CreateMap<IEnumerable<User>, IEnumerable<UserDto>>();
        }
    }
}
