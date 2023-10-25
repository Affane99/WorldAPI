using FluentValidation;
using System;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.SubPrefecture.Command.CreateSubPrefecture
{
    public class CreateSubPrefectureValidator : AbstractValidator<CreateSubPrefectureCommand>
    {
        private readonly ISubPrefectureRepository _subPrefectureRepository;
        private readonly IPrefectureRepository _prefectureRepository;

        public CreateSubPrefectureValidator(ISubPrefectureRepository subPrefectureRepository, IPrefectureRepository prefectureRepository)
        {
            _subPrefectureRepository = subPrefectureRepository;
            _prefectureRepository = prefectureRepository;
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
                    var nameExist = await _subPrefectureRepository.ExistsAsync(v => v.Name.ToLower().Equals(name.ToLower()));
                    return !nameExist;
                })
                .WithMessage("{PropertyName} '{PropertyValue}' already exist.");
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
