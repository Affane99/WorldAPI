using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;
using World.Application.Responses;
using World.Domain;

namespace World.Application.Features.Countries.Command.CreateCountry
{
    public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, BaseCommandResponse>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IContinentRepository _continentRepository;
        private readonly IMapper _mapper;

        public CreateCountryHandler(ICountryRepository countryRepository, IContinentRepository continentRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _continentRepository = continentRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCountryValidator(_countryRepository, _continentRepository);
            var validation = await validator.ValidateAsync(request);
            if(validation.IsValid == false)
            {
                throw new BadRequestException("Creation Failed", validation);
            }
            var country = _mapper.Map<Country>(request);
            country = await _countryRepository.CreateAsync(country);
            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Creation Successful",
                Id = country.Id
            };
            return response;
        }
    }
}
