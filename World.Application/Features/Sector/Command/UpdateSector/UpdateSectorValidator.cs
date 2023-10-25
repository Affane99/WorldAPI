using FluentValidation;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Sector.Command.UpdateSector
{
    public class UpdateSectorValidator : AbstractValidator<UpdateSectorCommand>
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly ISubPrefectureRepository _subPrefectureRepository;

        public UpdateSectorValidator(ISectorRepository sectorRepository, ISubPrefectureRepository subPrefectureRepository)
        {
            _sectorRepository = sectorRepository;
            _subPrefectureRepository = subPrefectureRepository;
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (id, token) =>
                {
                    return await _sectorRepository.ExistsAsync(y => y.Id == id);
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
                .MustAsync(async (sector, token) =>
                {
                    var nameExist = await _sectorRepository.ExistsAsync(v => v.Name.ToLower().Equals(sector.Name.ToLower()) && !sector.Id.Equals(v.Id));
                    return !nameExist;
                })
                .WithMessage($"The name already exist with another Sector.");
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
