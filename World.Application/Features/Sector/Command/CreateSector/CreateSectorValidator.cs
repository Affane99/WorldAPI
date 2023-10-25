using FluentValidation;
using System;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Sector.Command.CreateSector
{
    public class CreateSectorValidator : AbstractValidator<CreateSectorCommand>
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly ISubPrefectureRepository _subPrefectureRepository;

        public CreateSectorValidator(ISectorRepository sectorRepository, ISubPrefectureRepository subPrefectureRepository)
        {
            _sectorRepository = sectorRepository;
            _subPrefectureRepository = subPrefectureRepository;
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MinimumLength(2)
                .WithMessage("{PropertyName} must ⩾ {ComparisonValue} characters.")
                .MaximumLength(255)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.")
                .MustAsync(async (name, token) =>
                {
                    var nameExist = await _sectorRepository.ExistsAsync(v => v.Name.ToLower().Equals(name.ToLower()));
                    return !nameExist;
                })
                .WithMessage("{PropertyName} '{PropertyValue}' already exist.");
            RuleFor(x => x.SubPrefectureId)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (subPrefectureId, token) =>
                {
                    return await _subPrefectureRepository.ExistsAsync(v => v.Id.Equals(subPrefectureId));
                }).WithMessage("The {PropertyName} '{PropertyValue}' does not exist.");

        }
    }
}
