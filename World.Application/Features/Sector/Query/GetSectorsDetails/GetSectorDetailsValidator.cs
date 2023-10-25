using FluentValidation;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Sector.Query.GetSectorDetails
{
    public class GetSectorDetailsValidator : AbstractValidator<GetSectorDetailsQuery>
    {
        private readonly ISectorRepository _sectorRepository;

        public GetSectorDetailsValidator(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    return await _sectorRepository.ExistsAsync(x => x.Id == id);
                }).WithMessage("The {PropertyName} with value \"{PropertyValue}\" does not exist.");
        }
    }
}
