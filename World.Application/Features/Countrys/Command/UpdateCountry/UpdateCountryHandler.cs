using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.Countries.Command.UpdateCountry
{
    public class UpdateCountryHandler : IRequestHandler<UpdateCountryCommand, Unit>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IContinentRepository _continentRepository;
        private readonly IMapper _mapper;

        public UpdateCountryHandler(ICountryRepository countryRepository, IContinentRepository continentRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _continentRepository = continentRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCountryValidator(_countryRepository, _continentRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.IsValid == false)
            {
                throw new BadRequestException("Update Failed", validation);
            }
            var country = await _countryRepository.FindByIdAsync(request.Id);
            _mapper.Map(request, country);
            await _countryRepository.UpdateAsync(country);

            return Unit.Value;
        }
    }
}
