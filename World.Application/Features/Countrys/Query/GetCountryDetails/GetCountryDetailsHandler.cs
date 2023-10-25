using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Country;
using World.Application.Exceptions;

namespace World.Application.Features.Countrys.Query.GetCountryDetails
{
    public class GetCountryDetailsHandler : IRequestHandler<GetCountryDetailsQuery, CountryListDto>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetCountryDetailsHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<CountryListDto> Handle(GetCountryDetailsQuery request, CancellationToken cancellationToken)
        {
            var validation = await new GetCountryDetailsValidator(_countryRepository).ValidateAsync(request, cancellationToken);
            if(!validation.IsValid)
            {
                throw new BadRequestException("Getting details Failed", validation);
            }
            var country = _countryRepository.GetQuery("Continent").Where(x => x.Id.Equals(request.Id)).FirstOrDefault();
            CountryListDto countryDto = new CountryListDto();
            _mapper.Map(country, countryDto);
            return countryDto;
        }
    }
}
