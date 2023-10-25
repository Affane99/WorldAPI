using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using World.Application.DTOs.Continent.Validators;
using World.Application.Exceptions;
using World.Application.Features.Continents.Requests.Commands;
using World.Application.Contracts.Persistence;
using System.Linq;
using World.Application.Responses;

namespace World.Application.Features.Continents.Handlers.Commands
{
    public class UpdateContinentComamndHandler : IRequestHandler<UpdateContinentComamndRequest, BaseCommandResponse>
    {
        private readonly IContinentRepository _continentRepository;
        private readonly IMapper _mapper;

        public UpdateContinentComamndHandler(IContinentRepository continentRepository, IMapper mapper)
        {
            _continentRepository = continentRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateContinentComamndRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateContinentDtoValidator(_continentRepository);
            var validation = await validator.ValidateAsync(request.UpdateContinent);
            BaseCommandResponse response;
            if (validation.IsValid == false)
            {
                response = new BaseCommandResponse
                {
                    Success = false,
                    Message = "Update Failed",
                    Id = Guid.Empty,
                    Errors = validation.Errors.Select(v => $"•{v.ErrorMessage}" + Environment.NewLine).ToList()
                };
            }
            else
            {
                var continent = await _continentRepository.FindByIdAsync(request.UpdateContinent.Id);

                _mapper.Map(request.UpdateContinent, continent);

                await _continentRepository.UpdateAsync(continent);
                response = new BaseCommandResponse
                {
                    Success = true,
                    Message = "Update Successful",
                    Id = continent.Id
                };
            }

            return response;
        }
    }
}
