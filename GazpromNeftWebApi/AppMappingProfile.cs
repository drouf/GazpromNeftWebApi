using AutoMapper;
using GazpromNeftWebApi.Models;
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
        }
    }
}
