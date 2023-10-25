using AutoMapper;
using World.Application.DTOs.Sector;
using World.Application.Features.Sector.Command.CreateSector;
using World.Application.Features.Sector.Command.UpdateSector;
using World.Domain;

namespace World.Application.Profiles
{
    public class SectorProfile : Profile
    {
        public SectorProfile()
        {
            CreateMap<CreateSectorCommand, Sector>();
            CreateMap<Sector, SectorDto>();
            CreateMap<UpdateSectorCommand, Sector>();
        }
    }
}
