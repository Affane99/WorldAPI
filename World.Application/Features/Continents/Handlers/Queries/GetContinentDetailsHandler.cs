using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.DTOs.Continent;
using World.Application.Features.Continents.Requests.Queries;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Continents.Handlers.Queries
{
    public class GetContinentDetailsHandler : IRequestHandler<GetContinentDetailsRequest, ContinentListDto>
    {
        private readonly IContinentRepository _continentRepository;
        private readonly IMapper _mapper;

        public GetContinentDetailsHandler(IContinentRepository continentRepository, IMapper mapper)
        {
            _continentRepository = continentRepository;
            _mapper = mapper;
        }
        public async  Task<ContinentListDto> Handle(GetContinentDetailsRequest request, CancellationToken cancellationToken)
        {
            var continent = await _continentRepository.FindByIdAsync(request.Id);
            return _mapper.Map<ContinentListDto>(continent);
        }
    }
}
