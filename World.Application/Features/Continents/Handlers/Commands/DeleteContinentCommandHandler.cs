using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Exceptions;
using World.Application.Features.Continents.Requests.Commands;
using World.Application.Contracts.Persistence;
using World.Domain;
using World.Application.DTOs.Continent.Validators;

namespace World.Application.Features.Continents.Handlers.Commands
{
    public class DeleteContinentCommandHandler : IRequestHandler<DeleteContinentCommandRequest, Unit>
    {
        private readonly IContinentRepository _continentRepository;

        public DeleteContinentCommandHandler(IContinentRepository continentRepository)
        {
            _continentRepository = continentRepository;
        }
        public async Task<Unit> Handle(DeleteContinentCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new DeleteContinentDtoValidator(_continentRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.IsValid == false)
            {
                throw new BadRequestException("Delete Failed", validation);
            }
            var continent = await _continentRepository.FindByIdAsync(request.Id);
            if (continent is null)
            {
                throw new NotFoundException(nameof(Continent), request.Id);
            }
            await _continentRepository.DeleteAsync(continent);
            return Unit.Value;
        }
    }
}
