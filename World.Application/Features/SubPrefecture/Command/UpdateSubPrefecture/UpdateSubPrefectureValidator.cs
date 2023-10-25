using FluentValidation;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.SubPrefecture.Command.UpdateSubPrefecture
{
    public class UpdateSubPrefectureValidator : AbstractValidator<UpdateSubPrefectureCommand>
    {
        private readonly ISubPrefectureRepository _subPrefectureRepository;
        private readonly IPrefectureRepository _prefectureRepository;

        public UpdateSubPrefectureValidator(ISubPrefectureRepository subPrefectureRepository, IPrefectureRepository prefectureRepository)
        {
            _subPrefectureRepository = subPrefectureRepository;
            _prefectureRepository = prefectureRepository;
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (id, token) =>
                {
                    return await _subPrefectureRepository.ExistsAsync(y => y.Id == id);
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
                .MustAsync(async (subPrefecture, token) =>
                {
                    var nameExist = await _subPrefectureRepository.ExistsAsync(v => v.Name.ToLower().Equals(subPrefecture.Name.ToLower()) && !subPrefecture.Id.Equals(v.Id));
                    return !nameExist;
                })
                .WithMessage($"The name already exist with another SubPrefecture.");
            RuleFor(x => x.PrefectureId)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (prefectureId, token) =>
                {
                    return await _prefectureRepository.ExistsAsync(v => v.Id.Equals(prefectureId));
                }).WithMessage("The {PropertyName} '{PropertyValue}' does not exist.");
        }
    }
}
