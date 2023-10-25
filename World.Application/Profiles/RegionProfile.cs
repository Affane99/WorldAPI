using AutoMapper;
using World.Application.DTOs.Region;
using World.Application.Features.Region.Command.CreateRegion;
using World.Application.Features.Region.Command.UpdateRegion;
using World.Domain;

namespace World.Application.Profiles
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<CreateRegionCommand, Region>();
            CreateMap<Region, RegionDto>();
            CreateMap<UpdateRegionCommand, Region>();
        }
    }
}
