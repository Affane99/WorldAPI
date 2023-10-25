using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.DTOs.Continent.Validators;
using World.Application.Exceptions;
using World.Application.Features.Continents.Requests.Commands;
using World.Application.Contracts.Persistence;
using World.Application.Responses;
using World.Domain;
using World.Application.Contracts.Infrastucture;

namespace World.Application.Features.Continents.Handlers.Commands
{
    public class CreateContinentCommandHandler : IRequestHandler<CreateContinentCommandRequest, BaseCommandResponse>
    {
        private readonly IContinentRepository _continentRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public CreateContinentCommandHandler(IContinentRepository continentRepository, IMapper mapper, IEmailSender emailSender)
        {
            _continentRepository = continentRepository;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<BaseCommandResponse> Handle(CreateContinentCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateContinentDtoValidator(_continentRepository);
            var validation = await validator.ValidateAsync(request.CreateContinent);
            BaseCommandResponse response;
            if (validation.IsValid == false)
            {
                response = new BaseCommandResponse
                {
                    Success = false,
                    Message = "Creation Failed",
                    Id = Guid.Empty,
                    Errors = validation.Errors.Select(v => $"•{v.ErrorMessage}" + Environment.NewLine).ToList()
                };
            }
            else
            {
                var continent = _mapper.Map<Continent>(request.CreateContinent);
                continent = await _continentRepository.CreateAsync(continent);
                response = new BaseCommandResponse
                {
                    Success = true,
                    Message = "Creation Successful",
                    Id = continent.Id
                };
            }

            if (false)
            {
                /*var email = new Models.Email
                {
                    To = "affana@netsengroup.com",
                    Body = $"Le Continent {continent.Name} a été ajouté le {continent.CreatedDate:F}",
                    Subject = "Ajout d'un nouveau continent"
                };
                try
                {
                    var d = await _emailSender.SendEmailAsync(email);
                }
                catch (Exception)
                {

                    throw;
                }*/
            }

            return response;
        }
    }
}
