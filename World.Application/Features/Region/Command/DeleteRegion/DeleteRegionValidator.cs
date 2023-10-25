using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Region.Command.DeleteRegion
{
    public class DeleteRegionValidator : AbstractValidator<DeleteRegionCommand>
    {
        private readonly IRegionRepository _regionRepository;

        public DeleteRegionValidator(IRegionRepository regionRepository) 
        {
            _regionRepository = regionRepository;

            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    return await _regionRepository.ExistsAsync(x => x.Id == id);
                })
                .WithMessage("The {PropertyName} with value {PropertyValue} does not exist.");
        }
    }
}
