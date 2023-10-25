using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using World.Application.Contracts.Persistence;

namespace World.Application.DTOs.Continent.Validators
{
    public class UpdateContinentDtoValidator : AbstractValidator<UpdateContinentDto>
    {
        private readonly IContinentRepository _continentRepository;

        public UpdateContinentDtoValidator(IContinentRepository continentRepository) 
        {
            _continentRepository = continentRepository;

            Include(new IContinentValidator());
            RuleFor(p => p.Id)
                .NotNull().NotEmpty().WithMessage("{PropertyName} must be present")
                .MustAsync(async (id, token) =>
                {
                    return await _continentRepository.ExistsAsync(y => y.Id == id);
                }).WithMessage("The {PropertyName} with value {PropertyValue} does not exist.");
            RuleFor(p => p)
                .MustAsync(ContinentNameNotExist)
                .WithMessage("The continent name already Exists.");
        }

        public async Task<bool> ContinentNameNotExist(UpdateContinentDto updateContinent, CancellationToken cancellationToken)
        {
            bool exist = await _continentRepository.ExistsAsync(c => c.Name.ToLower().Equals(updateContinent.Name.ToLower()) && c.Id != updateContinent.Id);
            return !exist;
        }
    }
}
