using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Country;

namespace World.Application.Features.Countries.Command.CreateCountry
{
    public class CreateCountryValidator : AbstractValidator<CreateCountryCommand>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IContinentRepository _continentRepository;

        public CreateCountryValidator(ICountryRepository countryRepository, IContinentRepository continentRepository)
        {
            _countryRepository = countryRepository;
            _continentRepository = continentRepository;
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
                    var nameExist = await _countryRepository.ExistsAsync(v => v.Name.ToLower().Equals(name.ToLower()));
                    return !nameExist;
                })
                .WithMessage("{PropertyName} '{PropertyValue}' already exist.");
            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MinimumLength(2)
                .WithMessage("{PropertyName} must be ⩾ {ComparisonValue} characters.")
                .MaximumLength(4)
                .MustAsync(async (code, token) =>
                {
                    var codeExist = await _countryRepository.ExistsAsync(v => v.CountryCode.ToLower().Equals(code.ToLower()));
                    return !codeExist;
                }).WithMessage("{PropertyName} '{PropertyValue}' already exist.");
            RuleFor(x => x.Capitale)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MinimumLength(2)
                .WithMessage("{PropertyName} must ⩾ {ComparisonValue} characters.")
                .MaximumLength(255)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.")
                .MustAsync(async (capitale, token) =>
                {
                    var capitaleExist = await _countryRepository.ExistsAsync(v => v.Capitale.ToLower().Equals(capitale.ToLower()));
                    return !capitaleExist;
                }).WithMessage("{PropertyName} '{PropertyValue}' already exist for other countrys.");
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
