using FluentValidation;
using System;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Region.Command.CreateRegion
{
    public class CreateRegionValidator : AbstractValidator<CreateRegionCommand>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;

        public CreateRegionValidator(IRegionRepository regionRepository, ICountryRepository countryRepository)
        {
            _regionRepository = regionRepository;
            _countryRepository = countryRepository;
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MinimumLength(3)
                .WithMessage("{PropertyName} must ⩾ {ComparisonValue} characters.")
                .MaximumLength(255)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.")
                .MustAsync(async (name, token) =>
                {
                    var nameExist = await _regionRepository.ExistsAsync(v => v.Name.ToLower().Equals(name.ToLower()));
                    return !nameExist;
                })
                .WithMessage("{PropertyName} '{PropertyValue}' already exist.");
            RuleFor(x => x.CountryId)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (countryId, token) =>
                {
                    return await _countryRepository.ExistsAsync(v => v.Id.Equals(countryId));
                }).WithMessage("The {PropertyName} '{PropertyValue}' does not exist.");

        }
    }
}
