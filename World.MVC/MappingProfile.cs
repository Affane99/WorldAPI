using AutoMapper;
using World.MVC.Models;
using World.MVC.Services;

namespace World.MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateContinentDto, CreateContinentVM>().ReverseMap();
            CreateMap<ContinentListDto, ContinentVM>().ReverseMap();
            CreateMap<ContinentVM, UpdateContinentDto>().ReverseMap();
        }
    }
}
