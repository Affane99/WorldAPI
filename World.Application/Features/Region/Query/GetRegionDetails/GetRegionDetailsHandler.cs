using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Region;
using World.Application.Exceptions;

namespace World.Application.Features.Region.Query.GetRegionDetails
{
    public class GetRegionDetailsHandler : IRequestHandler<GetRegionDetailsQuery, RegionDto>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public GetRegionDetailsHandler(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
        public async Task<RegionDto> Handle(GetRegionDetailsQuery request, CancellationToken cancellationToken)
        {
            var validation = await new GetRegionDetailsValidator(_regionRepository).ValidateAsync(request, cancellationToken);
            if(!validation.IsValid)
            {
                throw new BadRequestException("Getting details Failed", validation);
            }
            var region = _regionRepository.GetQuery("Country.Continent").Where(x => x.Id.Equals(request.Id)).FirstOrDefault();
            RegionDto regionDto = new RegionDto();
            _mapper.Map(region, regionDto);
            return regionDto;
        }
    }
}
