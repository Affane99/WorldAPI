using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Sector;
using World.Application.Exceptions;

namespace World.Application.Features.Sector.Query.GetSectorDetails
{
    public class GetSectorDetailsHandler : IRequestHandler<GetSectorDetailsQuery, SectorDto>
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;

        public GetSectorDetailsHandler(ISectorRepository sectorRepository, IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }
        public async Task<SectorDto> Handle(GetSectorDetailsQuery request, CancellationToken cancellationToken)
        {
            var validation = await new GetSectorDetailsValidator(_sectorRepository).ValidateAsync(request, cancellationToken);
            if(!validation.IsValid)
            {
                throw new BadRequestException("Getting details Failed", validation);
            }
            var sector = _sectorRepository.GetQuery("SubPrefecture.Prefecture.Region.Country.Continent").Where(x => x.Id.Equals(request.Id)).FirstOrDefault();
            SectorDto sectorDto = new SectorDto();
            _mapper.Map(sector, sectorDto);
            return sectorDto;
        }
    }
}
