using AutoMapper;
using World.Application.DTOs.Prefecture;
using World.Application.Features.Prefecture.Command.CreatePrefecture;
using World.Application.Features.Prefecture.Command.UpdatePrefecture;
using World.Domain;

namespace World.Application.Profiles
{
    public class PrefectureProfile : Profile
    {
        public PrefectureProfile()
        {
            CreateMap<CreatePrefectureCommand, Prefecture>();
            CreateMap<Prefecture, PrefectureDto>();
            CreateMap<UpdatePrefectureCommand, Prefecture>();
        }
    }
}
