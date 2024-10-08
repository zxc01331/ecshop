using AutoMapper;
using eshop.Dtos.Users;
using eshop.Models;

namespace eshop.Profiles
{
    public class usersProfile : Profile
    {
        public usersProfile()
        {
            CreateMap<users, usersDto>();
        }
    }
}
