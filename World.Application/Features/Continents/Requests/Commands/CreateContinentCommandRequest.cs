using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.DTOs.Continent;
using World.Application.Responses;

namespace World.Application.Features.Continents.Requests.Commands
{
    public class CreateContinentCommandRequest : IRequest<BaseCommandResponse>
    {
        public CreateContinentDto CreateContinent { get; set; }
    }
}
