using FluentValidation;
using System;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Prefecture.Command.CreatePrefecture
{
    public class CreatePrefectureValidator : AbstractValidator<CreatePrefectureCommand>
    {
        private readonly IPrefectureRepository _prefectureRepository;
        private readonly IRegionRepository _regionRepository;

        public CreatePrefectureValidator(IPrefectureRepository prefectureRepository, IRegionRepository regionRepository)
        {
            _prefectureRepository = prefectureRepository;
            _regionRepository = regionRepository;
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
                    var nameExist = await _prefectureRepository.ExistsAsync(v => v.Name.ToLower().Equals(name.ToLower()));
                    return !nameExist;
                })
                .WithMessage("{PropertyName} '{PropertyValue}' already exist.");
            RuleFor(x => x.RegionId)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (regionId, token) =>
                {
                    return await _regionRepository.ExistsAsync(v => v.Id.Equals(regionId));
                }).WithMessage("The {PropertyName} '{PropertyValue}' does not exist.");

        }
    }
}
