using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Countries.Command.UpdateCountry
{
    public class UpdateCountryValidator : AbstractValidator<UpdateCountryCommand>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IContinentRepository _continentRepository;

        public UpdateCountryValidator(ICountryRepository countryRepository, IContinentRepository continentRepository)
        {
            _countryRepository = countryRepository;
            _continentRepository = continentRepository;
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (id, token) =>
                {
                    return await _countryRepository.ExistsAsync(y => y.Id == id);
                }).WithMessage("The {PropertyName} with value {PropertyValue} does not exist.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MinimumLength(3)
                .WithMessage("{PropertyName} must be ⩾ {ComparisonValue} characters.")
                .MaximumLength(255)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            RuleFor(x => x)
                .MustAsync(async (country, token) =>
                {
                    var nameExist = await _countryRepository.ExistsAsync(v => v.Name.ToLower().Equals(country.Name.ToLower()) && !country.Id.Equals(v.Id));
                    return !nameExist;
                })
                .WithMessage($"The name already exist with another Country.");

            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MinimumLength(2)
                .WithMessage("{PropertyName} must be ⩾ {ComparisonValue} characters.")
                .MaximumLength(4);
            RuleFor(x => x)
                .MustAsync(async (country, token) =>
                {
                    var codeExist = await _countryRepository.ExistsAsync(v => v.CountryCode.ToLower().Equals(country.CountryCode.ToLower()) && !v.Id.Equals(country.Id));
                    return !codeExist;
                }).WithMessage("Country Code already exist with another Country.");
            RuleFor(x => x.Capitale)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MinimumLength(2)
                .WithMessage("{PropertyName} must ⩾ {ComparisonValue} characters.")
                .MaximumLength(255)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            RuleFor(x => x)
                .MustAsync(async (country, token) =>
                {
                    var CapitaleExist = await _countryRepository.ExistsAsync(v => v.Capitale.ToLower().Equals(country.Capitale.ToLower()) && !v.Id.Equals(country.Id));
                    return !CapitaleExist;
                }).WithMessage("Capitale already exist with another Country.");
            RuleFor(x => x.ContinentId)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (continentId, token) =>
                {
                    return await _continentRepository.ExistsAsync(v => v.Id.Equals(continentId));
                }).WithMessage("The {PropertyName} '{PropertyValue}' does not exist.");
        }
    }
}
