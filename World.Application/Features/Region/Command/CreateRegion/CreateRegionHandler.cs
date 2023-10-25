using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;
using World.Application.Responses;

namespace World.Application.Features.Region.Command.CreateRegion
{
    public class CreateRegionHandler : IRequestHandler<CreateRegionCommand, BaseCommandResponse>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CreateRegionHandler(IRegionRepository regionRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateRegionValidator(_regionRepository, _countryRepository);
            var validation = await validator.ValidateAsync(request);
            if(validation.IsValid == false)
            {
                throw new BadRequestException("Creation Failed", validation);
            }
            var region = _mapper.Map<Domain.Region>(request);
            region = await _regionRepository.CreateAsync(region);
            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Creation Successful",
                Id = region.Id
            };
            return response;
        }
    }
}
