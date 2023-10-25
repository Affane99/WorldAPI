using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Countries.Command.DeleteCountry
{
    public class DeleteCountryValidator : AbstractValidator<DeleteCountryCommand>
    {
        private readonly ICountryRepository _countryRepository;

        public DeleteCountryValidator(ICountryRepository countryRepository) 
        {
            _countryRepository = countryRepository;

            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    return await _countryRepository.ExistsAsync(x => x.Id == id);
                })
                .WithMessage("{PropertyName} with value {PropertyValue} does not exist.");
        }
    }
}
