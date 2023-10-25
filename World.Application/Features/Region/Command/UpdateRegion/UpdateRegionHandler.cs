using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.Region.Command.UpdateRegion
{
    public class UpdateRegionHandler : IRequestHandler<UpdateRegionCommand, Unit>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public UpdateRegionHandler(IRegionRepository regionRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateRegionValidator(_regionRepository, _countryRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.IsValid == false)
            {
                throw new BadRequestException("Failed to Update", validation);
            }
            var region = await _regionRepository.FindByIdAsync(request.Id);
            _mapper.Map(request, region);
            await _regionRepository.UpdateAsync(region);

            return Unit.Value;
        }
    }
}
