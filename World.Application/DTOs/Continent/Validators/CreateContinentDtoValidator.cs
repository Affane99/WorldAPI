using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;

namespace World.Application.DTOs.Continent.Validators
{
    public class CreateContinentDtoValidator : AbstractValidator<CreateContinentDto>
    {
        private readonly IContinentRepository _continentRepository;

        public CreateContinentDtoValidator(IContinentRepository continentRepository) 
        {
            _continentRepository = continentRepository;
            Include(new IContinentValidator());
            RuleFor(p => p.Name)
                .MustAsync(ContinentNameNotExist)
                .WithMessage("{PropertyName} already Exists.");
        }

        public async Task<bool> ContinentNameNotExist(string name, CancellationToken cancellationToken)
        {
            bool exist = await _continentRepository.ExistsAsync(c => c.Name.ToLower().Equals(name.ToLower()));
            return !exist; 
        }
    }
}
