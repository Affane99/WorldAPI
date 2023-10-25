using AutoMapper;
using World.Application.DTOs.Continent;
using World.Domain;

namespace World.Application.Profiles
{
    public class ContinentProfile : Profile  
    {
        public ContinentProfile() 
        {
            CreateMap<Continent,ContinentListDto>().ReverseMap();
            CreateMap<CreateContinentDto,Continent>().ReverseMap();
            CreateMap<UpdateContinentDto,Continent>().ReverseMap();
        }
    }
}
