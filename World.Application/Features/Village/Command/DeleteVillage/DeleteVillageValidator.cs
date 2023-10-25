using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Village.Command.DeleteVillage
{
    public class DeleteVillageValidator : AbstractValidator<DeleteVillageCommand>
    {
        private readonly IVillageRepository _villageRepository;

        public DeleteVillageValidator(IVillageRepository villageRepository) 
        {
            _villageRepository = villageRepository;

            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    return await _villageRepository.ExistsAsync(x => x.Id == id);
                })
                .WithMessage("The {PropertyName} with value {PropertyValue} does not exist.");
        }
    }
}
