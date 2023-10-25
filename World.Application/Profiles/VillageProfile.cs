using AutoMapper;
using World.Application.DTOs.Village;
using World.Application.Features.Village.Command.CreateVillage;
using World.Application.Features.Village.Command.UpdateVillage;
using World.Domain;

namespace World.Application.Profiles
{
    public class VillageProfile : Profile
    {
        public VillageProfile()
        {
            CreateMap<CreateVillageCommand, Village>();
            CreateMap<Village, VillageDto>();
            CreateMap<UpdateVillageCommand, Village>();
        }
    }
}
