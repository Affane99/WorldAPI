using FluentValidation;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Prefecture.Command.UpdatePrefecture
{
    public class UpdatePrefectureValidator : AbstractValidator<UpdatePrefectureCommand>
    {
        private readonly IPrefectureRepository _prefectureRepository;
        private readonly IRegionRepository _regionRepository;

        public UpdatePrefectureValidator(IPrefectureRepository prefectureRepository, IRegionRepository regionRepository)
        {
            _prefectureRepository = prefectureRepository;
            _regionRepository = regionRepository;
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (id, token) =>
                {
                    return await _prefectureRepository.ExistsAsync(y => y.Id == id);
                }).WithMessage("The {PropertyName} with value {PropertyValue} does not exist.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MinimumLength(2)
                .WithMessage("{PropertyName} must be ⩾ {ComparisonValue} characters.")
                .MaximumLength(255)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            RuleFor(x => x)
                .MustAsync(async (prefecture, token) =>
                {
                    var nameExist = await _prefectureRepository.ExistsAsync(v => v.Name.ToLower().Equals(prefecture.Name.ToLower()) && !prefecture.Id.Equals(v.Id));
                    return !nameExist;
                })
                .WithMessage($"The name already exist with another Prefecture.");
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
