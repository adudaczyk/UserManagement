using AutoMapper;
using UserManagement.BusinessLogic.Models;
using UserManagement.EntityFramework.Models;

namespace UserManagement.BusinessLogic.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
