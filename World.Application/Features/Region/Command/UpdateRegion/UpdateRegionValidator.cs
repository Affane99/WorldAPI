using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Region.Command.UpdateRegion
{
    public class UpdateRegionValidator : AbstractValidator<UpdateRegionCommand>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;

        public UpdateRegionValidator(IRegionRepository regionRepository, ICountryRepository countryRepository)
        {
            _regionRepository = regionRepository;
            _countryRepository = countryRepository;
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (id, token) =>
                {
                    return await _regionRepository.ExistsAsync(y => y.Id == id);
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
                .MustAsync(async (region, token) =>
                {
                    var nameExist = await _regionRepository.ExistsAsync(v => v.Name.ToLower().Equals(region.Name.ToLower()) && !region.Id.Equals(v.Id));
                    return !nameExist;
                })
                .WithMessage($"The name already exist with another Region.");
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
