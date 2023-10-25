using FluentValidation;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.SubPrefecture.Query.GetSubPrefectureDetails
{
    public class GetSubPrefectureDetailsValidator : AbstractValidator<GetSubPrefectureDetailsQuery>
    {
        private readonly ISubPrefectureRepository _subPrefectureRepository;

        public GetSubPrefectureDetailsValidator(ISubPrefectureRepository subPrefectureRepository)
        {
            _subPrefectureRepository = subPrefectureRepository;
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    return await _subPrefectureRepository.ExistsAsync(x => x.Id == id);
                }).WithMessage("The {PropertyName} with value \"{PropertyValue}\" does not exist.");
        }
    }
}
