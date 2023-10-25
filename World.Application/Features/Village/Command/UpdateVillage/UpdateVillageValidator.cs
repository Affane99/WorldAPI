using FluentValidation;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Village.Command.UpdateVillage
{
    public class UpdateVillageValidator : AbstractValidator<UpdateVillageCommand>
    {
        private readonly IVillageRepository _villageRepository;
        private readonly ISectorRepository _sectorRepository;

        public UpdateVillageValidator(IVillageRepository villageRepository, ISectorRepository sectorRepository)
        {
            _villageRepository = villageRepository;
            _sectorRepository = sectorRepository;
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (id, token) =>
                {
                    return await _villageRepository.ExistsAsync(y => y.Id == id);
                }).WithMessage("The {PropertyName} with value {PropertyValue} does not exist.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MinimumLength(2)
                .WithMessage("{PropertyName} must be ⩾ {ComparisonValue} characters.")
                .MaximumLength(255)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            RuleFor(x => x)
                .MustAsync(async (village, token) =>
                {
                    var nameExist = await _villageRepository.ExistsAsync(v => v.Name.ToLower().Equals(village.Name.ToLower()) && !village.Id.Equals(v.Id));
                    return !nameExist;
                })
                .WithMessage($"The name already exist with another Village.");
            RuleFor(x => x.SectorId)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (sectorId, token) =>
                {
                    return await _sectorRepository.ExistsAsync(v => v.Id.Equals(sectorId));
                }).WithMessage("The {PropertyName} '{PropertyValue}' does not exist.");
        }
    }
}
