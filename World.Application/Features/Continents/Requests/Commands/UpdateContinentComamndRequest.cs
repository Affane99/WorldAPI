using MediatR;
using World.Application.DTOs.Continent;
using World.Application.Responses;

namespace World.Application.Features.Continents.Requests.Commands
{
    public class UpdateContinentComamndRequest : IRequest<BaseCommandResponse>
    {
        public UpdateContinentDto UpdateContinent { get; set; }
    }
}
