using AutoMapper;
using World.Application.DTOs.SubPrefecture;
using World.Application.Features.SubPrefecture.Command.CreateSubPrefecture;
using World.Application.Features.SubPrefecture.Command.UpdateSubPrefecture;
using World.Domain;

namespace World.Application.Profiles
{
    public class SubPrefectureProfile : Profile
    {
        public SubPrefectureProfile()
        {
            CreateMap<CreateSubPrefectureCommand, SubPrefecture>();
            CreateMap<SubPrefecture, SubPrefectureDto>();
            CreateMap<UpdateSubPrefectureCommand, SubPrefecture>();
        }
    }
}
