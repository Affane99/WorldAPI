using FluentValidation;
using System;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Village.Command.CreateVillage
{
    public class CreateVillageValidator : AbstractValidator<CreateVillageCommand>
    {
        private readonly IVillageRepository _villageRepository;
        private readonly ISectorRepository _sectorRepository;

        public CreateVillageValidator(IVillageRepository villageRepository, ISectorRepository sectorRepository)
        {
            _villageRepository = villageRepository;
            _sectorRepository = sectorRepository;
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MinimumLength(2)
                .WithMessage("{PropertyName} must ⩾ {ComparisonValue} characters.")
                .MaximumLength(255)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.")
                .MustAsync(async (name, token) =>
                {
                    var nameExist = await _villageRepository.ExistsAsync(v => v.Name.ToLower().Equals(name.ToLower()));
                    return !nameExist;
                })
                .WithMessage("{PropertyName} '{PropertyValue}' already exist.");
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
