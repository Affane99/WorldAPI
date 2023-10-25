using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace World.Application.DTOs.Continent.Validators
{
    public class IContinentValidator : AbstractValidator<IContinentDto>
    {
        public IContinentValidator() 
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(255)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
