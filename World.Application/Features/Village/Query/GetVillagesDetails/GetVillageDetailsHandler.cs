using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Village;
using World.Application.Exceptions;

namespace World.Application.Features.Village.Query.GetVillageDetails
{
    public class GetVillageDetailsHandler : IRequestHandler<GetVillageDetailsQuery, VillageDto>
    {
        private readonly IVillageRepository _villageRepository;
        private readonly IMapper _mapper;

        public GetVillageDetailsHandler(IVillageRepository villageRepository, IMapper mapper)
        {
            _villageRepository = villageRepository;
            _mapper = mapper;
        }
        public async Task<VillageDto> Handle(GetVillageDetailsQuery request, CancellationToken cancellationToken)
        {
            var validation = await new GetVillageDetailsValidator(_villageRepository).ValidateAsync(request, cancellationToken);
            if(!validation.IsValid)
            {
                throw new BadRequestException("Getting details Failed", validation);
            }
            var village = _villageRepository.GetQuery("Sector.SubPrefecture.Prefecture.Region.Country.Continent").Where(x => x.Id.Equals(request.Id)).FirstOrDefault();
            VillageDto villageDto = new VillageDto();
            _mapper.Map(village, villageDto);
            return villageDto;
        }
    }
}
