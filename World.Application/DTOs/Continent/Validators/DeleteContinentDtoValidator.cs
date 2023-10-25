using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Features.Continents.Requests.Commands;

namespace World.Application.DTOs.Continent.Validators
{
    public class DeleteContinentDtoValidator : AbstractValidator<DeleteContinentCommandRequest>
    {
        private readonly IContinentRepository _continentRepository;

        public DeleteContinentDtoValidator(IContinentRepository continentRepository)
        {
            _continentRepository = continentRepository;
            RuleFor(p => p.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(ExistAsync)
                .WithMessage("The {PropertyName} \"{PropertyValue}\" not exist");
        }

        private async Task<bool> ExistAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _continentRepository.ExistsAsync(c => c.Id.Equals(Id));
        }
    }
}
