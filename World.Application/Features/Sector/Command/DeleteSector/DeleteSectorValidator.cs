using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Sector.Command.DeleteSector
{
    public class DeleteSectorValidator : AbstractValidator<DeleteSectorCommand>
    {
        private readonly ISectorRepository _sectorRepository;

        public DeleteSectorValidator(ISectorRepository sectorRepository) 
        {
            _sectorRepository = sectorRepository;

            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    return await _sectorRepository.ExistsAsync(x => x.Id == id);
                })
                .WithMessage("The {PropertyName} with value {PropertyValue} does not exist.");
        }
    }
}
